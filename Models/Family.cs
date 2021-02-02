using System;
using System.Collections.Generic;

#nullable disable

namespace Houseplants.Models
{
    public partial class Family
    {
        public Family()
        {
            Plants = new HashSet<Plant>();
        }

        public int FamilyId { get; set; }
        public string FamilyName { get; set; }
        public string FamilyLatin { get; set; }

        public virtual ICollection<Plant> Plants { get; set; }
    }
}
