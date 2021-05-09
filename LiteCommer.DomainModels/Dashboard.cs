using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    public class Dashboard
    {
        public long Year { get; set; }
        public long January { get; set; }

        public long February { get; set; }

        public long March { get; set; }

        public long April { get; set; }

        public long May { get; set; }

        public long June { get; set; }

        public long July { get; set; }

        public long August { get; set; }

        public long September { get; set; }

        public long October { get; set; }

        public long November  { get; set; }

        public long December { get; set; }
        public long ToTal()
        {
            return January + February + March + April + May + June + July + August + September + December + October + November;
        }
    }
}
