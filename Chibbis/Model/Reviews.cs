using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chibbis.Model
{
    public class Reviews
    {
        public bool IsPositive { get; set; }
        public string Message { get; set; }
        public string DateAdded { get; set; }
        public string UserFIO { get; set; }
        public string RestaurantName { get; set; }
    }
}
