using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class BaseModel
    {
        public string Id { get; set; }
        public bool Deleted { get; set; } = false;
    }
}
