using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IScheduleDAL
    {
        bool ImportFile(string path, Schedule schedule);

        List<Schedule> SearchSchedule(string month);

        void DelSchedule(string id);
    }
}