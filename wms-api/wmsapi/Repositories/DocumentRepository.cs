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
    public class DocumentRepository
    {
        private readonly WMSDbContext _ActiveDbContext;
        private readonly APIHeader _APIHeader;

        public DocumentRepository(WMSDbContext ActiveDbContext, APIHeader Header)
        {
            _ActiveDbContext = ActiveDbContext;
            _APIHeader = Header;
        }

        public bool CreateDocument(CMMDX entity, out string message)
        {
            message = "";

            using (var db = _ActiveDbContext)
            {
                var department = db.CMMDX.FirstOrDefault(x => x.MDX001 == entity.MDX001);
                if (department != null)
                {
                    message = "entity already exists.";
                    return false;
                }

                db.CMMDX.Add(entity);
                db.SaveChanges();
                return true;
            }
        }

        public bool UpdateDocuments(CMMDX entity, out string message)
        {
            message = "";

            using (var db = _ActiveDbContext)
            {
                var department = db.CMMDX.FirstOrDefault(x => x.MDX001 == entity.MDX001);
                if (department == null)
                {
                    message = "entity does not exist.";
                    return false;
                }

                var local = db.Set<CMMDX>().Local.FirstOrDefault(x => x.MDX001 == entity.MDX001);

                if (local != null)
                {
                    db.Entry(local).State = EntityState.Detached;
                }

                db.Entry(entity).State = EntityState.Modified;                                
                db.SaveChanges();
                return true;
            }            
        }

        public List<dynamic> GetDocuments(string fields, string sort, int page, int limit)
        {
            using (var db = _ActiveDbContext)
            {
                var query = db.CMMDX.AsEnumerable();

                if (!string.IsNullOrEmpty(sort))
                {
                    query = query.OrderBy(sort);
                }

                if(page > -1)
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

        public dynamic GetDocumentById(string id, string fields)
        {
            using (var db = _ActiveDbContext)
            {
                var query = db.CMMDX.AsEnumerable();
                query = query.Where(x => x.MDX001 == id);

                if (!string.IsNullOrEmpty(fields))
                {
                    return query.Select(string.Format("new({0})", fields)).Cast<dynamic>().FirstOrDefault();
                }
                
                return query.FirstOrDefault<dynamic>();
            }
        }

        private object DocumentNumberLock = new object();
        public string GetDocumentNumber(string doctype)
        {
            lock(DocumentNumberLock)
            {
                using (var db = _ActiveDbContext)
                {
                    string basedDate = DateTime.Now.ToString("yyyyMMdd");

                    var query = db.SMSCA
                        .Where(x => x.SCA001 == doctype && x.SCA002.StartsWith(basedDate))
                        .Max(x => x.SCA002);

                    string Number = string.Empty;

                    if (query == null)
                        Number = basedDate + "00001";
                    else
                        Number = basedDate + (int.Parse(query.Substring(query.Length - 5, 5)) + 1).ToString("00000");

                    var entity = new SMSCA();
                    entity.SYSID = "";
                    entity.SCA001 = doctype;
                    entity.SCA002 = Number;
                    entity.CreatedDate = DateTime.Now;

                    db.SMSCA.Add(entity);
                    db.SaveChanges();

                    return Number;
                }
            }            
        }

        public List<dynamic> GetDocumentByCategory(string id, string fields)
        {
            using (var db = _ActiveDbContext)
            {
                var query = db.CMMDX.AsEnumerable();
                query = query.Where(x => x.MDX004 == id);

                if (!string.IsNullOrEmpty(fields))
                {
                    return query.Select(string.Format("new({0})", fields)).Cast<dynamic>().ToList();
                }

                return query.ToList<dynamic>();
            }
        }

        public bool DeleteDocument(string id)
        {
            using (var db = _ActiveDbContext)
            {
                var department = db.CMMDX.SingleOrDefault(x => x.MDX001 == id);

                if (department == null)
                    return false;

                db.CMMDX.Remove(department);
                db.SaveChanges();

                return true;
            }
        }

        public dynamic GetDocumentContent(string doctype, string number)
        {
            using(var db = _ActiveDbContext)
            {
                var header = db.MMDAA
                    .Where(x => x.DAA001 == doctype && x.DAA002 == number)
                    .FirstOrDefault();

                var xp = header as MME;           
                
                if (header == null)
                    return null;
                

                var items = db.MMDAB
                    .Where(x => x.DAB001 == doctype && x.DAB002 == number)
                    .ToList<MMDAB>();
                
                var fx = (MME)Convert.ChangeType(header, typeof(MME));

                var documentContent = new MME();
                documentContent = (MME)Convert.ChangeType(header, typeof(MME));
                documentContent.Items = items;
                
                return documentContent;
            }

            return null;
        }
    }
}