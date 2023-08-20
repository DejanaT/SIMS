using Project.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class User : BaseModel
    {
        public string JMBG {get; set; }
        public string Email {get; set; }
        public string Password {get; set; }
        public string Name {get; set; }
        public string Surname {get; set; }
        public string MobilePhone {get; set; }
        public UserType UserType { get; set; }

    }
}
