using System;
using System.Collections.Generic;

#nullable disable

namespace Houseplants.Models
{
    public partial class Plant
    {
        public int PlantId { get; set; }
        public string PlantName { get; set; }
        public string Latin { get; set; }
        public bool Level { get; set; }
        public int Family { get; set; }
        public int Care { get; set; }

        public virtual Care CareNavigation { get; set; }
        public virtual Family FamilyNavigation { get; set; }
    }
}
