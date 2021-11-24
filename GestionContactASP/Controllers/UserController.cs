using AutoMapper;
using GestionContact.CORE.servicesHTTP;
using GestionContact.DAL;
using GestionContactASP.Helpers;
using GestionContactASP.Models;
using GestionContactASP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionContactASP.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService<User> _repo;
        private readonly IMapper _mapper;

        public UserController(IUserService<User> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: UserController
        [Authorize]
        public ActionResult Index()
        {
            IEnumerable<User> c = _repo.GetAll();
            List<UserAsp> contacts = new List<UserAsp>();
            foreach (var item in c)
            {
                contacts.Add(_mapper.Map<UserAsp>(item));
            }
            return View(contacts);
        }

        // GET: UserController/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            User c = _repo.GetOne(id);
            UserAsp contact = _mapper.Map<UserAsp>(c);
            return View(contact);
        }

        // GET: UserController/Create
        
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserAsp contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_repo.Create(_mapper.Map<User>(contact)))
                        return RedirectToAction("Index", "Contact");
                    ViewBag.error = "impossible de Créer un utilisateur";
                }

                return View(contact);
            }
            catch (Exception)
            {

                return View("error");
            }
        }

        // GET: UserController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            User c = _repo.GetOne(id);
            UserAsp contact = _mapper.Map<UserAsp>(c);
            return View(contact);
        }

        // POST: UserController/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserAsp c)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool user = _repo.Update(id, _mapper.Map<User>(c));
                    if (user)
                        return RedirectToAction(nameof(Index));

                    ViewBag.error = "impossible de modifier l'utilisateur";
                }
                return View(c);
            }
            catch (Exception)
            {

                return View("error");
            }
        }

        // GET: UserController/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            User contact = _repo.GetOne(id);

            UserAsp contactASP = _mapper.Map<UserAsp>(contact);

            return View(contactASP);
        }

        // POST: UserController/Delete/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, UserAsp contact)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    bool user = _repo.Delete(id);
                    if (user)
                    {
                        ViewBag.error = "Impossible de supprimer cet utilisateur";
                        return RedirectToAction(nameof(Index));
                    }
                }
                
                return View(contact);
            }
            catch (Exception)
            {
                return View("error");
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginDto user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LoginSuccessDto u = _mapper.Map<LoginSuccessDto>(_repo.Login(user.Email, user.Password));
                    if (u != null)
                    {
                        SessionHelper.Set<LoginSuccessDto>(HttpContext.Session, u);

                        return RedirectToAction("Index", "Contact");
                    }
                    ViewBag.error = "erreur login/mot de passe";
                    
                }
                Login l = _mapper.Map<Login>(user);
                return View(l);
            }
            catch (Exception)
            {

                return View("error");
            }
        }

        [Authorize]
        public ActionResult LogOut()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}