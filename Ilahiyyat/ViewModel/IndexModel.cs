using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ilahiyyat.Models;
namespace Ilahiyyat.ViewModel
{
    public class IndexModel
    {
        public List<Slider> _slider { get; set; }
        public List<Sermon> _sermon { get; set; }
        public List<News> _news { get; set; }
        public List<Advertisement> _advert { get; set; }
    }
}