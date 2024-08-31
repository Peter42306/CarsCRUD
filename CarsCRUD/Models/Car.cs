namespace CarsCRUD.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public int Price { get; set; }
        public string? Description { get; set; }
        public bool IsAvailable { get; set; }
    }
}
