using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TIM.Data.Repositories.Interface;

namespace TIM.Data.Helpers
{
    public static class ItemListCreator
    {
        public static IEnumerable<SelectListItem> Sports()
        {
            var selectList = new List<SelectListItem>();

            var item = new SelectListItem()
            {
                Text = "football",
                Selected = false,
                Value = "football"
            };
            selectList.Add(item);

            item = new SelectListItem()
            {
                Text = "snooker",
                Selected = false,
                Value = "snooker"
            };
            selectList.Add(item);

            item = new SelectListItem()
            {
                Text = "tennis",
                Selected = false,
                Value = "tennis"
            };
            selectList.Add(item);

            item = new SelectListItem()
            {
                Text = "chess",
                Selected = false,
                Value = "chess"
            };
            selectList.Add(item);

            return selectList;
        }

        public static IEnumerable<SelectListItem> Sports(string sport)
        {
            var selectList = new List<SelectListItem>();

            var item = new SelectListItem()
            {
                Text = "football",
                Selected = false,
                Value = "football"
            };
            selectList.Add(item);
            if (item.Text == sport)
                item.Selected = true;

            item = new SelectListItem()
            {
                Text = "snooker",
                Selected = false,
                Value = "snooker"
            };
            selectList.Add(item);
            if (item.Text == sport)
                item.Selected = true;

            item = new SelectListItem()
            {
                Text = "tennis",
                Selected = false,
                Value = "tennis"
            };
            selectList.Add(item);
            if (item.Text == sport)
                item.Selected = true;

            item = new SelectListItem()
            {
                Text = "chess",
                Selected = false,
                Value = "chess"
            };
            selectList.Add(item);
            if (item.Text == sport)
                item.Selected = true;

            return selectList;
        }

        public static IEnumerable<SelectListItem> Athletes(IAthleteRepository _athRepo)
        {
            var athletes = _athRepo.GetAll();

            var selectList = new List<SelectListItem>();

            if (athletes != null)
            {
                foreach (var athlete in athletes)
                {
                    var item = new SelectListItem();
                    item.Text = athlete.FirstName + " " + athlete.LastName;
                    item.Value = athlete.AthleteId.ToStringIgnoreFraction();
                    selectList.Add(item);
                }
            }

            return selectList;
        }

        public static IEnumerable<SelectListItem> Athletes(IAthleteRepository _athRepo, IEnumerable<decimal> ids)
        {
            var athletes = _athRepo.GetAll();

            var selectList = new List<SelectListItem>();

            if (athletes != null)
            {
                foreach (var athlete in athletes)
                {
                    var item = new SelectListItem();
                    item.Text = athlete.FirstName + " " + athlete.LastName;
                    item.Value = athlete.AthleteId.ToStringIgnoreFraction();

                    if (ids.Contains(athlete.AthleteId))
                        item.Selected = true;

                    selectList.Add(item);
                }
            }

            return selectList;
        }

        public static IEnumerable<SelectListItem> Teams(ITeamRepository _teamRepo)
        {
            var teams = _teamRepo.GetAll();

            var selectList = new List<SelectListItem>();

            if (teams != null)
            {
                foreach (var team in teams)
                {
                    var item = new SelectListItem();

                    item.Text = team.Name;
                    item.Value = team.TeamId.ToStringIgnoreFraction();

                    selectList.Add(item);
                }
            }

            return selectList;
        }

        public static IEnumerable<SelectListItem> Teams(ITeamRepository _teamRepo, IEnumerable<decimal> ids)
        {
            var teams = _teamRepo.GetAll();

            var selectList = new List<SelectListItem>();

            if (teams != null)
            {
                foreach (var team in teams)
                {
                    var item = new SelectListItem();

                    item.Text = team.Name;
                    item.Value = team.TeamId.ToStringIgnoreFraction();

                    if (ids.Contains(team.TeamId))
                        item.Selected = true;

                    selectList.Add(item);
                }
            }

            return selectList;
        }

        public static IEnumerable<string> SportsList()
        {
            string[] Sports = new string[4];
            Sports[0] = "football";
            Sports[1] = "tennis";
            Sports[2] = "snooker";
            Sports[3] = "chess";

            return Sports;
        }
    }
}