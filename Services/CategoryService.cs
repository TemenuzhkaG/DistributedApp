using Data.Entities;
using Repository;
using Services.DTO;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly List<CategoryDTO> categoryDTO;
        private CategoryDTO newCategory;
        private Category myCategory;

        public CategoryService()
        {
            this.categoryDTO = new List<CategoryDTO>();
            this.newCategory = new CategoryDTO();
            this.myCategory = new Category();
        }

        public List<CategoryDTO> Get()
        {
            using (UnitOfWork unitOfWork = new())
            {
                foreach (var item in unitOfWork.CategoryRepository.Get())
                {

                    var categories = new CategoryDTO
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description
                    };


                    categoryDTO.Add(categories);
                }
            }
            return categoryDTO;
        }

        public CategoryDTO GetById(int id)
        {
            using (UnitOfWork unitOfWork = new())
            {
                Category category = unitOfWork.CategoryRepository.GetByID(id);
                if (category != null)
                {
                    newCategory = new CategoryDTO
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Description = category.Description
                    };
                }
            }

            return newCategory;
        }

        public bool Save(CategoryDTO categoryDTO)
        {
            myCategory = new Category()
            {
                Id = categoryDTO.Id,
                Name = categoryDTO.Name,
                Description = categoryDTO.Description

            };

            try
            {
                using (UnitOfWork unitOfWork = new())
                {
                    if (categoryDTO.Id == 0)
                    {
                        unitOfWork.CategoryRepository.Insert(myCategory);
                    }
                    else
                    {
                        unitOfWork.CategoryRepository.Update(myCategory);
                    }
                    unitOfWork.Save();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(CategoryDTO categoryDTO)
        {
            try
            {
                using (UnitOfWork unitOfWork = new())
                {
                    myCategory = unitOfWork.CategoryRepository.GetByID(categoryDTO.Id);
                    myCategory.Name = categoryDTO.Name;

                    myCategory.Description = categoryDTO.Description;
                    unitOfWork.CategoryRepository.Update(myCategory);
                    unitOfWork.Save();
                }

                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool Delete(int id)
        {
            try
            {
                using (UnitOfWork unitOfWork = new())
                {
                    myCategory = unitOfWork.CategoryRepository.GetByID(id);
                    unitOfWork.CategoryRepository.Delete(myCategory);
                    unitOfWork.Save();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
