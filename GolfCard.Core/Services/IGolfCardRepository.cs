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
        void SaveCard(IEnumerable<Game> scores);
        IEnumerable<Game> GetScoreBoard();
    }
}
