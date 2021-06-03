using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkyAPI.Modals;

namespace ParkyAPI.Repository.IRepository
{
    public interface INationalParkRepository
    {
        ICollection<NationalParks> GetNationalParks();
        NationalParks GetNationalPark(int NationalParkId);
        bool NationalParkExists(string Name);
        bool NationalParkExistId(int Id);
        bool CreateNationalPark(NationalParks nationalParks);
        bool UpdateNationalPark(NationalParks nationalParks);
        bool DeleteNationalPark(NationalParks nationalParks);
        bool Save();
    }
}
