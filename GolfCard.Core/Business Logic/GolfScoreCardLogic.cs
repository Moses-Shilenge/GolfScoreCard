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
                game.IN_1 = firstEnnead.Sum(s => s.Shots);
                game.IN_2 = secondEnnead.Sum(s => s.Shots);

                // IN values added to give the total number of shots (OUT) for all 18 holes
                game.OUT = game.IN_1 + game.IN_2;

                // NET shots after Handicap shots removed
                game.NET = game.OUT - game.HCP;

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

        public Game GetGameByIndex(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
