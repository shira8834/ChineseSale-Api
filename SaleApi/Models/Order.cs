using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaleApi.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int IdUser { get; set; }

        [Required, ForeignKey("Gift")]
        public int IdGift { get; set; }

        public Gift Gift { get; set; }

        public bool Win { get; set; }
    }
}
