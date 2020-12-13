using System;
using System.Collections.Generic;
using System.Text;

namespace WebLab.DAL.Entities
{
    public class Food
    {
        public int FoodId { get; set; } // id 
        public string FoodName { get; set; } // название 
        public string Description { get; set; } // описание 
        public int Weight { get; set; } // скорость
        public string Image { get; set; } // имя файла изображения
                                          // Навигационные свойства
        /// <summary>
        /// группа самолетов (например военные, частные и т.д.)
        /// </summary>
        public int FoodGroupId { get; set; }
        public FoodGroup Group { get; set; }
    }

    public class FoodGroup
    {
        public int FoodGroupId { get; set; }
        public string GroupName { get; set; }
        /// <summary>
        /// Навигационное свойство 1-ко-многим
        /// </summary>
        public List<Food> Foods { get; set; }
    }
}
