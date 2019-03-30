using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webs.classesMetier;

namespace webs.Models
{
    public class ExceptionAccessDB :Exception
    {
        public ExceptionAccessDB(string msgDetail) : base(msgDetail)
        {
        }
    }
}