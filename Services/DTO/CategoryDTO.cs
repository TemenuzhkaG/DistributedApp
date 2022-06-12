using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Services.DTO
{
   public class CategoryDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [DisplayName("Description")]
        [StringLength(500, MinimumLength = 5)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public bool Validate()
        {
            if (!String.IsNullOrEmpty(Name))
            {
                return true;
            }
            return false;

        }
    }
}
