using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkyAPI.Modals;
using ParkyAPI.Data;
using ParkyAPI.Repository.IRepository;
namespace ParkyAPI.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDBContext _db;

        public NationalParkRepository(ApplicationDBContext db)
        {
            _db = db;
        }
        public bool CreateNationalPark(NationalParks nationalParks)
        {
            _db.NationalParks.Add(nationalParks);
            return Save();
        }

        public bool DeleteNationalPark(NationalParks nationalParks)
        {
            _db.NationalParks.Remove(nationalParks);
            return Save();
        }

        public ICollection<NationalParks> GetNationalParks()
        {
            return _db.NationalParks.OrderBy(a => a.Name).ToList();
        }

        public NationalParks GetNationalPark(int NationalParkId)
        {
           return _db.NationalParks.FirstOrDefault(a => a.Id == NationalParkId);            
        }

        public bool NationalParkExists(string Name)
        {
            bool Value = _db.NationalParks.Any(a => a.Name.ToLower().Trim() == Name.ToLower().Trim());
            return Value;
        }

        public bool NationalParkExistId(int Id)
        {
            bool Value = _db.NationalParks.Any(a => a.Id == Id);
            return Value;
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateNationalPark(NationalParks nationalParks)
        {
            _db.NationalParks.Update(nationalParks);
           return Save();
        }
    }
}
