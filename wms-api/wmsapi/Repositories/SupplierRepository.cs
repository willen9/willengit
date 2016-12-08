using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using wmsapi.Models;

namespace wmsapi.Repositories
{
    public class SupplierRepository
    {
        private readonly WMSDbContext _ActiveDbContext;
        private readonly APIHeader _APIHeader;

        public SupplierRepository(WMSDbContext ActiveDbContext, APIHeader Header)
        {
            _ActiveDbContext = ActiveDbContext;
            _APIHeader = Header;
        }

        public bool CreateSupplier(CMMDI entity, out string message)
        {
            message = "";

            using (var db = _ActiveDbContext)
            {
                var supplier = db.CMMDI.FirstOrDefault(x => x.MDI001 == entity.MDI001);
                if (supplier != null)
                {
                    message = "entity already exists.";
                    return false;
                }

                db.CMMDI.Add(entity);
                db.SaveChanges();
                return true;
            }
        }

        public bool UpdateSupplier(CMMDI entity, out string message)
        {
            message = "";

            using (var db = _ActiveDbContext)
            {
                var supplier = db.CMMDI.FirstOrDefault(x => x.MDI001 == entity.MDI001);
                if (supplier == null)
                {
                    message = "entity does not exist.";
                    return false;
                }
               
                var local = db.Set<CMMDI>().Local.FirstOrDefault(x => x.MDI001 == entity.MDI001);

                if (local != null)
                {
                    db.Entry(local).State = EntityState.Detached;
                }

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
        }

        public List<dynamic> GetSupplier(string fields, string sort, int page, int limit)
        {
            using (var db = _ActiveDbContext)
            {
                var query = db.CMMDI.AsEnumerable();
           
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

        public dynamic GetSupplierById(string id, string fields)
        {
            using (var db = _ActiveDbContext)
            {
                var query = db.CMMDI.AsEnumerable();
                query = query.Where(x => x.MDI001 == id);

                if (!string.IsNullOrEmpty(fields))
                {
                    return query.Select(string.Format("new({0})", fields)).Cast<dynamic>().FirstOrDefault();
                }

                return query.FirstOrDefault<dynamic>();
            }
        }

        public bool DeleteSupplier(string id)
        {
            using (var db = _ActiveDbContext)
            {
                var supplier = db.CMMDI.SingleOrDefault(x => x.MDI001 == id);

                if (supplier == null)
                    return false;

                db.CMMDI.Remove(supplier);
                db.SaveChanges();

                return true;
            }
        }

        //Category Operations
        public bool CreateCategories(CMMDH entity, out string message)
        {
            message = "";

            using (var db = _ActiveDbContext)
            {
                var category = db.CMMDH.FirstOrDefault(x => x.MDH001 == entity.MDH001);
                if (category != null)
                {
                    message = "entity already exists.";
                    return false;
                }

                db.CMMDH.Add(entity);
                db.SaveChanges();
                return true;
            }
        }

        public bool UpdateCategories(CMMDH entity, out string message)
        {
            message = "";

            using (var db = _ActiveDbContext)
            {
                var category = db.CMMDH.FirstOrDefault(x => x.MDH001 == entity.MDH001);
                if (category == null)
                {
                    message = "entity does not exist.";
                    return false;
                }

                var local = db.Set<CMMDH>().Local.FirstOrDefault(x => x.MDH001 == entity.MDH001);

                if (local != null)
                {
                    db.Entry(local).State = EntityState.Detached;
                }

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
        }

        public List<dynamic> GetCategories(string fields, string sort, int page, int limit)
        {
            using (var db = _ActiveDbContext)
            {
                var query = db.CMMDH.AsEnumerable();

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

        public dynamic GetCategoriesById(string id, string fields)
        {
            using (var db = _ActiveDbContext)
            {
                var query = db.CMMDH.AsEnumerable();
                query = query.Where(x => x.MDH001 == id);

                if (!string.IsNullOrEmpty(fields))
                {
                    return query.Select(string.Format("new({0})", fields)).Cast<dynamic>().FirstOrDefault();
                }

                return query.FirstOrDefault<dynamic>();
            }
        }

        public bool DeleteCategories(string id)
        {
            using (var db = _ActiveDbContext)
            {
                var category = db.CMMDH.SingleOrDefault(x => x.MDH001 == id);

                if (category == null)
                    return false;

                db.CMMDH.Remove(category);
                db.SaveChanges();

                return true;
            }
        }
    }
}