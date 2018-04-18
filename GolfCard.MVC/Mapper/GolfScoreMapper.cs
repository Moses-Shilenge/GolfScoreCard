using GolfCard.MVC.Models;
using System.Collections.Generic;
using System;
using GolfCard.Core.Services;
using GolfCard.SqlClient.DTO;

namespace GolfCard.MVC.Mapper
{
    public class GolfScoreMapper
    {
        private ITeeRepository _teeRepository;

        public GolfScoreMapper()
        {
            _teeRepository = new TeeRepository();
        }

        public IEnumerable<Game> MapToGameModel(IList<ScoreCardView> card)
        {
            var mappedResult = new List<Game>();

            foreach (var item in card)
            {
                mappedResult.Add(new Game()
                {
                    Player = new SqlClient.DTO.Player
                    {
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
                    Tee = _teeRepository.GetTeeByIndex(i),
                    Shots = playerScores.Hole[i]
                });
            }           

            return shots;
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
                    IN_1 = player.IN_1,
                    IN_2 = player.IN_2,
                    OUT = player.OUT,
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