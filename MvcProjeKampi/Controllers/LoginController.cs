
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using EntityLayer.Dto;
using MvcProjeKampi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcProjeKampi.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        IAuthService authService = new AuthManager(new AdminManager(new EfAdminDal()),
          new WriterManager(new EfWriterDal()));
    
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(AdminLoginDto adminDto)
        {

            if (authService.AdminLogin(adminDto))
            {
                //yönlendrime işlemleri
                FormsAuthentication.SetAuthCookie(adminDto.AdminUserName, false);
                Session["AdminUsername"] = adminDto.AdminUserName;
                return RedirectToAction("Index", "Heading");
            }
            else
            {
                //Hata Mesajı döndür
                ViewData["ErrorMessage"] = "Kullanıcı Adı veya Parola Yanlış!";
                return View();
            }
            //kullanıcı adı şifre yanlışsa bilgilendir
            //passwordu hasslar
            //mimariye taşı
            //Gelen kutusu okundu okunmadı /yapıldı
            //aktif yap pasif yap yapıldı

        }
        //Yazar Paneli

        [HttpGet]
        public ActionResult WriterLogin()
        {
          
            return View();
        }
        [HttpPost]
        public ActionResult WriterLogIn(WriterLoginDto writerLoginDto)
        {

            var response = Request["g-recaptcha-response"];
            const string secret = "6LfuH0gcAAAAAJJtGCmqJvJ3cMtHsI4RD6PAMowp";
            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResult>(reply);

            if (authService.WriterLogin(writerLoginDto) && captchaResponse.Success)           
            {
                FormsAuthentication.SetAuthCookie(writerLoginDto.WriterMail, false);
                Session["WriterMail"] = writerLoginDto.WriterMail;
               
                return RedirectToAction("MyContent", "WriterPanelContent");
            }
            else
            {
                ViewData["ErrorMessage"] = "Kullanıcı adı veya Parola yanlış";
                return View();
            }
        }




        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            //Session.Abandon();
            return RedirectToAction("HomePage", "Home");
        }
        public ActionResult WriterLogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("WriterLogin", "Login");
        }

    }
    }