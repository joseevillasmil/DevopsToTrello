using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace joseevillasmil.TrelloDevops.Trello
{
    public class List
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool closed { get; set; }
        public object color { get; set; }
        public string idBoard { get; set; }
        public int pos { get; set; }
        public bool subscribed { get; set; }
        public object softLimit { get; set; }
    }
}
