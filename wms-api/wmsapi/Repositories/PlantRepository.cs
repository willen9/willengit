using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using wmsapi.Models;

namespace wmsapi.Repositories
{
    public class PlantRepository
    {
        private readonly WMSDbContext _ActiveDbContext;
        private readonly APIHeader _APIHeader;

        public PlantRepository(WMSDbContext ActiveDbContext, APIHeader Header)
        {
            _ActiveDbContext = ActiveDbContext;
            _APIHeader = Header;
        }

        public bool CreatePlant(CMMDP entity, out string message)
        {
            message = "";

            using (var db = _ActiveDbContext)
            {
                var plant = db.CMMDP.FirstOrDefault(x => x.MDP001 == entity.MDP001);
                if (plant != null)
                {
                    message = "entity already exists.";
                    return false;
                }

                db.CMMDP.Add(entity);
                db.SaveChanges();
                return true;
            }
        }

        public bool UpdatePlant(CMMDP entity, out string message)
        {
            message = "";

            using (var db = _ActiveDbContext)
            {
                var plant = db.CMMDP.FirstOrDefault(x => x.MDP001 == entity.MDP001 );
                if (plant == null)
                {
                    message = "entity does not exist.";
                    return false;
                }

                var local = db.Set<CMMDP>().Local.FirstOrDefault(x => x.MDP001 == entity.MDP001);

                if (local != null)
                {
                    db.Entry(local).State = EntityState.Detached;
                }

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
        }

        public List<dynamic> GetPlant(string fields, string sort, int page, int limit)
        {
            using (var db = _ActiveDbContext)
            {
                var query = db.CMMDP.AsEnumerable();

                if (!string.IsNullOrEmpty(sort))
                {
                    query = query.OrderBy(sort);
                }

                if (page > -1)
                {
                    int excludedRows = (page - 1) * limit;
                    query = query.Skip(excludedRows).Take(limit);
                }

                if (!string.IsNullOrEmpty(fields))
                {
                    return query.Select(string.Format("new({0})", fields)).Cast<dynamic>().ToList();
                }

                return query.ToList<dynamic>(); ;
            }
        }

        public dynamic GetPlantById(string id, string fields)
        {
            using (var db = _ActiveDbContext)
            {
                var query = db.CMMDP.AsEnumerable();
                query = query.Where(x => x.MDP001 == id);

                if (!string.IsNullOrEmpty(fields))
                {
                    return query.Select(string.Format("new({0})", fields)).Cast<dynamic>().FirstOrDefault();
                }

                return query.FirstOrDefault<dynamic>();
            }
        }

        public bool DeletePlant(string id)
        {
            using (var db = _ActiveDbContext)
            {
                var plant = db.CMMDP.SingleOrDefault(x => x.MDP001 == id);

                if (plant == null)
                    return false;

                db.CMMDP.Remove(plant);
                db.SaveChanges();

                return true;
            }
        }
    }
}