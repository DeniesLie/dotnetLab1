using LinqLab1.Domain;

namespace LinqLab1;

public class Queries
{
    private readonly Storage _storage;

    public Queries(Storage storage)
        => _storage = storage;
    
    // query
    public IDictionary<Manufacturer, IEnumerable<DateTime>> JoinManufacturerWithSupplyDates()
    {
        var result = from m in _storage.Manufacturers
            join i in _storage.Items on m.Id equals i.ManufacturerId
                into g
            select new
            {
                Manufacturer = m,
                SupplyDateTimes = g.SelectMany(i => i.SupplyDateTimes).Distinct()
            };
        return result.ToDictionary(g => g.Manufacturer, 
            g => g.SupplyDateTimes);
    }

    public void JoinItemsWithManufacturers()
    {
        _storage.Items = _storage.Items.Join(_storage.Manufacturers,
            i => i.ManufacturerId,
            m => m.Id,
            (item, manufacturer) =>
            {
                item.Manufacturer = manufacturer;
                return item;
            }).ToList();
    }

    // query
    public IEnumerable<Item> GetAllItemsInStorage()
        => from i in _storage.Items select i;

    public IEnumerable<Item> GetAllPhones()
        => _storage.Items
            .Where(i =>
                i.ItemCategories.Select(c => c.Name)
                    .Contains("smartphone"));

    // query
    public IEnumerable<Item> GetItemsSortedByPrice()
        => from i in _storage.Items
            orderby i.PricePerUnit descending
            select i;

    public IEnumerable<Item> GetItemsSortedByLastSupplyDate()
        => _storage.Items.OrderByDescending(i => i.SupplyDateTimes.Max());
    
    // query
    public IDictionary<Manufacturer, int> GetItemsAmountPerManufacturer()
    {
        var result = 
            from item in _storage.Items
            group item by item.Manufacturer;

        return result.ToDictionary(g => g.Key, g => g.Count());
    }

    public IEnumerable<Item> GetTop3CheapestItems()
        => _storage.Items.OrderBy(i => i.PricePerUnit).Take(3);
    
    // query
    public IDictionary<Manufacturer, IEnumerable<ItemCategory>> GetItemCategoriesSoldByManufacturer()
    {
        var result =
            from item in _storage.Items
            group item by item.Manufacturer;

        return result.ToDictionary(g => g.Key,
            g => g.SelectMany(i => i.ItemCategories).Distinct());
    }
    
    public IEnumerable<Item> FindLaptopsByPriceRange(double fromPrice, double toPrice)
        => _storage.Items.Where(i =>
            i.ItemCategories.Select(i => i.Name).Contains("laptop")
            && i.PricePerUnit >= fromPrice && i.PricePerUnit <= toPrice);
    
    // query
    public IEnumerable<Item> GetWiredChargers()
    {
        return (from item in _storage.Items
                where (item.ItemCategories.Select(c => c.Name).Contains("charger"))
                select item)
            .Except(
                from item in _storage.Items
                where (item.ItemCategories.Select(c => c.Name).Contains("wireless"))
                select item);
    }
    
    public IDictionary<Manufacturer, IEnumerable<Item>> GetManufacturersWithAtLeastOneLaptop()
        => _storage.Items.GroupBy(i => i.Manufacturer)
            .Where(g => 
                g.Any(i => i.ItemCategories.Select(c => c.Name).Contains("laptop")))
            .ToDictionary(g => g.Key!, 
                g => 
                    g.Where(i => i.ItemCategories.Select(c => c.Name).Contains("laptop")));
    
    // query
    public double GetAveragePriceOfPhones()
        => (from item in _storage.Items
                where (item.ItemCategories.Select(c => c.Name).Contains("smartphone"))
                select item)
            .Average(i => i.PricePerUnit);
    

    public IDictionary<DateTime, IEnumerable<Item>> GetSuppliesInfo()
        => _storage.Items.SelectMany(i =>
                i.SupplyDateTimes.Select(date => new {Item = i, SupplyDateTime = date}))
            .GroupBy(s => s.SupplyDateTime)
            .OrderByDescending(s => s.Key)
            .ToDictionary(key => key.Key, 
                val => val.Select(s => s.Item));
    
    public IDictionary<Manufacturer, Item> GetTheMostExpensiveItemsPerManufacturer()
        => _storage.Items.GroupBy(i => i.Manufacturer)
            .ToDictionary(g => g.Key!, 
                g => 
                    g.Aggregate((i1, i2) => i1.PricePerUnit > i2.PricePerUnit ? i1 : i2));
}
