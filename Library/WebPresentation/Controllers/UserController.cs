using LogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataDomain;
using WebPresentation.Models;

namespace WebPresentation.Controllers
{
    public class UserController : Controller
    {
        public UserManager _userManager = new UserManager();
        public TransactionManager _transactionManager = new TransactionManager();

        // GET: UserController
        public ActionResult Index()
        {
            List<UserVM> users = _userManager.getAllUsers();

            return View(users);
        }

        // GET: UserController/Details/5
        public ActionResult Details(string email)
        {
            UserVM user = _userManager.getUserByEmail(email);

            ViewBag.Transactions = _transactionManager.getTransactionsByUserId(user.UserID);
            ViewBag.Copies = _transactionManager.getCheckedOutCopies(user.UserID);

            return View(user);
        }

        // GET: UserController/Edit
        public ActionResult Edit() 
        {
            ViewBag.User = _userManager.getUserByEmail(User.Identity.Name);
            ViewBag.Active = "active";

            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IFormCollection collection)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, bool active, IFormCollection collection)
        {
            try
            { 
                if(active)
                {
                    _userManager.deactivateUser(id);
                }
                else
                {
                    _userManager.activateUser(id);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: UserController/Transaction/5
        public ActionResult Transaction(int id, string email)
        {
            List<CopyVM> copies = _transactionManager.getCopiesByTransactionId(id);

            ViewBag.Email = email;

            return View(copies);
        }
    }
}
