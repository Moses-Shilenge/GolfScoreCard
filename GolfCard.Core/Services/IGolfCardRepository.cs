using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GolfCard.SqlClient.DTO;

namespace GolfCard.Core.Services
{
    public interface IGolfCardRepository
    {
        void CreateCard(IEnumerable<Game> scores);
        IEnumerable<Game> GetScoreBoard();
        IList<Tee> GetAllParsFromTees();
        Game GetGameById(Guid id);
        void EditCard(Game game);
        void DeleteCardById(Guid id);
    }
}
