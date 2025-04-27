using LogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataDomain;
using WebPresentation.Models;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using NuGet.Common;

namespace WebPresentation.Controllers
{
    public class CopyController : Controller
    {
        BookManager _bookManager = new BookManager();
        TransactionManager _transactionManager = new TransactionManager();

        // POST: CopyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, IFormCollection collection)
        {
            try
            {
                string condition = collection["condition"];

                if (condition is null || condition == "")
                {
                    throw new ArgumentException("Condition Cannot Be Empty");
                }

                _bookManager.addCopy(new Copy { BookId = id, Condition = condition });

                return RedirectToAction("Edit", "Book", new { id = id });
            }
            catch
            {
                return RedirectToAction("Edit", "Book", new { id = id });
            }
        }

        // POST: CopyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                string condition = collection["condition"];
                int copyId = int.Parse(collection["copyId"]);

                if (condition is null || condition == "")
                {
                    throw new ArgumentException("Condition Cannot Be Empty");
                }

                _bookManager.editCopy(new Copy { CopyID = copyId, Condition = condition }, _bookManager.getCopyById(copyId));

                return RedirectToAction("Edit", "Book", new { id = id });
            }
            catch
            {
                return RedirectToAction("Edit", "Book", new { id = id });
            }
        }

        // POST: CopyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int copyId, int bookId, IFormCollection collection)
        {
            try
            {
                if (_bookManager.getCopyById(copyId).Active)
                {
                    _bookManager.deactivateCopy(copyId);
                } else
                {
                    _bookManager.activateCopy(copyId);
                }

                return RedirectToAction("Edit", "Book", new { id = bookId });
            }
            catch
            {
                return RedirectToAction("Edit", "Book", new { id = bookId });
            }
        }

        // GET: CopyController/CheckoutList/5
        [HttpGet]
        public ActionResult Checkout()
        {
            return View(CheckoutList.copies);
        }

        // POST: CopyController/Checkout/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Checkout(IFormCollection collection)
        {
            try
            {
                AccessToken token = new AccessToken(User.Identity.Name);
                if (token.IsSet)
                {
                    List<Transaction> transaction = new List<Transaction>();

                    foreach (var copy in CheckoutList.copies)
                    {
                        Transaction copyTransaction = new Transaction()
                        {
                            UserId = token.UserId,
                            TransactionType = "CHECKOUT",
                            CopyId = copy.CopyID,
                            TransactionDate = DateTime.Now
                        };
                        transaction.Add(copyTransaction);
                    }

                    _transactionManager.checkoutBook(transaction);
                    CheckoutList.copies.Clear();

                    return RedirectToAction("Index", "Home");
                } 
                else
                {
                    throw new ArgumentException("User Does Not Correspond to DB User");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(CheckoutList.copies);
            }
        }

        // POST: CopyController/Checkin/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Checkin(int id, IFormCollection collection)
        {
            try
            {
                Transaction transaction = new Transaction()
                {
                    UserId = new AccessToken(User.Identity.Name).UserId,
                    TransactionType = "CHECKIN",
                    CopyId = id,
                    TransactionDate = DateTime.Now
                };

                _transactionManager.checkinBook(transaction);

                return RedirectToAction(nameof(CheckedOut));
            }
            catch
            {
                return View();
            }
        }

        // POST: CopyController/AddToCheckout/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCheckout(int copyId, int bookId)
        {
            try
            {
                if (CheckoutList.IsInList(copyId))
                {
                    throw new ArgumentException("Copy is Already In Checkout List");
                } 
                else
                {
                    CheckoutList.copies.Add(_bookManager.getCopyVMById(copyId));
                }

                return RedirectToAction("Details", "Book", new { id = bookId });
            }
            catch
            {
                return RedirectToAction("Details", "Book", new { id = bookId });
            }
        }

        // POST: CopyController/RemoveFromCheckout/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveFromCheckout(int id)
        {
            try
            {
                if (!CheckoutList.IsInList(id))
                {
                    throw new ArgumentException("Copy is Not In Checkout List");
                }
                else
                {
                    CheckoutList.RemoveFromList(id);
                }

                return RedirectToAction(nameof(Checkout));
            }
            catch
            {
                return RedirectToAction(nameof(Checkout));
            }
        }
        
        // GET: CopyController/CheckoutList/5
        [HttpGet]
        [Authorize]
        public ActionResult CheckedOut()
        {
            List<CopyVM> copies = new List<CopyVM>();

            try
            {
                copies = _transactionManager.getCheckedOutCopies(new AccessToken(User.Identity.Name).UserId);
            }
            catch {}

            return View(copies);
        }
    }
}
