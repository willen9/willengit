using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class Position_DLogic
    {
        IPosition_DDAL objDAL = new Position_DDAL();

        public List<Position_D> SearchPosition_D(Position_D position_D)
        {
            return objDAL.SearchPosition_D(position_D);
        }

        public List<Position_D> Position_DList(string Col, string Condition, string conditionValue, int Page)
        {
            return objDAL.Position_DList(Col, Condition, conditionValue, Page);
        }
    }
}