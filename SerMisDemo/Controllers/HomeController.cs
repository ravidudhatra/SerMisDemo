using SerMisDemo.Data;
using SerMisDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SerMisDemo.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                Session["Name"] = name;
                string existingName = Session["Name"].ToString();
                Person person = db.People.FirstOrDefault(p => p.Name == existingName);
                if (person == null)
                {
                    person = new Person
                    {
                        Name = existingName,
                        Flag = 3,
                        DateUpdated = DateTime.Now
                    };
                    db.People.Add(person);
                    db.SaveChanges();
                }

                // Construct the URLs for the City and Age pages
                var cityUrl = Url.Action("City", "Home", new { id = person.Id });
                var ageUrl = Url.Action("Age", "Home", new { id = person.Id });

                return Json(new { CityUrl = cityUrl, AgeUrl = ageUrl }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ModelState.AddModelError("Name", "Please enter a name.");
                return View();
            }
        }

        public ActionResult City()
        {
            if (Session["Name"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult City(string city)
        {
            string name = Session["Name"].ToString();
            Person person = db.People.FirstOrDefault(p => p.Name == name);
            if (person != null)
            {
                person.City = city;
                person.Flag = 3;
                person.DateUpdated = DateTime.Now;
            }
            else
            {
                person = new Person
                {
                    Name = name,
                    City = city,
                    Flag = 3,
                    DateUpdated = DateTime.Now
                };
                db.People.Add(person);
            }
            db.SaveChanges();
            return RedirectToAction("Age", "Home");
        }

        public ActionResult Age()
        {
            if (Session["Name"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Age(int age)
        {
            string name = Session["Name"].ToString();
            Person person = db.People.FirstOrDefault(p => p.Name == name);
            if (person != null)
            {
                person.Age = age;
                person.Flag = 3;
                person.DateUpdated = DateTime.Now;
            }
            else
            {
                person = new Person
                {
                    Name = name,
                    Age = age,
                    Flag = 3,
                    DateUpdated = DateTime.Now
                };
                db.People.Add(person);
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}