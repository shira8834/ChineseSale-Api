using SaleApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SaleApi.Dto.GiftDto;
using static SaleApi.Dto.UserDto;

namespace SaleApi.Dto
{
    public class BagDto
    {
        public class GetBagDto
        {
            [Key]
            public int Id { get; set; }

            [ForeignKey("User")]
            public int IdUser { get; set; }
            //public UserDtoToBag User { get; set; } = null!;

            [ForeignKey("Gift")]
            public int IdGift { get; set; }
            public GiftResponseDto Gift { get; set; } = null!;

        }

        public class CreateBagDto
        {

            [ForeignKey("User")]
            public int IdUser { get; set; }
            [ForeignKey("Gift")]
            public int IdGift { get; set; }
            //public CreateGiftDto Gift { get; set; }
        }
    }
}
