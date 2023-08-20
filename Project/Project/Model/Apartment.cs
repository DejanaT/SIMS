namespace Project.Model
{
    public class Apartment : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int RoomsQuantity { get; set; }
        public int MaxNumberOfGuests { get; set; }
    }
}
