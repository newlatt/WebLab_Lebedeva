
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebLab.Blazor.Data
{
    public class FoodListViewModel
    {
        [JsonPropertyName("foodId")]
        public int FoodId { get; set; } // id 
        [JsonPropertyName("foodName")]
        public string FoodName { get; set; } // название 
    }
    public class FoodDetailsViewModel
    {
        [JsonPropertyName("foodName")] public string FoodName { get; set; } // название корма
        [JsonPropertyName("description")]
        public string Description { get; set; } // описание 
        [JsonPropertyName("weight")]
        public int Weight { get; set; } 
        [JsonPropertyName("image")]
        public string Image { get; set; } // имя файла изображения
    }
}
