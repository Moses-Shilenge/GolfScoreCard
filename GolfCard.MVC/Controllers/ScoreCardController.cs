using GolfCard.Core.Business_Logic;
using GolfCard.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GolfCard.Core;
using GolfCard.MVC.Mapper;
using GolfCard.SqlClient.DTO;

namespace GolfCard.MVC.Controllers
{
    public class ScoreCardController : Controller
    {
        private GolfScoreCardLogic _golfScoreCardLogic;
        private GolfScoreMapper _golfScoreMapper;
        public ScoreCardController()
        {
            _golfScoreCardLogic = new GolfScoreCardLogic();
            _golfScoreMapper = new GolfScoreMapper();
        }
        // GET: ScoreCard
        public ActionResult ViewScoreCard()
        {

            var scores = _golfScoreCardLogic.GetScores();
            var scoreboard = _golfScoreMapper.MapToScoreCardView(scores);
            return View(scoreboard);
        }

        public ActionResult ViewStableScoring()
        {

            var scores = _golfScoreCardLogic.GetScores();
            var scoreboard = _golfScoreMapper.CalculatePointsAndMap(scores);
            return View(scoreboard);
        }

        public ActionResult CreateCard()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCard(IList<ScoreCardView> scorecard)
        {
            IEnumerable<Game> scores = new List<Game>();

            var playerScores = scorecard.Where(p => p.Player != null).Where(s => !s.Hole.Contains(0)).ToList();

            if (playerScores.Any())
            {
                scores = _golfScoreCardLogic.WorkOutScore(_golfScoreMapper.MapToGameModel(playerScores)).Result;
                _golfScoreCardLogic.SaveScores(scores);

                var scoreboard = _golfScoreMapper.MapToScoreCardView(scores);

                return RedirectToAction("ViewScoreCard", scoreboard);
            }
            else
            {
                ViewBag.ErrorMessage = "The scorecard contains invalid entries.\nPlease insert a player name and all the shots before clicking on the SUBMIT button.";
                return View();
            }                      
        }

        public ActionResult EditCard(Guid Id)
        {
            var player = _golfScoreCardLogic.GetGameById(Id);
            var scoreboard = (_golfScoreMapper.MapToScoreCardView(new List<Game>() { player }));
            return View(scoreboard.FirstOrDefault());
        }

        public ActionResult DeleteCard(Guid Id)
        {
            var player = _golfScoreCardLogic.GetGameById(Id);
            var scoreboard = (_golfScoreMapper.MapToScoreCardView(new List<Game>() { player }));
            return View(scoreboard.FirstOrDefault());
        }

        [HttpPost]
        public ActionResult EditCard(ScoreCardView scoreCardView)
        {
            if (scoreCardView.Player != null && !scoreCardView.Hole.Contains(0))
            {
                var scores = _golfScoreCardLogic.WorkOutScore(_golfScoreMapper.MapToGameModel(new List<ScoreCardView>() { scoreCardView })).Result;
                _golfScoreCardLogic.EditScores(scores.FirstOrDefault());
                return RedirectToAction("ViewScoreCard");
            }
            else
            {
                ViewBag.ErrorMessage = "The scorecard contains invalid entries.\nPlease insert all the shots before clicking on the SUBMIT button.";
                return View();
            }            
        }

        [HttpPost]
        public ActionResult DeleteCard(ScoreCardView scoreCardView)
        {
            try
            {
                _golfScoreCardLogic.DeleteScores(scoreCardView.Id);
                return RedirectToAction("ViewScoreCard");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error occurered while deleting the score \n {ex}";
                return View();
            }
        }
    }
}