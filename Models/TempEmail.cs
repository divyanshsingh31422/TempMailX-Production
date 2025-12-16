namespace TempMailX.Models
{
    public class TempEmail
    {
        public int Id { get; set; }
        public string? EmailAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiryAt { get; set; }
        public bool IsActive { get; set; }
    }
}
