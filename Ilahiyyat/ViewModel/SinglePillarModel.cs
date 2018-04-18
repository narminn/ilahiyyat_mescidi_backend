using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ilahiyyat.Models;

namespace Ilahiyyat.ViewModel
{
    public class SinglePillarModel
    {
        public Pillar _pillar { get; set; }
        public List<Pillar> _pillars { get; set; }
    }
}