using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IPosition_HDAL
    {
        bool AddPosition_H(Position_H position_H, string strEmployeeId,
            string strRemark, out string msg);

        bool UpdatePosition_H(Position_H position_H, string strEmployeeId,
            string strRemark, out string msg);

        bool DelPosition_H(Position_H position_H, out string msg);

        List<Position_H> SearchPosition_H(string col, string condition, string value);

        Position_H GetPosition_HInfo(Position_H position_H);

        bool IsPositionNo(Position_H position_H);

        bool ImportFile(string path);

    }
}