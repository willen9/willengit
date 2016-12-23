using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class Position_HLogic
    {
        IPosition_HDAL objDAL = new Position_HDAL();

        public bool AddPosition_H(Position_H position_H, string strEmployeeId, string strRemark, out string msg)
        {
            return objDAL.AddPosition_H(position_H, strEmployeeId, strRemark, out msg);
        }

        public bool UpdatePosition_H(Position_H position_H, string strEmployeeId,
             string strRemark, out string msg)
        {
            return objDAL.UpdatePosition_H(position_H, strEmployeeId,
              strRemark, out msg);
        }

        public bool DelPosition_H(Position_H position_H, out string msg)
        {
            return objDAL.DelPosition_H(position_H, out msg);
        }

        public List<Position_H> SearchPosition_H(string col, string condition, string value)
        {
            return objDAL.SearchPosition_H(col, condition, value);
        }

        public Position_H GetPosition_HInfo(Position_H position_H)
        {
            return objDAL.GetPosition_HInfo(position_H);
        }

        public bool IsPositionNo(Position_H position_H)
        {
            return objDAL.IsPositionNo(position_H);
        }

        public bool ImportFile(string path)
        {
            return objDAL.ImportFile(path);
        }

    }
}