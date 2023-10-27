using System.ComponentModel.DataAnnotations;

namespace Market.Model
{
    public class Product
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public byte[] Pictures { get; set; }

        [Required]
        public bool State { get; set; }
    }
}
