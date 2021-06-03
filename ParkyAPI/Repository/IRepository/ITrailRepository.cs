using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkyAPI.Modals;

namespace ParkyAPI.Repository.IRepository
{
    public interface ITrailRepository
    {
        ICollection<Trail> GetTrail();
        ICollection<Trail> GetTrailsNationPark(int npId);
        Trail GetTrail(int TrailId);
        bool TrailExists(string Name);
        bool TrailExistId(int Id);
        bool CreateTrail(Trail Trail);
        bool UpdateTrail(Trail Trail);
        bool DeleteTrail(Trail Trail);
        bool Save();
    }
}
