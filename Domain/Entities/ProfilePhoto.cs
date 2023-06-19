using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProfilePhoto : File
    {
        public AppUser AppUser { get; set; }
    }
}
