
using DataAccessLayer.Concrete;
using MvcProjeKampi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }
        #region Mimarisiz Kategori icerisindeki Baslik Listesi Grafigi
        public ActionResult CategoryChart() //köprü görevi gören action result view oluşturulmayacak
        {
            return Json(BlogList(), JsonRequestBehavior.AllowGet); //Kategory islemleri icin kullanilan bir yazim komutu
        }

        public List<Category> BlogList()
        {
            List<Category> ct = new List<Category>();
            ct.Add(new Category()
            {
                CategoryName = "Yazılım",
                CategoryCount = 8
            });
            ct.Add(new Category()
            {
                CategoryName = "Gezi",
                CategoryCount = 4
            });
            ct.Add(new Category()
            {
                CategoryName = "Teknoloji",
                CategoryCount = 7
            });
            ct.Add(new Category()
            {
                CategoryName = "Spor",
                CategoryCount = 3
            });
            return ct;
        }
        #endregion
        public ActionResult WriterListLineChart()
        {
            return View();
        }

        // Yazarlarin Baslik Sayisi Listesi \\

        #region Mimarili Yazar Baslik Sayisi Listesi Grafigi

        public ActionResult WriterListChart()
        {
            return Json(WriterList(), JsonRequestBehavior.AllowGet);
        }

        public List<Writer> WriterList()
        {
            List<Writer> writerClasses = new List<Writer>();
            using (var context = new Context())
            {
                writerClasses = context.Writers.Select(x => new Writer
                {
                    WriterName = x.WriterName,
                    WriterCount = x.Headings.Count()
                }).ToList();
            }
            return writerClasses;
        }

        #endregion

        public ActionResult HeadingListColumnChart()
        {
            return View();
        }
        // Basliklarin Icerik Sayisi Listesi \\

        #region Mimarili Baslik  Icerik Sayisi Listesi Grafigi

        public ActionResult HeadingListChart()
        {
            return Json(HeadingList(), JsonRequestBehavior.AllowGet);
        }

        public List<Heading> HeadingList()
        {
            List<Heading> headingClasses = new List<Heading>();
            using (var context = new Context())
            {
                headingClasses = context.Headings.Select(x => new Heading
                {
                    HeadingName = x.HeadingName,
                    HeadingCount = x.Contents.Count()
                }).ToList();
            }

            return headingClasses;
        }

        #endregion
    }
}