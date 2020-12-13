using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLab.DAL.Entities;

namespace WebLab.Models
{
    public class Cart
    {
        public Dictionary<int, CartItem> Items { get; set; }
        public Cart()
        {
            Items = new Dictionary<int, CartItem>();
        }
        /// <summary>
        /// Количество объектов в корзине
        /// </summary>
        public int Count
        {
            get
            {
                return Items.Sum(item => item.Value.Quantity);
            }
        }
        /// <summary>
        /// Вес
        /// </summary>
        public int Weights
        {
            get
            {
                return Items.Sum(item => item.Value.Quantity * item.Value.Food.Weight);
            }
        }
        /// <summary>
        /// Добавление в корзину
        /// </summary>
        /// <param name="food">добавляемый объект</param>
        virtual public void AddToCart(Food food)
        {
            // если объект есть в корзине
            // то увеличить количество
            if (Items.ContainsKey(food.FoodId)) Items[food.FoodId].Quantity++;
            // иначе - добавить объект в корзину
            else Items.Add(food.FoodId, new CartItem { Food = food, Quantity = 1 });
        }
        /// <summary>
        /// Удалить объект из корзины
        /// </summary>
        /// <param name="id">id удаляемого объекта</param>
        virtual public void RemoveFromCart(int id)
        {
            Items.Remove(id);
        }
        /// <summary>
        /// Очистить корзину
        /// </summary>
        virtual public void ClearAll()
        {
            Items.Clear();
        }
    }
    /// <summary>
    /// Клас описывает одну позицию в корзине
    /// </summary>
    public class CartItem
    {
        public Food Food { get; set; }
        public int Quantity { get; set; }
    }
}
