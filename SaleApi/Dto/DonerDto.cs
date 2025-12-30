using SaleApi.Models;
using System.ComponentModel.DataAnnotations;

namespace SaleApi.Dto
{
    public class DonerDto
    {

        public class CreateDonerDto
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            [EmailAddress]
            public string EMail { get; set; }
        }

        public  class NewGiftFromDoner
        {
            public Gift DonerGift { get; set; }
        }

        public class DeleteDonerDto
        {
            [Required]
            public int Id { get; set; }
        }

        public class UpdateDonerDto
        {
            [Required]
            public int Id { get; set; }

            public string? FirstName { get; set; } = null;
            public string? LastName { get; set; } = null;
            [EmailAddress]
            public string? EMail { get; set; } = null;
        }
    }
}
