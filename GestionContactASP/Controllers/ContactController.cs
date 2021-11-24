using AutoMapper;
using GestionContact.CORE.servicesHTTP;
using GestionContact.DAL;
using GestionContactASP.Helpers;
using GestionContactASP.Models;
using GestionContactASP.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionContactASP.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly IContactService<Contact> _repo;
        private readonly IMapper _mapper;
        public ContactController(IContactService<Contact> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: ContactController
        public ActionResult Index()
        {
            try
            {
                IEnumerable<Contact> c = _repo.GetAll(SessionHelper.Get<LoginSuccessDto>(HttpContext.Session).Id);
                List<ContactASP> contacts = new List<ContactASP>();
                foreach (var item in c)
                {
                    contacts.Add(_mapper.Map<ContactASP>(item));
                }
                return View(contacts);

            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: ContactController/Details/5
        public ActionResult Details(int id)
        {
            Contact c = _repo.GetOne(id, SessionHelper.Get<LoginSuccessDto>(HttpContext.Session).Id);
            ContactASP contact = _mapper.Map<ContactASP>(c);
            return View(contact);
        }

        // GET: ContactController/Create
        public ActionResult Create()
        {
            ContactASP p = new ContactASP();
            p.UserId = SessionHelper.Get<LoginSuccessDto>(HttpContext.Session).Id;
            return View(p);
        }

        // POST: ContactController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContactASP contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    contact.UserId = SessionHelper.Get<LoginSuccessDto>(HttpContext.Session).Id;
                    if(_repo.Create(_mapper.Map<Contact>(contact)))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    ViewBag.error = "impossible de Créer un contact";
                }
                return View(contact);
            }
            catch (Exception)
            {

                return View("error");
            }
        }

        // GET: ContactController/Edit/5
        public ActionResult Edit(int id)
        {
            Contact c = _repo.GetOne(id, SessionHelper.Get<LoginSuccessDto>(HttpContext.Session).Id);
            ContactASP contact = _mapper.Map<ContactASP>(c);
            return View(contact);
        }

        // POST: ContactController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ContactASP c)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool contact = _repo.Update(id, _mapper.Map<Contact>(c));

                    if (contact)
                        return RedirectToAction(nameof(Index));
                    ViewBag.error = "impossible de modifier ce contact";
                }
                return View(c);
            }
            catch (Exception)
            {

                return View("error");
            }
        }

        // GET: ContactController/Delete/5
        public ActionResult Delete(int id)
        {
            Contact contact = _repo.GetOne(id, SessionHelper.Get<LoginSuccessDto>(HttpContext.Session).Id);

            ContactASP contactASP = _mapper.Map<ContactASP>(contact);

            return View(contactASP);
        }

        // POST: ContactController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ContactASP contact)
        {
            try
            {
                bool prod = _repo.Delete(id);
                if (prod)
                    return RedirectToAction(nameof(Index));

                ViewBag.error = "impossible de suppprimer le contact";
                return View(contact);
            }
            catch (Exception)
            {

                return View("error");
            }
        }

        // GET: ContactController/Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(IFormCollection collection)
        {
            IEnumerable<Contact> c = _repo.GetAllByName(SessionHelper.Get<LoginSuccessDto>(HttpContext.Session).Id, collection["search"]);
            List<ContactASP> contacts = new List<ContactASP>();
            foreach (var item in c)
            {
                contacts.Add(_mapper.Map<ContactASP>(item));
            }
            return View("Index", contacts);
        }
    }
}
