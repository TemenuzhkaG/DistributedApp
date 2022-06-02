using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
  public  class Recipe : BaseEntity
    {
        public Recipe()
        {
            this.Ingredients = new HashSet<Ingredient>();
        }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]

        public string Instructions { get; set; }

        public int PreparationTime { get; set; }

        public int CookingTime { get; set; }

        public int PortionsCount { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
