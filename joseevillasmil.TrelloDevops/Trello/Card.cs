using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace joseevillasmil.TrelloDevops.Trello
{
    public class Card
    {
        public string id { get; set; }
        public Badges badges { get; set; }
        public List<object> checkItemStates { get; set; }
        public bool closed { get; set; }
        public bool dueComplete { get; set; }
        public DateTime dateLastActivity { get; set; }
        public string desc { get; set; }
        public object due { get; set; }
        public object dueReminder { get; set; }
        public object email { get; set; }
        public string idBoard { get; set; }
        public List<object> idChecklists { get; set; }
        public string idList { get; set; }
        public List<string> idMembers { get; set; }
        public List<object> idMembersVoted { get; set; }
        public int idShort { get; set; }
        public object idAttachmentCover { get; set; }
        public List<object> labels { get; set; }
        public List<object> idLabels { get; set; }
        public bool manualCoverAttachment { get; set; }
        public string name { get; set; }
        public int pos { get; set; }
        public string shortLink { get; set; }
        public string shortUrl { get; set; }
        public object start { get; set; }
        public bool subscribed { get; set; }
        public string url { get; set; }
        public Cover cover { get; set; }
        public bool isTemplate { get; set; }
        public object cardRole { get; set; }
    }
}
