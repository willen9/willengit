using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Http.ModelBinding;
using wmsapi.Models;

namespace wmsapi.Repositories
{
    public class CustomerRepository
    {
        private readonly WMSDbContext _ActiveDbContext;
        private readonly APIHeader _APIHeader;
       
        public CustomerRepository(WMSDbContext ActiveDbContext, APIHeader Header)
        {
            _ActiveDbContext = ActiveDbContext;
            _APIHeader = Header;
        }

        public bool CreateCustomer(CMMDG entity, out string message)
        {
            message = "";

            using (var db = _ActiveDbContext)
            {
                var customer = db.CMMDG.FirstOrDefault(x => x.MDG001 == entity.MDG001);
                if (customer != null)
                {
                    message = "entity already exists.";
                    return false;
                }

                db.CMMDG.Add(entity);
                db.SaveChanges();
                return true;
            }
        }

        public bool UpdateCustomers(CMMDG entity, out string message)
        {
            message = "";

            using (var db = _ActiveDbContext)
            {
                var customer = db.CMMDG.FirstOrDefault(x => x.MDG001 == entity.MDG001);
                if (customer == null)
                {
                    message = "entity does not exist.";
                    return false;
                }

                var local = db.Set<CMMDG>().Local.FirstOrDefault(x => x.MDG001 == entity.MDG001);

                if (local != null)
                {
                    db.Entry(local).State = EntityState.Detached;
                }

                db.Entry(entity).State = EntityState.Modified;                                
                db.SaveChanges();
                return true;
            }            
        }

        public List<dynamic> GetCustomers(string fields, string sort, int page, int limit)
        {
            using (var db = _ActiveDbContext)
            {
                var query = db.CMMDG.AsEnumerable();

                if (!string.IsNullOrEmpty(sort))

                if(page > -1)
                {
                {
                    query = query.OrderBy(sort);
                }
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

        public dynamic GetCustomerById(string id, string fields)
        {
            using (var db = _ActiveDbContext)
            {
                var query = db.CMMDG.AsEnumerable();
                query = query.Where(x => x.MDG001 == id);

                if (!string.IsNullOrEmpty(fields))
                {
                    return query.Select(string.Format("new({0})", fields)).Cast<dynamic>().FirstOrDefault();
                }
                
                return query.FirstOrDefault<dynamic>();
            }
        }

        public bool DeleteCustomer(string id)
        {
            using (var db = _ActiveDbContext)
            {
                var customer = db.CMMDG.SingleOrDefault(x => x.MDG001 == id);

                if (customer == null)
                    return false;

                db.CMMDG.Remove(customer);
                db.SaveChanges();

                return true;
            }
        }

        //Category Operations
        public bool CreateCategories(CMMDF entity, out string message)
        {
            message = "";

            using (var db = _ActiveDbContext)
            {
                var category = db.CMMDF.FirstOrDefault(x => x.MDF001 == entity.MDF001);
                if (category != null)
                {
                    message = "entity already exists.";
                    return false;
                }

                db.CMMDF.Add(entity);
                db.SaveChanges();
                return true;
            }
        }

        public bool UpdateCategories(CMMDF entity, out string message)
        {
            message = "";

            using (var db = _ActiveDbContext)
            {
                var category = db.CMMDF.FirstOrDefault(x => x.MDF001 == entity.MDF001);
                if (category == null)
                {
                    message = "entity does not exist.";
                    return false;
                }

                var local = db.Set<CMMDF>().Local.FirstOrDefault(x => x.MDF001 == entity.MDF001);

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
                var query = db.CMMDF.AsEnumerable();

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
                var query = db.CMMDF.AsEnumerable();
                query = query.Where(x => x.MDF001 == id);

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
                var category = db.CMMDF.SingleOrDefault(x => x.MDF001 == id);

                if (category == null)
                    return false;

                db.CMMDF.Remove(category);
                db.SaveChanges();

                return true;
            }
        }
    }
}