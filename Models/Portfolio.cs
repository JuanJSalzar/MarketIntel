namespace backend.Models;

public class Portfolio
{
    public int StockId { get; set; }
    public Stock Stock { get; set; } 
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}