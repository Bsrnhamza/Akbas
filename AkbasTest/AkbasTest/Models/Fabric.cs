using System.ComponentModel.DataAnnotations;

namespace AkbasTest.Models
{
    public class Fabric
    {
        public int FabricId { get; set; }

        [Required(ErrorMessage = "Lütfen kumaş adını giriniz.")]
        public string FabricName { get; set; }

        [Required(ErrorMessage = "Lütfen kumaş türünü giriniz.")]
        public string FabricType { get; set; }

        [Required(ErrorMessage = "Lütfen kumaş rengini giriniz.")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Lütfen kumaş fiyatını giriniz.")]
        [Range(0, double.MaxValue, ErrorMessage = "Geçerli bir fiyat giriniz.")]
        public decimal Price { get; set; }
    }
}
