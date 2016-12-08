using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using wmsapi.Models;

namespace wmsapi.Repositories
{
    public class DepartmentRepository
    {
        private readonly WMSDbContext _ActiveDbContext;
        private readonly APIHeader _APIHeader;

        public DepartmentRepository(WMSDbContext ActiveDbContext, APIHeader Header)
        {
            _ActiveDbContext = ActiveDbContext;
            _APIHeader = Header;
        }

        public bool CreateDepartment(CMMDC entity, out string message)
        {
            message = "";

            using (var db = _ActiveDbContext)
            {
                var customer = db.CMMDC.FirstOrDefault(x => x.MDC001 == entity.MDC001);
                if (customer != null)
                {
                    message = "entity already exists.";
                    return false;
                }

                db.CMMDC.Add(entity);
                db.SaveChanges();
                return true;
            }
        }

        public bool UpdateDepartment(CMMDC entity, out string message)
        {
            message = "";

            using (var db = _ActiveDbContext)
            {
                var customer = db.CMMDC.FirstOrDefault(x => x.MDC001 == entity.MDC001);
                if (customer == null)
                {
                    message = "entity does not exist.";
                    return false;
                }

                var local = db.Set<CMMDC>().Local.FirstOrDefault(x => x.MDC001 == entity.MDC001);

                if (local != null)
                {
                    db.Entry(local).State = EntityState.Detached;
                }

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
        }

        public List<dynamic> GetDepartments(string fields, string sort, int page, int limit)
        {
            using (var db = _ActiveDbContext)
            {
                var query = db.CMMDC.AsEnumerable();

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

        public dynamic GetDepartmentById(string id, string fields)
        {
            using (var db = _ActiveDbContext)
            {
                var query = db.CMMDC.AsEnumerable();
                query = query.Where(x => x.MDC001 == id);

                if (!string.IsNullOrEmpty(fields))
                {
                    return query.Select(string.Format("new({0})", fields)).Cast<dynamic>().FirstOrDefault();
                }

                return query.FirstOrDefault<dynamic>();
            }
        }

        public bool DeleteDepartment(string id)
        {
            using (var db = _ActiveDbContext)
            {
                var customer = db.CMMDC.SingleOrDefault(x => x.MDC001 == id);

                if (customer == null)
                    return false;

                db.CMMDC.Remove(customer);
                db.SaveChanges();

                return true;
            }
        }
    }
}