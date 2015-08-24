using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TIM.Data;
using TIM.Data.Helpers;
using TIM.Data.ModelClasses;
using TIM.Data.ModelClasses.Dto;
using TIM.Data.Repositories.Implementation;
using TIM.Data.Repositories.Interface;
using TIM.Web.Models;

namespace TIM.Web.Controllers
{
    public class EventController : Controller
    {
        private IEventRepository _eventRepo;
        private IAthleteRepository _athRepo;
        private ITeamRepository _teamRepo;

        public EventController()
        {
            // tymczasowo; potem użyć jakiejś frameworka do DI
            this._eventRepo = new EventRepository();
            this._athRepo = new AthleteRepository();
            this._teamRepo = new TeamRepository();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult List()
        {
            IEnumerable<EventViewModel> events =
                    EventViewModel.CreateViewModels(_eventRepo.GetAll());

            return View(events);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.sportsList = ItemListCreator.Sports();
            ViewBag.teamsList = ItemListCreator.Teams(_teamRepo);
            ViewBag.athletesList = ItemListCreator.Athletes(_athRepo);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(EventViewModel ev)
        {
            ViewBag.sportsList = ItemListCreator.Sports();
            ViewBag.teamsList = ItemListCreator.Teams(_teamRepo);
            ViewBag.athletesList = ItemListCreator.Athletes(_athRepo);

            if (ModelState.IsValid)
            {
                EventDTO eventDTO = Mapper.Map<EventViewModel, EventDTO>(ev);
                if (_eventRepo.Add(eventDTO))
                {
                    ViewBag.Success = "Added succesfully";
                    return View();
                }
            }

            ViewBag.Error = "Error adding an event!";
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (_eventRepo.Delete(id))
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
            EventDTO evDto = _eventRepo.GetById(id);
            EventViewModel eventViewModel = new EventViewModel(evDto, _teamRepo, _athRepo);
            
            return View(eventViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EventViewModel ev)
        {
            if (ModelState.IsValid)
            {
                if (_eventRepo.Update(new EventDTO(ev)))
                {
                    TempData["Success"] = "Updated successfully";
                    return RedirectToAction("List");
                }
            }

            TempData["Error"] = "Error updating an event!";
            return RedirectToAction("List");
        }
    }
}