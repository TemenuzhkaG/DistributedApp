using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Data.Entities
{
   public class Category : BaseEntity
    {
        public Category()
        {
            this.Recipes = new HashSet<Recipe>();
        }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
