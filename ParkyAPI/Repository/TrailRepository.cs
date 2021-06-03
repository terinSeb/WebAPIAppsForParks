using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkyAPI.Modals;
using ParkyAPI.Data;
using ParkyAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
namespace ParkyAPI.Repository
{
    public class TrailRepository : ITrailRepository
    {
        private readonly ApplicationDBContext _db;

        public TrailRepository(ApplicationDBContext db)
        {
            _db = db;
        }
        public bool CreateTrail(Trail Trail)
        {
            _db.Trails.Add(Trail);
            return Save();
        }

        public bool DeleteTrail(Trail Trail)
        {
            _db.Trails.Remove(Trail);
            return Save();
        }

        public ICollection<Trail> GetTrail()
        {
            return _db.Trails.Include(c => c.NationalPark).OrderBy(a => a.Name).ToList();
        }

        public Trail GetTrail(int TrailId)
        {
           return _db.Trails.Include(c => c.NationalPark).FirstOrDefault(a => a.Id == TrailId);            
        }

        public bool TrailExists(string Name)
        {
            bool Value = _db.Trails.Any(a => a.Name.ToLower().Trim() == Name.ToLower().Trim());
            return Value;
        }

        public bool TrailExistId(int Id)
        {
            bool Value = _db.Trails.Any(a => a.Id == Id);
            return Value;
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateTrail(Trail Trail)
        {
            _db.Trails.Update(Trail);
           return Save();
        }

        public ICollection<Trail> GetTrailsNationPark(int npId)
        {
            return _db.Trails.Include(c => c.NationalPark).Where(c => c.NationalParkId == npId).ToList();
        }
    }
}
