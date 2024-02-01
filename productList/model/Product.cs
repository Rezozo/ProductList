namespace productList.model
{
    public  class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Article { get; set; }
        public int ProductShopNumber { get; set; }
        public decimal Cost { get; set; }
        public string Type { get; set; }
        public byte[] Image { get; set; }

        public Product() { }

        public Product(int id, string title, string description, string article, int productShopNumber, decimal cost, string type, byte[] image)
        {
            Id = id;
            Title = title;
            Description = description;
            Article = article;
            ProductShopNumber = productShopNumber;
            Cost = cost;
            Type = type;
            Image = image;
        }
    }
}
