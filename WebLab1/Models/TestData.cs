using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLab.DAL.Data;
using WebLab.DAL.Entities;

namespace WebLab.Models
{
    public class TestData
    {
        public static List<Food> GetFoodsList()
        {
            return new List<Food>
            {
                new Food{ FoodId=1, FoodGroupId=1},
                new Food{ FoodId=2, FoodGroupId=1},
                new Food{ FoodId=3, FoodGroupId=2},
                new Food{ FoodId=4, FoodGroupId=2},
                new Food{ FoodId=5, FoodGroupId=3}
            };
        }
        public static IEnumerable<object[]> Params()
        {
            // 1-я страница, кол. объектов 3, id первого объекта 1
            yield return new object[] { 1, 3, 1 };
            // 2-я страница, кол. объектов 2, id первого объекта 4
            yield return new object[] { 2, 2, 4 };
        }

        public static void FillContext(ApplicationDbContext context)
        {
            context.FoodGroups.Add(new FoodGroup { GroupName = "fake group" });
            context.AddRange(new List<Food>
                {
                    new Food{ FoodId=1, FoodGroupId=1},
                    new Food{ FoodId=2, FoodGroupId=1},
                    new Food{ FoodId=3, FoodGroupId=2},
                    new Food{ FoodId=4, FoodGroupId=2},
                    new Food{ FoodId=5, FoodGroupId=3}
                });
            context.SaveChanges();
        }
    }
}
