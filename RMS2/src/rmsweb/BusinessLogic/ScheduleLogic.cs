using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class ScheduleLogic
    {
        IScheduleDAL objDAL = new ScheduleDAL();

        public bool ImportFile(string path, Schedule schedule)
        {
            return objDAL.ImportFile(path, schedule);
        }

        public List<Schedule> SearchSchedule(string month)
        {
            return objDAL.SearchSchedule(month);
        }

        public void DelSchedule(string id)
        {
            objDAL.DelSchedule(id);
        }
    }
}