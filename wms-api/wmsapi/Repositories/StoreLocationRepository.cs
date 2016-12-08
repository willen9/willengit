using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using wmsapi.Models;

namespace wmsapi.Repositories
{
    public class StoreLocationRepository
    {
        private readonly WMSDbContext _ActiveDbContext;
        private readonly APIHeader _APIHeader;

        public StoreLocationRepository(WMSDbContext ActiveDbContext, APIHeader Header)
        {
            _ActiveDbContext = ActiveDbContext;
            _APIHeader = Header;
        }

        public bool CreateStoreLocation(CMMDQ entity, out string message)
        {
            message = "";

            using (var db = _ActiveDbContext)
            {
                var storeLocation = db.CMMDQ.FirstOrDefault(x => x.MDQ001 == entity.MDQ001 && x.MDQ002 == entity.MDQ002);
                if (storeLocation != null)
                {
                    message = "entity already exists.";
                    return false;
                }

                db.CMMDQ.Add(entity);
                db.SaveChanges();
                return true;
            }
        }

        public bool UpdateStoreLocation(CMMDQ entity, out string message)
        {
            message = "";

            using (var db = _ActiveDbContext)
            {
                var storeLocation = db.CMMDQ.FirstOrDefault(x => x.MDQ001 == entity.MDQ001 && x.MDQ002 == entity.MDQ002);
                if (storeLocation == null)
                {
                    message = "entity does not exist.";
                    return false;
                }

                var local = db.Set<CMMDQ>().Local.FirstOrDefault(x => x.MDQ001 == entity.MDQ001 && x.MDQ002 == entity.MDQ002);

                if (local != null)
                {
                    db.Entry(local).State = EntityState.Detached;
                }

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
        }

        public List<dynamic> GetStoreLocation(string fields, string sort, int page, int limit)
        {
            using (var db = _ActiveDbContext)
            {
                var query = db.CMMDQ.AsEnumerable();

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

        public dynamic GetStoreLocationById(string pk1, string pk2, string fields)
        {
            using (var db = _ActiveDbContext)
            {
                var query = db.CMMDQ.AsEnumerable();
                query = query.Where(x => x.MDQ001 == pk1 && x.MDQ002 == pk2);

                if (!string.IsNullOrEmpty(fields))
                {
                    return query.Select(string.Format("new({0})", fields)).Cast<dynamic>().FirstOrDefault();
                }

                return query.FirstOrDefault<dynamic>();
            }
        }

        public bool DeleteStoreLocation(string pk1, string pk2)
        {
            using (var db = _ActiveDbContext)
            {
                var storeLocation = db.CMMDQ.SingleOrDefault(x => x.MDQ001 == pk1 && x.MDQ002 == pk2);

                if (storeLocation == null)
                    return false;

                db.CMMDQ.Remove(storeLocation);
                db.SaveChanges();

                return true;
            }
        }
    }
}