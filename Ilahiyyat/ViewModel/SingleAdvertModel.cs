using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ilahiyyat.Models;

namespace Ilahiyyat.ViewModel
{
    public class SingleAdvertModel
    {
        public Advertisement _single_advert { get; set; }
        public List<Advertisement> _advert { get; set; }
    }
}