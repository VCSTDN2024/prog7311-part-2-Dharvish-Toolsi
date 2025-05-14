namespace PROG7311_POE_Part_2.Models
{
    public class CombinedModel
    {
        // Used when a view needs the farmer and their products.
        public Farmer Farmer { get; set; }
        public Product? Product { get; set; }
        public List<Product>? Products { get; set; }
        public CombinedModel() { }
        public CombinedModel(Farmer farmer, List<Product>? products)
        {
            Farmer = farmer;
            Products = products;
        }
    }
}
