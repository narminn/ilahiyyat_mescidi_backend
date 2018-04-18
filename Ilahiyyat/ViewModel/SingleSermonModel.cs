using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ilahiyyat.Models;

namespace Ilahiyyat.ViewModel
{
    public class SingleSermonModel
    {
        public Sermon _sermon { get; set; }
        public List<Sermon> _sermons { get; set; }
    }
}