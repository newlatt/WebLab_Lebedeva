using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLab.DAL.Data;
using WebLab.DAL.Entities;

namespace WebLab.Services
{
    public class DbInitializer
    {
        public static async Task Seed(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // создать БД, если она еще не создана
            context.Database.EnsureCreated();
            // проверка наличия ролей
            if (!context.Roles.Any())
            {
                var roleAdmin = new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "admin"
                };
                // создать роль admin
                await roleManager.CreateAsync(roleAdmin);
            }
            // проверка наличия пользователей
            if (!context.Users.Any())
            {
                // создать пользователя user@mail.ru
                var user = new ApplicationUser
                {
                    Email = "user@mail.ru",
                    UserName = "user@mail.ru"
                };
                await userManager.CreateAsync(user, "123456");
                // создать пользователя admin@mail.ru
                var admin = new ApplicationUser
                {
                    Email = "admin@mail.ru",
                    UserName = "admin@mail.ru"
                };
                await userManager.CreateAsync(admin, "123456");
                // назначить роль admin
                admin = await userManager.FindByEmailAsync("admin@mail.ru");
                await userManager.AddToRoleAsync(admin, "admin");
            }

            //проверка наличия групп объектов
            if (!context.FoodGroups.Any())
            {
                context.FoodGroups.AddRange(
                new List<FoodGroup>
                {
                    new FoodGroup {GroupName="Взрослые собаки"},
                    new FoodGroup {GroupName="Щенки"},
                    new FoodGroup {GroupName="Взрослые коты"},
                    new FoodGroup {GroupName="Котята"},
                    new FoodGroup {GroupName="Птицы"},
                    new FoodGroup {GroupName="Грызуны"}
                });
                await context.SaveChangesAsync();
            }
            // проверка наличия объектов
            if (!context.Foods.Any())
            {
                context.Foods.AddRange(new List<Food>
                {
                    new Food {FoodName="Bavaro Solid", Description="обеспечивает базовое питание для пожилых и/или малоактивных собак всех пород.", 
                        Weight = 18, FoodGroupId=1, Image="full_bavaro-dogfood-solid.png" },
                    new Food {FoodName="JosiDog Junior", Description="полнорационный корм для собак всех пород начиная с 8-й недели жизни", 
                        Weight =18, FoodGroupId=2, Image="josidog-junior.png" },
                    new Food {FoodName="JosiCat Crispy Duck", Description="полнорационный корм для взрослых кошек с аппетитным мясом утки", 
                        Weight =10, FoodGroupId=3, Image="josicat-crispy-duck-10kg.png" },
                    new Food {FoodName="Kitten", Description="Полнорационный сбалансированный корм супер-премиум класса для беременных, кормящих кошек, а также для котят", 
                        Weight =10, FoodGroupId=4, Image="030_Kitten.png" },
                    new Food {FoodName="JosiDog Economy", Description="Подходит для всех пород крупных и мелких собак", 
                        Weight =15, FoodGroupId=3, Image="JosiDog_econome.jpg" },
                    new Food {FoodName="Lolo Pets Basic", Description="Корм для попугаев",
                        Weight =1, FoodGroupId=5, Image="10.jpg" },
                    new Food {FoodName="Lolo Pets Carte", Description="Корм для грызунов",
                        Weight =1, FoodGroupId=6, Image="7.png" }
                });
                await context.SaveChangesAsync();
            }
        }

    }
}
