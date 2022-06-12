namespace LinqLab1.Domain;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public decimal PricePerUnit { get; set; }
    public uint Amount { get; set; }
    public List<DateTime> SupplyDateTimes { get; set; } = new();
    
    public int ManufacturerId { get; set; }
    public Manufacturer? Manufacturer { get; set; }
    public List<ItemCategory> ItemCategories { get; set; } = new();
    
}