using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class MessageController : Controller
    {

        MessageManager mm = new MessageManager(new EfMessageDal());

        MessageValidator messagevalidators = new MessageValidator();
        // GET: Message
        public ActionResult Inbox()
        {
            var mail = (string)Session["AdminUserName"];
            var messageinlist = mm.GetListInbox(mail);
            return View(messageinlist);
        }

        public ActionResult Sendbox()
        {
            var mail = (string)Session["AdminUserName"];
            var messagesendlist = mm.GetSendbox(mail);
            return View(messagesendlist);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewMessage(Message p, string button)
        {
            ValidationResult results = messagevalidators.Validate(p);
            if (button == "draft")
            {

                results = messagevalidators.Validate(p);
                if (results.IsValid)
                {
                    string userEmail = (string)Session["AdminUserName"];
                    p.MessageDate = DateTime.Now;
                    p.SenderMail = userEmail;
                    p.isDraft = true;
                    mm.MessageAddBL(p);
                    return RedirectToAction("Draft");
                }
                else
                {
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            else if (button == "save")
            {
                results = messagevalidators.Validate(p);
                if (results.IsValid)
                {
                    string userEmail = (string)Session["AdminUserName"];
                    p.MessageDate = DateTime.Now;
                    p.SenderMail = userEmail;
                    p.isDraft = true;
                    mm.MessageAddBL(p);
                    return RedirectToAction("SendBox");
                }
                else
                {
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            return View();

       
        }
        public ActionResult GetInboxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            values.MessageRead = true;
            mm.MessageUpdate(values);
            return View(values);
        }
        public ActionResult GetSendboxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            values.MessageRead = true;
            mm.MessageUpdate(values);
            return View(values);
        }

        public ActionResult Draft()
        {
            var mail = (string)Session["AdminUserName"];
            var deger = mm.GetListDraft(mail);
            return View(deger);
        }
        public ActionResult GetDraftMessageDetails(int id)
        {
            var Values = mm.GetByID(id);

            return View(Values);
        }
        public ActionResult Read()
        {
            var mail = (string)Session["AdminUserName"];
            var deger = mm.GetReadList(mail);
            return View(deger);
        }

        public ActionResult UnRead()
        {
            var mail = (string)Session["AdminUserName"];
            var deger = mm.GetUnReadList(mail);
            return View(deger);
        }
    }
}