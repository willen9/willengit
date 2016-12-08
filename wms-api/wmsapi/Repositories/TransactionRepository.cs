using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Http.ModelBinding;
using wmsapi.Models;

namespace wmsapi.Repositories
{
    public class TransactionRepository
    {
        private readonly WMSDbContext _ActiveDbContext;

        public TransactionRepository(WMSDbContext ActiveDbContext)
        {
            _ActiveDbContext = ActiveDbContext;
        }

        public void CreateMMETransaction(MMDAA header, List<MMDAB> details)
        {
            using (var db = _ActiveDbContext)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    //SYSID,DAA001,DAA002,DAA003,DAA004,DAA005,DAA006,DAA007,DAA009,DAA010,DAA011,DAA015,DAA020,DAA030,DAA999,CreatedDate,Creator)VALUES(@SYSID,@DAA001,@DAA002,@DAA003,@DAA004,@DAA005,@DAA006,@DAA007,@DAA009,@DAA010,@DAA011,@DAA015,@DAA020,@DAA030,'N',getdate(),@Creator)
                    db.Database.ExecuteSqlCommand(@"INSERT INTO MMDAA(DAA001,DAA002,SYSID,DAA003,DAA004,DAA005,DAA006,DAA007,DAA008,DAA009,DAA010,DAA011,DAA015,DAA016,DAA020,DAA030,DAA041,DAA042,DAA800,DAA801,DAA851,DAA852,DAA854,DAA855,DAA857,DAA858,DAA992,DAA993,DAA994,DAA995,DAA996,DAA997,DAA998,DAA999,Creator,CreatedDate,Modifier,ModifiedDate)
                                                    VALUES(@DAA001,@DAA002,@SYSID,@DAA003,@DAA004, @DAA005, @DAA006, @DAA007, @DAA008, @DAA009, @DAA010, @DAA011, @DAA015, @DAA016, @DAA020, @DAA030, @DAA041, @DAA042, @DAA800, @DAA801, @DAA851, @DAA852, @DAA854, @DAA855, @DAA857, @DAA858, @DAA992, @DAA993, @DAA994, @DAA995, @DAA996, @DAA997, @DAA998, @DAA999, @Creator, @CreatedDate, @Modifier, @ModifiedDate)",
                        new SqlParameter("@DAA001", header.DAA001),
                        new SqlParameter("@DAA002", header.DAA002),
                        new SqlParameter("@SYSID", header.SYSID??""),
                        new SqlParameter("@DAA003", header.DAA003),
                        new SqlParameter("@DAA004", header.DAA004),
                        new SqlParameter("@DAA005", header.DAA005??""),
                        new SqlParameter("@DAA006", header.DAA006??""),
                        new SqlParameter("@DAA007", header.DAA007??""),
                        new SqlParameter("@DAA008", header.DAA008??""),
                        new SqlParameter("@DAA009", header.DAA009??""),
                        new SqlParameter("@DAA010", header.DAA010??""),
                        new SqlParameter("@DAA011", header.DAA011??""),
                        new SqlParameter("@DAA015", header.DAA015??0),
                        new SqlParameter("@DAA016", header.DAA016??0),
                        new SqlParameter("@DAA020", header.DAA020??""),
                        new SqlParameter("@DAA030", header.DAA030??""),
                        new SqlParameter("@DAA041", header.DAA041??0),
                        new SqlParameter("@DAA042", header.DAA042??0),
                        new SqlParameter("@DAA800", DBNull.Value),
                        new SqlParameter("@DAA801", DBNull.Value),
                        new SqlParameter("@DAA851", header.DAA851??""),
                        new SqlParameter("@DAA852", header.DAA852??""),
                        new SqlParameter("@DAA854", header.DAA854??""),
                        new SqlParameter("@DAA855", header.DAA855??""),
                        new SqlParameter("@DAA857", header.DAA857??""),
                        new SqlParameter("@DAA858", header.DAA858??""),
                        new SqlParameter("@DAA992", header.DAA992??""),
                        new SqlParameter("@DAA993", header.DAA993??0),
                        new SqlParameter("@DAA994", header.DAA994??0),
                        new SqlParameter("@DAA995", header.DAA995??""),
                        new SqlParameter("@DAA996", header.DAA996??""),
                        new SqlParameter("@DAA997", header.DAA997??""),
                        new SqlParameter("@DAA998", header.DAA998??""),
                        new SqlParameter("@DAA999", header.DAA999??""),
                        new SqlParameter("@Creator", header.Creator??""),
                        new SqlParameter("@CreatedDate", DateTime.Now),
                        new SqlParameter("@Modifier", DBNull.Value),
                        new SqlParameter("@ModifiedDate", DBNull.Value)
                        );

                    //INSERT INTO MMDAB(SYSID,DAB001,DAB002,DAB003,DAB004,DAB005,DAB007,DAB008,DAB009,DAB010,DAB011,DAB020,DAB021,DAB022,DAB026,DAB034,DAB035,DAB995,DAB999,CreatedDate,Creator)VALUES(@SYSID,@DAB001,@DAB002,@DAB003,@DAB004,@DAB005,@DAB007,@DAB008,@DAB009,@DAB010,@DAB011,@DAB020,@DAB021,@DAB022,@DAB026,@DAB034,@DAB035,@DAB995,'N',getdate(),@Creator)
                    foreach (MMDAB objDAB in details)
                    {
                        db.Database.ExecuteSqlCommand(@"INSERT INTO MMDAB(DAB001,DAB002,DAB003,SYSID,DAB004,DAB005,DAB006,DAB007,DAB008,DAB009,DAB010,DAB011,DAB020,DAB021,DAB022,DAB025,DAB026,DAB034,DAB035,DAB036,DAB051,DAB052,DAB053,DAB054,DAB800,DAB801,DAB851,DAB852,DAB853,DAB854,DAB855,DAB856,DAB857,DAB858,DAB859,DAB992,DAB993,DAB994,DAB995,DAB996,DAB997,DAB998,DAB999,Creator,CreatedDate,Modifier,ModifiedDate)
                                                        VALUES(@DAB001, @DAB002, @DAB003, @SYSID, @DAB004, @DAB005, @DAB006, @DAB007, @DAB008, @DAB009, @DAB010, @DAB011, @DAB020, @DAB021, @DAB022, @DAB025, @DAB026, @DAB034, @DAB035, @DAB036, @DAB051, @DAB052, @DAB053, @DAB054, @DAB800, @DAB801, @DAB851, @DAB852, @DAB853, @DAB854, @DAB855, @DAB856, @DAB857, @DAB858, @DAB859, @DAB992, @DAB993, @DAB994, @DAB995, @DAB996, @DAB997, @DAB998, @DAB999, @Creator, @CreatedDate, @Modifier, @ModifiedDate)",
                                                           new SqlParameter("@DAB001", objDAB.DAB001),
                                                           new SqlParameter("@DAB002", objDAB.DAB002),
                                                           new SqlParameter("@DAB003", objDAB.DAB003),
                                                           new SqlParameter("@SYSID", objDAB.SYSID??""),
                                                           new SqlParameter("@DAB004", objDAB.DAB004??""),
                                                           new SqlParameter("@DAB005", objDAB.DAB005??""),
                                                           new SqlParameter("@DAB006", objDAB.DAB006??""),
                                                           new SqlParameter("@DAB007", objDAB.DAB007??""),
                                                           new SqlParameter("@DAB008", objDAB.DAB008??""),
                                                           new SqlParameter("@DAB009", objDAB.DAB009??""),
                                                           new SqlParameter("@DAB010", objDAB.DAB010??""),
                                                           new SqlParameter("@DAB011", objDAB.DAB011??""),
                                                           new SqlParameter("@DAB020", objDAB.DAB020??""),
                                                           new SqlParameter("@DAB021", objDAB.DAB021??""),
                                                           new SqlParameter("@DAB022", objDAB.DAB022??""),
                                                           new SqlParameter("@DAB025", objDAB.DAB025??0),
                                                           new SqlParameter("@DAB026", objDAB.DAB026??0),
                                                           new SqlParameter("@DAB034", objDAB.DAB034??""),
                                                           new SqlParameter("@DAB035", objDAB.DAB035??""),
                                                           new SqlParameter("@DAB036", objDAB.DAB036??""),
                                                           new SqlParameter("@DAB051", objDAB.DAB051??""),
                                                           new SqlParameter("@DAB052", objDAB.DAB052??0),
                                                           new SqlParameter("@DAB053", objDAB.DAB053??0),
                                                           new SqlParameter("@DAB054", objDAB.DAB054??""),
                                                           new SqlParameter("@DAB800", DBNull.Value),
                                                           new SqlParameter("@DAB801", DBNull.Value),
                                                           new SqlParameter("@DAB851", objDAB.DAB851??""),
                                                           new SqlParameter("@DAB852", objDAB.DAB852??""),
                                                           new SqlParameter("@DAB853", objDAB.DAB853??""),
                                                           new SqlParameter("@DAB854", objDAB.DAB854??""),
                                                           new SqlParameter("@DAB855", objDAB.DAB855??""),
                                                           new SqlParameter("@DAB856", objDAB.DAB856??""),
                                                           new SqlParameter("@DAB857", objDAB.DAB857??""),
                                                           new SqlParameter("@DAB858", objDAB.DAB858??""),
                                                           new SqlParameter("@DAB859", objDAB.DAB859??""),
                                                           new SqlParameter("@DAB992", objDAB.DAB992??""),
                                                           new SqlParameter("@DAB993", objDAB.DAB993??0),
                                                           new SqlParameter("@DAB994", objDAB.DAB994??0),
                                                           new SqlParameter("@DAB995", objDAB.DAB995??""),
                                                           new SqlParameter("@DAB996", objDAB.DAB996??""),
                                                           new SqlParameter("@DAB997", objDAB.DAB997??""),
                                                           new SqlParameter("@DAB998", objDAB.DAB998??""),
                                                           new SqlParameter("@DAB999", objDAB.DAB999??""),
                                                           new SqlParameter("@Creator", objDAB.Creator??""),
                                                           new SqlParameter("@CreatedDate", DateTime.Now),
                                                           new SqlParameter("@Modifier", DBNull.Value),
                                                           new SqlParameter("@ModifiedDate", DBNull.Value)
                                                        );
                    }

                    transaction.Commit();
                }
            }
        }

        public void CreateMMBTransaction(MMDCA header, List<MMDCB> details)
        {
            using (var db = _ActiveDbContext)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.Database.ExecuteSqlCommand(@"INSERT INTO MMDCA(SYSID,DCA001,DCA002,DCA003,DCA004,DCA005,DCA006,DCA015,DCA020,DCA030,DCA995,DCA999,CreatedDate,Creator)
                                                    VALUES (@SYSID,@DCA001,@DCA002,@DCA003,@DCA004,@DCA005,@DCA006,@DCA015,@DCA020,@DCA030,@DCA995,'N',getdate(),@Creator)",
                        new SqlParameter("@SYSID", header.SYSID ?? ""),
                        new SqlParameter("@DCA001", header.DCA001),
                        new SqlParameter("@DCA002", header.DCA002),
                        new SqlParameter("@DCA003", header.DCA003),
                        new SqlParameter("@DCA004", header.DCA004),
                        new SqlParameter("@DCA005", header.DCA005 ?? ""),
                        new SqlParameter("@DCA006", header.DCA006 ?? ""),
                        new SqlParameter("@DCA015", header.DCA015 ?? 0),
                        new SqlParameter("@DCA020", header.DCA020 ?? ""),
                        new SqlParameter("@DCA030", header.DCA030 ?? ""),
                        new SqlParameter("@DCA995", header.DCA995 ?? ""),
                        new SqlParameter("@Creator", header.Creator ?? "")
                        );

                    foreach (MMDCB objDCB in details)
                    {
                        db.Database.ExecuteSqlCommand(@"INSERT INTO MMDCB(SYSID,DCB001,DCB002,DCB003,DCB004,DCB005,DCB007,DCB008,DCB009,DCB010,DCB011,DCB020,DCB021,DCB022,DCB026,DCB034,DCB035,DCB851,DCB852,DCB853,DCB995,DCB999,CreatedDate,Creator)
                                                        VALUES (@SYSID,@DCB001,@DCB002,@DCB003,@DCB004,@DCB005,@DCB007,@DCB008,@DCB009,@DCB010,@DCB011,@DCB020,@DCB021,@DCB022,@DCB026,@DCB034,@DCB035,@DCB851,@DCB852,@DCB853,@DCB995,'N',getdate(),@Creator)",
                                                           new SqlParameter("@SYSID", objDCB.SYSID ?? ""),
                                                           new SqlParameter("@DCB001", objDCB.DCB001),
                                                           new SqlParameter("@DCB002", objDCB.DCB002),
                                                           new SqlParameter("@DCB003", objDCB.DCB003),
                                                           new SqlParameter("@DCB004", objDCB.DCB004 ?? ""),
                                                           new SqlParameter("@DCB005", objDCB.DCB005 ?? ""),
                                                           new SqlParameter("@DCB007", objDCB.DCB007 ?? ""),
                                                           new SqlParameter("@DCB008", objDCB.DCB008 ?? ""),
                                                           new SqlParameter("@DCB009", objDCB.DCB009 ?? ""),
                                                           new SqlParameter("@DCB010", objDCB.DCB010 ?? ""),
                                                           new SqlParameter("@DCB011", objDCB.DCB011 ?? ""),
                                                           new SqlParameter("@DCB020", objDCB.DCB020 ?? ""),
                                                           new SqlParameter("@DCB021", objDCB.DCB021 ?? ""),
                                                           new SqlParameter("@DCB022", objDCB.DCB022 ?? ""),
                                                           new SqlParameter("@DCB026", objDCB.DCB026 ?? 0),
                                                           new SqlParameter("@DCB034", objDCB.DCB034 ?? ""),
                                                           new SqlParameter("@DCB035", objDCB.DCB035 ?? ""),
                                                           new SqlParameter("@DCB851", objDCB.DCB851 ?? ""),
                                                           new SqlParameter("@DCB852", objDCB.DCB852 ?? ""),
                                                           new SqlParameter("@DCB853", objDCB.DCB853 ?? ""),
                                                           new SqlParameter("@DCB995", objDCB.DCB995 ?? ""),
                                                           new SqlParameter("@Creator", objDCB.Creator ?? "")
                                                        );
                    }

                    transaction.Commit();
                }
            }
        }

        public void CreateMMDTransaction(MMDEA header, List<MMDEB> details)
        {
            using (var db = _ActiveDbContext)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.Database.ExecuteSqlCommand(@"INSERT INTO MMDEA(SYSID,DEA001,DEA002,DEA003,DEA004,DEA005,DEA006,DEA015,DEA020,DEA030,DEA999,CreatedDate,Creator)
                                                    VALUES (@SYSID,@DEA001,@DEA002,@DEA003,@DEA004,@DEA005,@DEA006,@DEA015,@DEA020,@DEA030,'N',getdate(),@Creator)",
                        new SqlParameter("@SYSID", header.SYSID ?? ""),
                        new SqlParameter("@DEA001", header.DEA001),
                        new SqlParameter("@DEA002", header.DEA002),
                        new SqlParameter("@DEA003", header.DEA003),
                        new SqlParameter("@DEA004", header.DEA004),
                        new SqlParameter("@DEA005", header.DEA005 ?? ""),
                        new SqlParameter("@DEA006", header.DEA006 ?? ""),
                        new SqlParameter("@DEA015", header.DEA015 ?? 0),
                        new SqlParameter("@DEA020", header.DEA020 ?? ""),
                        new SqlParameter("@DEA030", header.DEA030 ?? ""),
                        new SqlParameter("@Creator", header.Creator ?? "")
                        );

                    foreach (MMDEB objDEB in details)
                    {
                        db.Database.ExecuteSqlCommand(@"INSERT INTO MMDEB(SYSID,DEB001,DEB002,DEB003,DEB004,DEB005,DEB007,DEB008,DEB011,DEB012,DEB013,DEB016,DEB017,DEB018,DEB020,DEB021,DEB022,DEB026,DEB034,DEB999,CreatedDate,Creator)
                                                        VALUES (@SYSID,@DEB001,@DEB002,@DEB003,@DEB004,@DEB005,@DEB007,@DEB008,@DEB011,@DEB012,@DEB013,@DEB016,@DEB017,@DEB018,@DEB020,@DEB021,@DEB022,@DEB026,@DEB034,'N',getdate(),@Creator)",
                                                           new SqlParameter("@SYSID", objDEB.SYSID ?? ""),
                                                           new SqlParameter("@DEB001", objDEB.DEB001),
                                                           new SqlParameter("@DEB002", objDEB.DEB002),
                                                           new SqlParameter("@DEB003", objDEB.DEB003),
                                                           new SqlParameter("@DEB004", objDEB.DEB004 ?? ""),
                                                           new SqlParameter("@DEB005", objDEB.DEB005 ?? ""),
                                                           new SqlParameter("@DEB007", objDEB.DEB007 ?? ""),
                                                           new SqlParameter("@DEB008", objDEB.DEB008 ?? ""),
                                                           new SqlParameter("@DEB011", objDEB.DEB011 ?? ""),
                                                           new SqlParameter("@DEB012", objDEB.DEB012 ?? ""),
                                                           new SqlParameter("@DEB013", objDEB.DEB013 ?? ""),
                                                           new SqlParameter("@DEB016", objDEB.DEB016 ?? ""),
                                                           new SqlParameter("@DEB017", objDEB.DEB017 ?? ""),
                                                           new SqlParameter("@DEB018", objDEB.DEB018 ?? ""),
                                                           new SqlParameter("@DEB020", objDEB.DEB020 ?? ""),
                                                           new SqlParameter("@DEB021", objDEB.DEB021 ?? ""),
                                                           new SqlParameter("@DEB022", objDEB.DEB022 ?? ""),
                                                           new SqlParameter("@DEB026", objDEB.DEB026 ?? 0),
                                                           new SqlParameter("@DEB034", objDEB.DEB034 ?? ""),
                                                           new SqlParameter("@Creator", objDEB.Creator ?? "")
                                                        );
                    }

                    transaction.Commit();
                }
            }
        }

        public void CreateMMFTransaction(MMDLA header, List<MMDLB> details)
        {
            using (var db = _ActiveDbContext)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.Database.ExecuteSqlCommand(@"INSERT INTO MMDLA(SYSID,DLA001,DLA002,DLA003,DLA004,DLA005,DLA006,DLA007,DLA009,DLA010,DLA011,DLA015,DLA020,DLA030,DLA999,CreatedDate,Creator)
                                                    VALUES (@SYSID,@DLA001,@DLA002,@DLA003,@DLA004,@DLA005,@DLA006,@DLA007,@DLA009,@DLA010,@DLA011,@DLA015,@DLA020,@DLA030,'N',getdate(),@Creator)",
                        new SqlParameter("@SYSID", header.SYSID ?? ""),
                        new SqlParameter("@DLA001", header.DLA001),
                        new SqlParameter("@DLA002", header.DLA002),
                        new SqlParameter("@DLA003", header.DLA003),
                        new SqlParameter("@DLA004", header.DLA004),
                        new SqlParameter("@DLA005", header.DLA005 ?? ""),
                        new SqlParameter("@DLA006", header.DLA006 ?? ""),
                        new SqlParameter("@DLA007", header.DLA007 ?? ""),
                        new SqlParameter("@DLA009", header.DLA009 ?? ""),
                        new SqlParameter("@DLA010", header.DLA010 ?? ""),
                        new SqlParameter("@DLA011", header.DLA011 ?? ""),
                        new SqlParameter("@DLA015", header.DLA015 ?? 0),
                        new SqlParameter("@DLA020", header.DLA020 ?? ""),
                        new SqlParameter("@DLA030", header.DLA030 ?? ""),
                        new SqlParameter("@Creator", header.Creator ?? "")
                        );

                    foreach (MMDLB objDLB in details)
                    {
                        db.Database.ExecuteSqlCommand(@"INSERT INTO MMDLB(SYSID,DLB001,DLB002,DLB003,DLB004,DLB005,DLB007,DLB008,DLB009,DLB010,DLB011,DLB020,DLB021,DLB022,DLB026,DLB034,DLB035,DLB999,CreatedDate,Creator)
                                                        VALUES (@SYSID,@DLB001,@DLB002,@DLB003,@DLB004,@DLB005,@DLB007,@DLB008,@DLB009,@DLB010,@DLB011,@DLB020,@DLB021,@DLB022,@DLB026,@DLB034,@DLB035,'N',getdate(),@Creator)",
                                                           new SqlParameter("@SYSID", objDLB.SYSID ?? ""), 
                                                           new SqlParameter("@DLB001", objDLB.DLB001),
                                                           new SqlParameter("@DLB002", objDLB.DLB002),
                                                           new SqlParameter("@DLB003", objDLB.DLB003),
                                                           new SqlParameter("@DLB004", objDLB.DLB004 ?? ""),
                                                           new SqlParameter("@DLB005", objDLB.DLB005 ?? ""),
                                                           new SqlParameter("@DLB007", objDLB.DLB007 ?? ""),
                                                           new SqlParameter("@DLB008", objDLB.DLB008 ?? ""),
                                                           new SqlParameter("@DLB009", objDLB.DLB009 ?? ""),
                                                           new SqlParameter("@DLB010", objDLB.DLB010 ?? ""),
                                                           new SqlParameter("@DLB011", objDLB.DLB011 ?? ""),
                                                           new SqlParameter("@DLB020", objDLB.DLB020 ?? ""),
                                                           new SqlParameter("@DLB021", objDLB.DLB021 ?? ""),
                                                           new SqlParameter("@DLB022", objDLB.DLB022 ?? ""),
                                                           new SqlParameter("@DLB026", objDLB.DLB026 ?? 0),
                                                           new SqlParameter("@DLB034", objDLB.DLB034 ?? ""),
                                                           new SqlParameter("@DLB035", objDLB.DLB035 ?? ""),
                                                           new SqlParameter("@Creator", objDLB.Creator ?? "")
                                                        );
                    }

                    transaction.Commit();
                }
            }
        }

        public void CreateMMGTransaction(MMDGA header, List<MMDGB> details)
        {
            using (var db = _ActiveDbContext)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.Database.ExecuteSqlCommand(@"INSERT INTO MMDGA(SYSID,DGA001,DGA002,DGA003,DGA004,DGA005,DGA006,DGA015,DGA020,DGA030,DGA999,CreatedDate,Creator)
                                                    VALUES (@SYSID,@DGA001,@DGA002,@DGA003,@DGA004,@DGA005,@DGA006,@DGA015,@DGA020,@DGA030,'N',getdate(),@Creator)",
                        new SqlParameter("@SYSID", header.SYSID ?? ""),
                        new SqlParameter("@DGA001", header.DGA001),
                        new SqlParameter("@DGA002", header.DGA002),
                        new SqlParameter("@DGA003", header.DGA003),
                        new SqlParameter("@DGA004", header.DGA004),
                        new SqlParameter("@DGA005", header.DGA005 ?? ""),
                        new SqlParameter("@DGA006", header.DGA006 ?? ""),
                        new SqlParameter("@DGA015", header.DGA015 ?? 0),
                        new SqlParameter("@DGA020", header.DGA020 ?? ""),
                        new SqlParameter("@DGA030", header.DGA030 ?? ""),
                        new SqlParameter("@Creator", header.Creator ?? "")
                        );

                    foreach (MMDGB objDGB in details)
                    {
                        db.Database.ExecuteSqlCommand(@"INSERT INTO MMDGB(SYSID,DGB001,DGB002,DGB003,DGB004,DGB005,DGB007,DGB008,DGB009,DGB010,DGB011,DGB020,DGB021,DGB022,DGB026,DGB034,DGB035,DGB995,DGB999,CreatedDate,Creator)
                                                        VALUES (@SYSID,@DGB001,@DGB002,@DGB003,@DGB004,@DGB005,@DGB007,@DGB008,@DGB009,@DGB010,@DGB011,@DGB020,@DGB021,@DGB022,@DGB026,@DGB034,@DGB035,@DGB995,'N',getdate(),@Creator)",
                                                           new SqlParameter("@SYSID", objDGB.SYSID ?? ""),
                                                           new SqlParameter("@DGB001", objDGB.DGB001),
                                                           new SqlParameter("@DGB002", objDGB.DGB002),
                                                           new SqlParameter("@DGB003", objDGB.DGB003),
                                                           new SqlParameter("@DGB004", objDGB.DGB004 ?? ""),
                                                           new SqlParameter("@DGB005", objDGB.DGB005 ?? ""),
                                                           new SqlParameter("@DGB007", objDGB.DGB007 ?? ""),
                                                           new SqlParameter("@DGB008", objDGB.DGB008 ?? ""),
                                                           new SqlParameter("@DGB009", objDGB.DGB009 ?? ""),
                                                           new SqlParameter("@DGB010", objDGB.DGB010 ?? ""),
                                                           new SqlParameter("@DGB011", objDGB.DGB011 ?? ""),
                                                           new SqlParameter("@DGB020", objDGB.DGB020 ?? ""),
                                                           new SqlParameter("@DGB021", objDGB.DGB021 ?? ""),
                                                           new SqlParameter("@DGB022", objDGB.DGB022 ?? ""),
                                                           new SqlParameter("@DGB026", objDGB.DGB026 ?? 0),
                                                           new SqlParameter("@DGB034", objDGB.DGB034 ?? ""),
                                                           new SqlParameter("@DGB035", objDGB.DGB035 ?? ""),
                                                           new SqlParameter("@DGB995", objDGB.DGB995 ?? ""),
                                                           new SqlParameter("@Creator", objDGB.Creator ?? "")
                                                        );
                    }

                    transaction.Commit();
                }
            }
        }

        public void CreateMMHTransaction(MMDHA header, List<MMDHB> details)
        {
            using (var db = _ActiveDbContext)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.Database.ExecuteSqlCommand(@"INSERT INTO MMDHA(SYSID,DHA001,DHA002,DHA003,DHA004,DHA005,DHA006,DHA015,DHA020,DHA030,DHA999,CreatedDate,Creator)
                                                    VALUES (@SYSID,@DHA001,@DHA002,@DHA003,@DHA004,@DHA005,@DHA006,@DHA015,@DHA020,@DHA030,'N',getdate(),@Creator)",
                        new SqlParameter("@SYSID", header.SYSID ?? ""),
                        new SqlParameter("@DHA001", header.DHA001),
                        new SqlParameter("@DHA002", header.DHA002),
                        new SqlParameter("@DHA003", header.DHA003),
                        new SqlParameter("@DHA004", header.DHA004),
                        new SqlParameter("@DHA005", header.DHA005 ?? ""),
                        new SqlParameter("@DHA006", header.DHA006 ?? ""),
                        new SqlParameter("@DHA015", header.DHA015 ?? 0),
                        new SqlParameter("@DHA020", header.DHA020 ?? ""),
                        new SqlParameter("@DHA030", header.DHA030 ?? ""),
                        new SqlParameter("@Creator", header.Creator ?? "")
                        );

                    foreach (MMDHB objDHB in details)
                    {
                        db.Database.ExecuteSqlCommand(@"INSERT INTO MMDHB(SYSID,DHB001,DHB002,DHB003,DHB004,DHB005,DHB007,DHB008,DHB009,DHB010,DHB011,DHB020,DHB021,DHB022,DHB026,DHB034,DHB035,DHB995,DHB999,CreatedDate,Creator)
                                                        VALUES (@SYSID,@DHB001,@DHB002,@DHB003,@DHB004,@DHB005,@DHB007,@DHB008,@DHB009,@DHB010,@DHB011,@DHB020,@DHB021,@DHB022,@DHB026,@DHB034,@DHB035,@DHB995,'N',getdate(),@Creator)",
                                                           new SqlParameter("@SYSID", objDHB.SYSID ?? ""),
                                                           new SqlParameter("@DHB001", objDHB.DHB001),
                                                           new SqlParameter("@DHB002", objDHB.DHB002),
                                                           new SqlParameter("@DHB003", objDHB.DHB003),
                                                           new SqlParameter("@DHB004", objDHB.DHB004 ?? ""),
                                                           new SqlParameter("@DHB005", objDHB.DHB005 ?? ""),
                                                           new SqlParameter("@DHB007", objDHB.DHB007 ?? ""),
                                                           new SqlParameter("@DHB008", objDHB.DHB008 ?? ""),
                                                           new SqlParameter("@DHB009", objDHB.DHB009 ?? ""),
                                                           new SqlParameter("@DHB010", objDHB.DHB010 ?? ""),
                                                           new SqlParameter("@DHB011", objDHB.DHB011 ?? ""),
                                                           new SqlParameter("@DHB020", objDHB.DHB020 ?? ""),
                                                           new SqlParameter("@DHB021", objDHB.DHB021 ?? ""),
                                                           new SqlParameter("@DHB022", objDHB.DHB022 ?? ""),
                                                           new SqlParameter("@DHB026", objDHB.DHB026 ?? 0),
                                                           new SqlParameter("@DHB034", objDHB.DHB034 ?? ""),
                                                           new SqlParameter("@DHB035", objDHB.DHB035 ?? ""),
                                                           new SqlParameter("@DHB995", objDHB.DHB995 ?? ""),
                                                           new SqlParameter("@Creator", objDHB.Creator ?? "")
                                                        );
                    }

                    transaction.Commit();
                }
            }
        }

        public void CreateMMITransaction(MMDKA header, List<MMDKB> details)
        {
            using (var db = _ActiveDbContext)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.Database.ExecuteSqlCommand(@"INSERT INTO MMDKA(SYSID,DKA001,DKA002,DKA003,DKA004,DKA005,DKA006,DKA015,DKA020,DKA030,DKA999,CreatedDate,Creator)
                                                    VALUES (@SYSID,@DKA001,@DKA002,@DKA003,@DKA004,@DKA005,@DKA006,@DKA015,@DKA020,@DKA030,'N',getdate(),@Creator)",
                        new SqlParameter("@SYSID", header.SYSID ?? ""),
                        new SqlParameter("@DKA001", header.DKA001),
                        new SqlParameter("@DKA002", header.DKA002),
                        new SqlParameter("@DKA003", header.DKA003),
                        new SqlParameter("@DKA004", header.DKA004),
                        new SqlParameter("@DKA005", header.DKA005 ?? ""),
                        new SqlParameter("@DKA006", header.DKA006 ?? ""),
                        new SqlParameter("@DKA015", header.DKA015 ?? 0),
                        new SqlParameter("@DKA020", header.DKA020 ?? ""),
                        new SqlParameter("@DKA030", header.DKA030 ?? ""),
                        new SqlParameter("@Creator", header.Creator ?? "")
                        );

                    foreach (MMDKB objDKB in details)
                    {
                        db.Database.ExecuteSqlCommand(@"INSERT INTO MMDKB(SYSID,DKB001,DKB002,DKB003,DKB004,DKB005,DKB007,DKB008,DKB009,DKB010,DKB011,DKB020,DKB021,DKB022,DKB026,DKB034,DKB035,DKB995,DKB999,CreatedDate,Creator)
                                                        VALUES (@SYSID,@DKB001,@DKB002,@DKB003,@DKB004,@DKB005,@DKB007,@DKB008,@DKB009,@DKB010,@DKB011,@DKB020,@DKB021,@DKB022,@DKB026,@DKB034,@DKB035,@DKB995,'N',getdate(),@Creator)",
                                                           new SqlParameter("@SYSID", objDKB.SYSID ?? ""),
                                                           new SqlParameter("@DKB001", objDKB.DKB001),
                                                           new SqlParameter("@DKB002", objDKB.DKB002),
                                                           new SqlParameter("@DKB003", objDKB.DKB003),
                                                           new SqlParameter("@DKB004", objDKB.DKB004 ?? ""),
                                                           new SqlParameter("@DKB005", objDKB.DKB005 ?? ""),
                                                           new SqlParameter("@DKB007", objDKB.DKB007 ?? ""),
                                                           new SqlParameter("@DKB008", objDKB.DKB008 ?? ""),
                                                           new SqlParameter("@DKB009", objDKB.DKB009 ?? ""),
                                                           new SqlParameter("@DKB010", objDKB.DKB010 ?? ""),
                                                           new SqlParameter("@DKB011", objDKB.DKB011 ?? ""),
                                                           new SqlParameter("@DKB020", objDKB.DKB020 ?? ""),
                                                           new SqlParameter("@DKB021", objDKB.DKB021 ?? ""),
                                                           new SqlParameter("@DKB022", objDKB.DKB022 ?? ""),
                                                           new SqlParameter("@DKB026", objDKB.DKB026 ?? 0),
                                                           new SqlParameter("@DKB034", objDKB.DKB034 ?? ""),
                                                           new SqlParameter("@DKB035", objDKB.DKB035 ?? ""),
                                                           new SqlParameter("@DKB995", objDKB.DKB995 ?? ""),
                                                           new SqlParameter("@Creator", objDKB.Creator ?? "")
                                                        );
                    }

                    transaction.Commit();
                }
            }
        }

        public void CreateMMJTransaction(MMDMA header, List<MMDMB> details)
        {
            using (var db = _ActiveDbContext)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.Database.ExecuteSqlCommand(@"INSERT INTO MMDMA(SYSID,DMA001,DMA002,DMA003,DMA004,DMA005,DMA006,DMA007,DMA010,DMA011,DMA015,DMA020,DMA030,DMA999,CreatedDate,Creator)
                                                    VALUES (@SYSID,@DMA001,@DMA002,@DMA003,@DMA004,@DMA005,@DMA006,@DMA007,@DMA010,@DMA011,@DMA015,@DMA020,@DMA030,'N',getdate(),@Creator)",
                        new SqlParameter("@SYSID", header.SYSID ?? ""),
                        new SqlParameter("@DMA001", header.DMA001),
                        new SqlParameter("@DMA002", header.DMA002),
                        new SqlParameter("@DMA003", header.DMA003),
                        new SqlParameter("@DMA004", header.DMA004),
                        new SqlParameter("@DMA005", header.DMA005 ?? ""),
                        new SqlParameter("@DMA006", header.DMA006 ?? ""),
                        new SqlParameter("@DMA007", header.DMA007 ?? ""),
                        new SqlParameter("@DMA010", header.DMA010 ?? ""),
                        new SqlParameter("@DMA011", header.DMA011 ?? ""),
                        new SqlParameter("@DMA015", header.DMA015 ?? 0),
                        new SqlParameter("@DMA020", header.DMA020 ?? ""),
                        new SqlParameter("@DMA030", header.DMA030 ?? ""),
                        new SqlParameter("@Creator", header.Creator ?? "")
                        );

                    foreach (MMDMB objDMB in details)
                    {
                        db.Database.ExecuteSqlCommand(@"INSERT INTO MMDMB(SYSID,DMB001,DMB002,DMB003,DMB004,DMB005,DMB007,DMB008,DMB009,DMB010,DMB011,DMB020,DMB021,DMB022,DMB026,DMB027,DMB034,DMB035,DMB999,CreatedDate,Creator)
                                                        VALUES (@SYSID,@DMB001,@DMB002,@DMB003,@DMB004,@DMB005,@DMB007,@DMB008,@DMB009,@DMB010,@DMB011,@DMB020,@DMB021,@DMB022,@DMB026,0,@DMB034,@DMB035,'N',getdate(),@Creator)",
                                                           new SqlParameter("@SYSID", objDMB.SYSID ?? ""),
                                                           new SqlParameter("@DMB001", objDMB.DMB001),
                                                           new SqlParameter("@DMB002", objDMB.DMB002),
                                                           new SqlParameter("@DMB003", objDMB.DMB003),
                                                           new SqlParameter("@DMB004", objDMB.DMB004 ?? ""),
                                                           new SqlParameter("@DMB005", objDMB.DMB005 ?? ""),
                                                           new SqlParameter("@DMB007", objDMB.DMB007 ?? ""),
                                                           new SqlParameter("@DMB008", objDMB.DMB008 ?? ""),
                                                           new SqlParameter("@DMB009", objDMB.DMB009 ?? ""),
                                                           new SqlParameter("@DMB010", objDMB.DMB010 ?? ""),
                                                           new SqlParameter("@DMB011", objDMB.DMB011 ?? ""),
                                                           new SqlParameter("@DMB020", objDMB.DMB020 ?? ""),
                                                           new SqlParameter("@DMB021", objDMB.DMB021 ?? ""),
                                                           new SqlParameter("@DMB022", objDMB.DMB022 ?? ""),
                                                           new SqlParameter("@DMB026", objDMB.DMB026 ?? 0),
                                                           new SqlParameter("@DMB034", objDMB.DMB034 ?? ""),
                                                           new SqlParameter("@DMB035", objDMB.DMB035 ?? ""),
                                                           new SqlParameter("@Creator", objDMB.Creator ?? "")
                                                        );
                    }

                    transaction.Commit();
                }
            }
        }

        public void CreateMMKTransaction(MMDNA header, List<MMDNB> details)
        {
            using (var db = _ActiveDbContext)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.Database.ExecuteSqlCommand(@"INSERT INTO MMDNA(SYSID,DNA001,DNA002,DNA003,DNA004,DNA005,DNA006,DNA010,DNA011,DNA015,DNA020,DNA030,DNA999,CreatedDate,Creator)
                                                    VALUES (@SYSID,@DNA001,@DNA002,@DNA003,@DNA004,@DNA005,@DNA006,@DNA010,@DNA011,@DNA015,@DNA020,@DNA030,'N',getdate(),@Creator)",
                        new SqlParameter("@SYSID", header.SYSID ?? ""),
                        new SqlParameter("@DNA001", header.DNA001),
                        new SqlParameter("@DNA002", header.DNA002),
                        new SqlParameter("@DNA003", header.DNA003),
                        new SqlParameter("@DNA004", header.DNA004),
                        new SqlParameter("@DNA005", header.DNA005 ?? ""),
                        new SqlParameter("@DNA006", header.DNA006 ?? ""),
                        new SqlParameter("@DNA010", header.DNA010 ?? ""),
                        new SqlParameter("@DNA011", header.DNA011 ?? ""),
                        new SqlParameter("@DNA015", header.DNA015 ?? 0),
                        new SqlParameter("@DNA020", header.DNA020 ?? ""),
                        new SqlParameter("@DNA030", header.DNA030 ?? ""),
                        new SqlParameter("@Creator", header.Creator ?? "")
                        );

                    foreach (MMDNB objDNB in details)
                    {
                        db.Database.ExecuteSqlCommand(@"INSERT INTO MMDNB(SYSID,DNB001,DNB002,DNB003,DNB004,DNB005,DNB007,DNB008,DNB009,DNB010,DNB011,DNB020,DNB021,DNB022,DNB026,DNB034,DNB035,DNB999,CreatedDate,Creator)
                                                        VALUES (@SYSID,@DNB001,@DNB002,@DNB003,@DNB004,@DNB005,@DNB007,@DNB008,@DNB009,@DNB010,@DNB011,@DNB020,@DNB021,@DNB022,@DNB026,@DNB034,@DNB035,'N',getdate(),@Creator)",
                                                           new SqlParameter("@SYSID", objDNB.SYSID ?? ""),
                                                           new SqlParameter("@DNB001", objDNB.DNB001),
                                                           new SqlParameter("@DNB002", objDNB.DNB002),
                                                           new SqlParameter("@DNB003", objDNB.DNB003),
                                                           new SqlParameter("@DNB004", objDNB.DNB004 ?? ""),
                                                           new SqlParameter("@DNB005", objDNB.DNB005 ?? ""),
                                                           new SqlParameter("@DNB007", objDNB.DNB007 ?? ""),
                                                           new SqlParameter("@DNB008", objDNB.DNB008 ?? ""),
                                                           new SqlParameter("@DNB009", objDNB.DNB009 ?? ""),
                                                           new SqlParameter("@DNB010", objDNB.DNB010 ?? ""),
                                                           new SqlParameter("@DNB011", objDNB.DNB011 ?? ""),
                                                           new SqlParameter("@DNB020", objDNB.DNB020 ?? ""),
                                                           new SqlParameter("@DNB021", objDNB.DNB021 ?? ""),
                                                           new SqlParameter("@DNB022", objDNB.DNB022 ?? ""),
                                                           new SqlParameter("@DNB026", objDNB.DNB026 ?? 0),
                                                           new SqlParameter("@DNB034", objDNB.DNB034 ?? ""),
                                                           new SqlParameter("@DNB035", objDNB.DNB035 ?? ""),
                                                           new SqlParameter("@Creator", objDNB.Creator ?? "")
                                                        );
                    }

                    transaction.Commit();
                }
            }
        }
    }
}