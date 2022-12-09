using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class SkillController : Controller
    {

        SkillManager sm = new SkillManager(new EfSkillDal());
        TalentManager tm = new TalentManager(new EfTalentDal());
        // GET: Skill
        public ActionResult Index()
        {
            var skillValues = tm.GetList();
            return View(skillValues);
        }
        public ActionResult SkillDetails(int id)
        {
            var skildetails = tm.GetByID(id);

            return View(skildetails);

        }
    }
}