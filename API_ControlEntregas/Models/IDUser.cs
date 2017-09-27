using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_ControlEntregas.Models
{
    public class IDUser
    {
        public String idUser { get; set; }
        public String fullName { get; set; }
        public String enterprise { get; set; }
        public String position { get; set; }
        public String email { get; set; }
        public bool status { get; set; }

        public IDUser()
        {

        }
    }
}