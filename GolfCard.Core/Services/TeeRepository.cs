using GolfCard.SqlClient;
using GolfCard.SqlClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCard.Core.Services
{
    public class TeeRepository : ITeeRepository
    {
        private SQLDBContext _sqlConnect;

        public TeeRepository()
        {
           _sqlConnect = new SQLDBContext();
        }
        public Tee GetTeeByIndex(int index)
        {
            return _sqlConnect.Tees.Where(t => t.Index == index).FirstOrDefault();
        }
    }
}
