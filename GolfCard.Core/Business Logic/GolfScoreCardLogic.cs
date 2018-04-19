using GolfCard.Core.Services;
using GolfCard.SqlClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCard.Core.Business_Logic
{
    public class GolfScoreCardLogic
    {
        private IGolfCardRepository _golfCardRepository;
        public GolfScoreCardLogic()
        {
            _golfCardRepository = new GolfCardRepository();
        }
        public async Task<IEnumerable<Game>> WorkOutScore(IEnumerable<Game> games)
        {
            List<Game> workOutScores = new List<Game>();

            foreach (var game in games)
            {
                // seperate the Enneads
                var firstEnnead = game.Shots.Take(9);
                var secondEnnead = game.Shots.Skip(9).Take(9);

                // first ennead shots and second ennead shots
                game.IN = firstEnnead.Sum(s => s.Shots);
                game.OUT = secondEnnead.Sum(s => s.Shots);

                // IN values added to give the total number of shots (OUT) for all 18 holes
                game.TOT = game.IN + game.OUT;

                // NET shots after Handicap shots removed
                game.NET = game.TOT - game.HCP;

                workOutScores.Add(game);
            }

            return await Task.FromResult(workOutScores);
        }

        public IEnumerable<Game> GetScores()
        {
            return _golfCardRepository.GetScoreBoard();
        }

        public void SaveScores(IEnumerable<Game> scores)
        {
            _golfCardRepository.SaveCard(scores);
        }

        public Game GetGameById(Guid Id)
        {
            return _golfCardRepository.GetGameById(Id);
        }

        public List<int> CalculateStableFord(Game score)
        {
            var handiCapToAllocate = score.HCP;
            var handiCaps = new int[18];
            var points = new int[18];
            var pars = _golfCardRepository.GetAllParsFromTees().Select(t => t.Par).ToList();

            while (handiCapToAllocate != 0)
            {
                for (int i = 0; i < 18; i++)
                {
                    handiCaps[i] = handiCaps[i] + 1;
                    handiCapToAllocate--;

                    if (handiCapToAllocate <= 0) break; 
                }
            }

            for (int i = 0; i < 18; i++)
            {
                var newPar = handiCaps[i] + pars[i];

                if (newPar == score.Shots[i].Shots)
                {
                    points[i] = 2;
                }
                else if ((newPar + 1) == score.Shots[i].Shots)
                {
                    points[i] = 1;
                }                
                else if ((newPar - 1) == score.Shots[i].Shots)
                {
                    points[i] = 3;
                }
                else if ((newPar - 2) == score.Shots[i].Shots)
                {
                    points[i] = 4;
                }
                else
                {
                    points[i] = 0;
                }
            }

            return points.ToList();
        }

        public void EditScores(Game game)
        {
            _golfCardRepository.EditCard(game);
        }

        public int SumUpPoints(List<int> points)
        {
            var totalPoints = 0;

            foreach (var point in points)
            {
                totalPoints += point;
            }

            return totalPoints;
        }

        public void DeleteScores(Guid Id)
        {
            _golfCardRepository.DeleteCardById(Id);
        }
    }
}
