using LogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataDomain;
using WebPresentation.Models;
using System;
using Microsoft.AspNetCore.Authorization;

namespace WebPresentation.Controllers
{
    public class UserController : Controller
    {
        public UserManager _userManager = new UserManager();
        public TransactionManager _transactionManager = new TransactionManager();
        private AccessToken _token;

        // GET: UserController
        [Authorize]
        public ActionResult Index()
        {
            _token = new AccessToken(User.Identity.Name);

            if (_token.IsSet && _token.IsLibrarian)
            {
                List<UserVM> users = _userManager.getAllUsers();

                return View(users);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        // GET: UserController/Details/5
        [Authorize]
        public ActionResult Details(string email)
        {
            _token = new AccessToken(User.Identity.Name);

            if (_token.IsSet && _token.IsLibrarian)
            {
                UserVM user = _userManager.getUserByEmail(email);

                ViewBag.Transactions = _transactionManager.getTransactionsByUserId(user.UserID);
                ViewBag.Copies = _transactionManager.getCheckedOutCopies(user.UserID);

                return View(user);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        // GET: UserController/Edit
        [Authorize]
        public ActionResult Edit() 
        {
            ViewBag.User = _userManager.getUserByEmail(User.Identity.Name);
            ViewBag.Active = "active";

            return View();
        }

        // POST: UserController/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IFormCollection collection)
        {
            try
            {
                _userManager.editName(collection["firstname"], collection["lastname"], User.Identity.Name);
            }
            catch { }

            ViewBag.User = _userManager.getUserByEmail(User.Identity.Name);
            ViewBag.Active = "active";

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
        [Authorize]
        public ActionResult Transaction(int id, string email)
        {
            _token = new AccessToken(User.Identity.Name);

            if (_token.IsSet && _token.IsLibrarian)
            {
                List<CopyVM> copies = _transactionManager.getCopiesByTransactionId(id);

                ViewBag.Email = email;

                return View(copies);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }
    }
}
