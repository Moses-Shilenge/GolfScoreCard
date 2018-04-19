using GolfCard.MVC.Models;
using System.Collections.Generic;
using System;
using GolfCard.Core.Services;
using GolfCard.SqlClient.DTO;
using GolfCard.Core.Business_Logic;

namespace GolfCard.MVC.Mapper
{
    public class GolfScoreMapper
    {
        private ITeeRepository _teeRepository;
        private GolfScoreCardLogic _golfScoreCardLogic;

        public GolfScoreMapper()
        {
            _teeRepository = new TeeRepository();
            _golfScoreCardLogic = new GolfScoreCardLogic();
        }

        public IEnumerable<Game> MapToGameModel(IList<ScoreCardView> card)
        {
            var mappedResult = new List<Game>();

            foreach (var item in card)
            {
                mappedResult.Add(new Game()
                {
                    Id = item.Id,
                    Player = new SqlClient.DTO.Player
                    {
                        Id = item.Id,
                        Name = item.Player
                    },
                    Shots = MapShots(item),
                    Date = DateTime.Now,
                    HCP = item.HandiCap
                });
            }

            return mappedResult;
        }

        private List<Shot> MapShots(ScoreCardView playerScores)
        {
            var shots = new List<Shot>();

            for (int i = 0; i < playerScores.Hole.Count; i++)
            {
                shots.Add(new Shot
                {
                    Id = playerScores.Id,
                    Tee = _teeRepository.GetTeeByIndex(i),
                    Shots = playerScores.Hole[i]
                });
            }           

            return shots;
        }

        internal List<StableFord> CalculatePointsAndMap(IEnumerable<Game> PlayerScores)
        {
            var stableFordRes = new List<StableFord>();

            foreach (var score in PlayerScores)
            {
                var handicapAllocation = _golfScoreCardLogic.CalculateStableFord(score);

                stableFordRes.Add(new StableFord
                {
                    Id = score.Id,
                    Player = score.Player.Name,
                    HandiCap = score.HCP,
                    Hole = MapShotsToScoreCardView(score.Shots),
                    Points = handicapAllocation,
                    IN = score.IN,
                    OUT = score.OUT,
                    TOT = score.TOT,
                    NET = score.NET,
                    StableFordsPoints = _golfScoreCardLogic.SumUpPoints(handicapAllocation)
                });
            }

            return stableFordRes;
        }

        internal IList<ScoreCardView> MapToScoreCardView(IEnumerable<Game> playerScores)
        {
            var scoreView = new List<ScoreCardView>();

            foreach (var player in playerScores)
            {
                scoreView.Add(new ScoreCardView
                {
                    Id = player.Id,
                    Player = player.Player.Name,
                    HandiCap = player.HCP,
                    Hole = MapShotsToScoreCardView(player.Shots),
                    IN = player.IN,
                    OUT = player.OUT,
                    TOT = player.TOT,
                    NET = player.NET
                });
            }

            return scoreView;
        }

        private List<int> MapShotsToScoreCardView(List<Shot> shots)
        {
            var score = new List<int>();

            foreach (var shot in shots)
            {
                score.Add(shot.Shots);
            }

            return score;
        }
    }
}