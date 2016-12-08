using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using wmsapi.Models;

namespace wmsapi.Repositories
{
    public class StoreBinRepository
    {
        private readonly WMSDbContext _ActiveDbContext;
        private readonly APIHeader _APIHeader;

        public StoreBinRepository(WMSDbContext ActiveDbContext, APIHeader Header)
        {
            _ActiveDbContext = ActiveDbContext;
            _APIHeader = Header;
        }

        public bool CreateStoreBin(CMMDR entity, out string message)
        {
            message = "";

            using (var db = _ActiveDbContext)
            {
                var storeBin = db.CMMDR.FirstOrDefault(x => x.MDR001 == entity.MDR001 && x.MDR002 == entity.MDR002&&x.MDR003==entity.MDR003);
                if (storeBin != null)
                {
                    message = "entity already exists.";
                    return false;
                }

                db.CMMDR.Add(entity);
                db.SaveChanges();
                return true;
            }
        }

        public bool UpdateStoreBin(CMMDR entity, out string message)
        {
            message = "";

            using (var db = _ActiveDbContext)
            {
                var storeBin = db.CMMDR.FirstOrDefault(x => x.MDR001 == entity.MDR001 && x.MDR002 == entity.MDR002 && x.MDR003 == entity.MDR003);
                if (storeBin == null)
                {
                    message = "entity does not exist.";
                    return false;
                }

                var local = db.Set<CMMDR>().Local.FirstOrDefault(x => x.MDR001 == entity.MDR001 && x.MDR002 == entity.MDR002 && x.MDR003 == entity.MDR003);

                if (local != null)
                {
                    db.Entry(local).State = EntityState.Detached;
                }

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
        }

        public List<dynamic> GetStoreBin(string fields, string sort, int page, int limit)
        {
            using (var db = _ActiveDbContext)
            {
                var query = db.CMMDR.AsEnumerable();

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

        public dynamic GetStoreBinById(string pk1, string pk2, string pk3,string fields)
        {
            using (var db = _ActiveDbContext)
            {
                var query = db.CMMDR.AsEnumerable();
                query = query.Where(x => x.MDR001 == pk1 && x.MDR002 == pk2&& x.MDR003 == pk3);

                if (!string.IsNullOrEmpty(fields))
                {
                    return query.Select(string.Format("new({0})", fields)).Cast<dynamic>().FirstOrDefault();
                }

                return query.FirstOrDefault<dynamic>();
            }
        }

        public bool DeleteStoreBin(string pk1, string pk2, string pk3)
        {
            using (var db = _ActiveDbContext)
            {
                var storeBin = db.CMMDR.SingleOrDefault(x => x.MDR001 == pk1 && x.MDR002 == pk2 && x.MDR003 == pk3);

                if (storeBin == null)
                    return false;

                db.CMMDR.Remove(storeBin);
                db.SaveChanges();

                return true;
            }
        }
    }
}