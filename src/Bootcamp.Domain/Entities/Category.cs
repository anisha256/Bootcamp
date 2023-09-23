﻿

using Bootcamp.Domain;
using Bootcamp.Domain.Entities;

namespace Bootcamp.WebAPI.Modals
{
    public class Category : AuditableEnttity
    {
        public Category() { 
        this.CategoryItems = new HashSet<CategoryItem>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<CategoryItem> CategoryItems { get; set; }


    }
}
