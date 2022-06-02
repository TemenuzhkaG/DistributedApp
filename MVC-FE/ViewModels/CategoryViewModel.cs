﻿using Services.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_FE.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public CategoryViewModel() { }

        public CategoryViewModel(RecipeDTO recipeDTO)
        {
            Id = recipeDTO.Id;
            Name = recipeDTO.Name;
        }
    }
}