using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface ISubstitutes_DDAL
    {
        List<Substitutes_D> SearchSubstitutes_D(Substitutes_D substitutes_D);

    }
}