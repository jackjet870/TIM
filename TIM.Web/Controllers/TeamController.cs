using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TIM.Data.Repositories.Interface;
using TIM.Data.Repositories.Implementation;
using TIM.Data;
using TIM.Data.Helpers;

namespace TIM.Web.Controllers
{
    public class TeamController : Controller
    {
        private ITeamRepository _teamRepo;
        private IAthleteRepository _athRepo;

        public TeamController()
        {
            _teamRepo = new TeamRepository();
            _athRepo = new AthleteRepository();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult List()
        {
            IEnumerable<Team> teams = _teamRepo.GetAll();

            return View(teams);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.sportsList = ItemListCreator.Sports();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Team team)
        {
            ViewBag.sportsList = ItemListCreator.Sports();

            if (ModelState.IsValid)
            {
                if (_teamRepo.Add(team))
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
            if (_teamRepo.Delete(id))
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
            Team team = _teamRepo.GetById(id);

            var sportsList = ItemListCreator.Sports();
            sportsList.Where(sport => sport.Text == team.Sport).FirstOrDefault().Selected = true;
            sportsList.ElementAt(0).Selected = false;
            ViewBag.sportsList = sportsList;


            return View(team);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Team team)
        {
            if (ModelState.IsValid)
            {
                if (_teamRepo.Update(team))
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