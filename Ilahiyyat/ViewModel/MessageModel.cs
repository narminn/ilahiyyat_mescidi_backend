using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ilahiyyat.Models;

namespace Ilahiyyat.ViewModel
{
    public class MessageModel
    {
        public List<Contact> _contact { get; set; }
        public List<Question> _quest { get; set; }
    }
}