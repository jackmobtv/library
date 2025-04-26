using LogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataDomain;

namespace WebPresentation.Controllers
{
    public class CopyController : Controller
    {
        BookManager _bookManager = new BookManager();

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

        // POST: CopyController/Checkout/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: CopyController/Checkin/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkin(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
