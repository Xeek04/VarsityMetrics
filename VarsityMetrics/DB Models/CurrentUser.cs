using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarsityMetrics.DB_Models
{
    internal class CurrentUser
    {
        public static string Username { get; set; }
        public static Constants.Role Role { get; set; }
    }
}
