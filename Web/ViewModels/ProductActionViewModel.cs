using Entities;

namespace Web.ViewModels
{
    public class ProductActionViewModel: PageViewModel
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CategoryID { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public bool IsFeatured { get; set; }
        public bool DayProduct { get; set; }
        public bool IsSlider { get; set; }
        public int StockQuantity { get; set; }
        public int ProductRecordID { get; set; }
        public string Name { get; set; }
        public string? Summary { get; set; }
        public string? Description { get; set; }
        public string ProductPictures { get; set; }
        public int ThumbnailPicture { get; set; }
        public ICollection<ProductPicture> ProductPicturesList { get; set; }
    }
}
