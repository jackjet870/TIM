using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TIM.Data;
using TIM.Data.Helpers;
using TIM.Data.Repositories.Implementation;
using TIM.Data.Repositories.Interface;

namespace TIM.Web.Controllers
{
    public class AthleteController : Controller
    {
        private IAthleteRepository _athRepo;
        private ITeamRepository _teamRepo;

        public AthleteController()
        {
            // tymczasowo; potem użyć jakiejś frameworka do DI
            this._athRepo = new AthleteRepository();
            this._teamRepo = new TeamRepository();
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            IEnumerable<Athlete> athletes = _athRepo.GetAll();

            return View(athletes);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.teamsList = ItemListCreator.Teams(_teamRepo);
            ViewBag.sportsList = ItemListCreator.Sports();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Athlete athlete)
        {
            ViewBag.teamsList = ItemListCreator.Teams(_teamRepo);
            ViewBag.sportsList = ItemListCreator.Sports();

            if (ModelState.IsValid)
            {
                if (_athRepo.Add(athlete))
                {
                    ViewBag.Success = "Added succesfully";                    
                    return View();
                }                   
            }

            ViewBag.Error = "Error adding an athlete!";
            return View();
        }

        [HttpGet] // można w przyszłości zmienić na Post i dorobić jakiegoś Ajaxa do usuwania
        public ActionResult Delete(decimal id)
        {
            if (_athRepo.Delete(id))
            {
                TempData["Success"] = "Deleted successfully!";
            }
            else
                TempData["Error"] = "Error deleting athlete!";


            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            Athlete athlete = _athRepo.GetById((int)id);

            var teamsList = ItemListCreator.Teams(_teamRepo);
            teamsList.ElementAt(0).Selected = false;
            teamsList.Where(item => item.Text == athlete.TeamName).First().Selected = true;
            ViewBag.teamsList = teamsList;


            var sportsList = ItemListCreator.Sports();
            sportsList.Where(sport => sport.Text == athlete.Sport).First().Selected = true;
            sportsList.ElementAt(0).Selected = false;
            ViewBag.sportsList = sportsList;

            return View(athlete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Athlete athlete)
        {
            if (ModelState.IsValid)
            {
                if (_athRepo.Update(athlete))
                {
                    TempData["Success"] = "Updated succesfully";
                    return RedirectToAction("List");
                }
            }

            TempData["Error"] = "Error updating an athlete!";
            return RedirectToAction("List");
        }
    }
}
