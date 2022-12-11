using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using EntityLayer.Dto;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{

    public class WriterController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterDal());
        WriterValidator writervalidator=new WriterValidator();
        IAuthService authService = new AuthManager(new AdminManager(new EfAdminDal()), new WriterManager(new EfWriterDal()));
        RoleManager roleManager = new RoleManager(new EfRoleDal());

        // GET: Writer
        public ActionResult Index()
        {
            var WriterValues = wm.GetList();
            return View(WriterValues);
        }
        [HttpGet]
        public ActionResult AddWriter()
        {
            List<SelectListItem> valuewriterrole = (from c in roleManager.GetRoles()
                                                   select new SelectListItem
                                                   {
                                                       Text = c.RoleName,
                                                       Value = c.RoleId.ToString()

                                                   }).ToList();

            ViewBag.valuewriter = valuewriterrole;
            return View();
        }
      

        [HttpPost]
        public ActionResult AddWriter(WriterLoginDto loginDto)
        {          
            authService.WriterRegister(loginDto.WriterName,   loginDto.WriterSurname, loginDto.WriterImage, loginDto.WriterAbout,  loginDto.WriterMail, loginDto.WriterPassword, loginDto.WriterTitle, loginDto.RoleId);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditWriter(int id)
        {

            var writervalue = wm.GetByID(id);
            List<SelectListItem> valuewriterrole = (from c in roleManager.GetRoles()
                                                    select new SelectListItem
                                                    {
                                                        Text = c.RoleName,
                                                        Value = c.RoleId.ToString()

                                                    }).ToList();

            ViewBag.valuewriter = valuewriterrole;
            return View(writervalue);
        }
        [HttpPost]
        public ActionResult EditWriter(Writer p,WriterLoginDto loginDto)
        {
            ValidationResult results = writervalidator.Validate(p);

            if (results.IsValid)
            {

                if (Request.Files.Count > 0)
                {
                    string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                    string uzanti = Path.GetExtension(Request.Files[0].FileName);
                    string yol = "~/Image/" + dosyaadi + uzanti;
                    Request.Files[0].SaveAs(Server.MapPath(yol));
                    p.WriterImage = "/Image/" + dosyaadi + uzanti;
                }
                wm.WriterUpdate(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();

        }
    }
}