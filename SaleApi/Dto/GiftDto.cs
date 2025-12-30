using SaleApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SaleApi.Dto
{
    public class GiftDto
    {
        public class GetGiftDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string? Description { get; set; }

            public string? Img { get; set; }

            public int Price { get; set; }
            public int IdDoner { get; set; }
            public Doner Doner { get; set; }
        }
        public class CreateGiftDto
        {
            public string Name { get; set; }
            public string? Description { get; set; }

            public string? Img { get; set; }

            [JsonPropertyName("price")]
            public int Price { get; set; }

            public int IdDoner { get; set; }
           // public int? CategoryId { get; set; }

        }

        public class UpdateGiftDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string? Description { get; set; }

            public string? Img { get; set; }

            public int Price { get; set; }

            public int IdDoner { get; set; }
            // public int? CategoryId { get; set; }

        }

        public class GiftDonerDto
        {
            public int Id { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; } 
            [EmailAddress]
            public string EMail { get; set; }
        }


    }
}
