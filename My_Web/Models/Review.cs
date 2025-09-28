namespace My_Web.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }

        public Product? Product { get; set; }
        public User? User { get; set; }
    }
}
