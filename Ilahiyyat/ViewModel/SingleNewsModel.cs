using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ilahiyyat.Models;

namespace Ilahiyyat.ViewModel
{
    public class SingleNewsModel
    {
        public News _single_news { get; set; }
        public List<News> _news { get; set; }
    }
}