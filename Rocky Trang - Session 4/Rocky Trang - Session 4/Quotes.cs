using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocky_Trang___Session_4
{
    public class Quotes
    {
        public string quote { get; set; }
        public string author { get; set; }
        public bool favourite { get; set; }

        public Quotes(string quote, string author, bool favourite = false)
        {
            this.quote = quote;
            this.author = author;
            this.favourite = favourite;
        }
    }
}
