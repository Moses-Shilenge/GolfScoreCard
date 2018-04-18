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

        public ActionResult CreateCard()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCard(IList<ScoreCardView> scorecard)
        {
            IEnumerable<Game> scores = new List<Game>();

            var playerScores = scorecard.Where(p => p.Player != null).ToList();
            
           if (playerScores.Any())
            {
                scores = _golfScoreCardLogic.WorkOutScore(_golfScoreMapper.MapToGameModel(playerScores)).Result;
                _golfScoreCardLogic.SaveScores(scores);
            }
           else
               return new HttpStatusCodeResult(404, "The scorecard contains invalid entries.\nPlease insert a player name and all the shots before clicking on the SUBMIT button.");

            var scoreboard = _golfScoreMapper.MapToScoreCardView(scores);

            return RedirectToAction("ViewScoreCard", scoreboard);
        }
    }
}