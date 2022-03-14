using Entities;

namespace Web.ViewModels
{
    public class SearchProductVM
    {
        public List<Category>? Categories { get; set; }
        public List<Product>? Products { get; set; }
        public int? CategoryId { get; set; }
        public int? SortBy { get; set; }
        public string? SearchTerm { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? PageNo{ get; set; }
        public int MaxPage { get; set; }

    }
}
