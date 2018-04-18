using GolfCard.SqlClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCard.Core.Services
{
    public interface ITeeRepository
    {
       Tee GetTeeByIndex(int index);
    }
}
