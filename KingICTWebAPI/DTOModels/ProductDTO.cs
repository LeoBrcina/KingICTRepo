namespace KingICTWebAPI.DTOModels
{
    public class ProductDTO
    {
        //data transfer object for choosing which information we want to show after initially getting everything from the dummy api

        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public decimal price { get; set; }
        public string[] images { get; set; }
    }
}
