using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class ContactController : Controller
    {
        MessageManager mm = new MessageManager(new EfMessageDal());
        ContactManager cm = new ContactManager(new EfContactDal());

        // GET: Contact
        public ActionResult Index()
        {
            var contactvalues = cm.GetList();
            return View(contactvalues);
        }
        public ActionResult GetContactDetails(int id)
        {
            var contactvalues = cm.GetByID(id);

            return View(contactvalues);
        }
        public PartialViewResult PartialMessageMenu()
        {
            var contactList = cm.GetList();
            ViewBag.contactCount = contactList.Count();
         
            //ViewBag.draftCount = drafList.Count();
            var deger5 = mm.GetReadList();
            ViewBag.sayi5 = deger5.Count();

            var deger6 = mm.GetUnReadList();
            ViewBag.sayi6 = deger6.Count();

            return PartialView();
        }
    }
}