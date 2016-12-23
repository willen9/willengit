using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IPosition_DDAL
    {
        List<Position_D> SearchPosition_D(Position_D position_D);

        List<Position_D> Position_DList(string Col, string Condition, string conditionValue, int Page);
    }
}