using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class Substitutes_DLogic
    {
        ISubstitutes_DDAL objDAL = new Substitutes_DDAL();

        public List<Substitutes_D> SearchSubstitutes_D(Substitutes_D substitutes_D)
        {
            return objDAL.SearchSubstitutes_D(substitutes_D);
        }
    }
}