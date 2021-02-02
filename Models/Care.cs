using System;
using System.Collections.Generic;

#nullable disable

namespace Houseplants.Models
{
    public partial class Care
    {
        public Care()
        {
            Plants = new HashSet<Plant>();
        }

        public int CareId { get; set; }
        public string WaterNeed { get; set; }
        public string LightNeed { get; set; }
        public string NutritionNeed { get; set; }

        public virtual ICollection<Plant> Plants { get; set; }
    }
}
