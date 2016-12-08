using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using wmsapi.Models;

namespace wmsapi.Repositories
{
    public class MaterialRepository
    {
        private readonly WMSDbContext _ActiveDbContext;
        private readonly APIHeader _APIHeader;

        public MaterialRepository(WMSDbContext ActiveDbContext, APIHeader Header)
        {
            _ActiveDbContext = ActiveDbContext;
            _APIHeader = Header;
        }

        public bool CreateMaterial(MMMDB entity, out string message)
        {
            message = "";

            using (var db = _ActiveDbContext)
            {
                var department = db.MMMDB.FirstOrDefault(x => x.MDB001 == entity.MDB001);
                if (department != null)
                {
                    message = "entity already exists.";
                    return false;
                }

                db.MMMDB.Add(entity);
                db.SaveChanges();
                return true;
            }
        }

        public bool UpdateMaterial(MMMDB entity, out string message)
        {
            message = "";

            using (var db = _ActiveDbContext)
            {
                var department = db.MMMDB.FirstOrDefault(x => x.MDB001 == entity.MDB001);
                if (department == null)
                {
                    message = "entity does not exist.";
                    return false;
                }

                var local = db.Set<MMMDB>().Local.FirstOrDefault(x => x.MDB001 == entity.MDB001);

                if (local != null)
                {
                    db.Entry(local).State = EntityState.Detached;
                }

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
        }

        public List<dynamic> GetMaterial(string fields, string sort, int page, int limit)
        {
            using (var db = _ActiveDbContext)
            {
                var query = db.MMMDB.AsEnumerable();

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

        public dynamic GetMaterialById(string id, string fields)
        {
            using (var db = _ActiveDbContext)
            {
                var query = db.MMMDB.AsEnumerable();
                query = query.Where(x => x.MDB001 == id);

                if (!string.IsNullOrEmpty(fields))
                {
                    return query.Select(string.Format("new({0})", fields)).Cast<dynamic>().FirstOrDefault();
                }

                return query.FirstOrDefault<dynamic>();
            }
        }

        public bool DeleteMaterial(string id)
        {
            using (var db = _ActiveDbContext)
            {
                var department = db.MMMDB.SingleOrDefault(x => x.MDB001 == id);

                if (department == null)
                    return false;

                db.MMMDB.Remove(department);
                db.SaveChanges();

                return true;
            }
        }
    }
}