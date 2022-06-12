using LinqLab1;
using LinqLab1.Domain;

var storage = new Storage();
storage.SeedData();

var queries = new Queries(storage);

Console.WriteLine("1. Get all items:");
queries.GetAllItemsInStorage().Print();

Console.WriteLine("2. Show supply dates for each manufacturer:");
queries.JoinManufacturerWithSupplyDates().Print();

Console.WriteLine("3. Join items with manufacturers:");
queries.JoinItemsWithManufacturers();
queries.GetAllItemsInStorage().Print();

Console.WriteLine("4. Get all phones");
queries.GetAllPhones().Print();

Console.WriteLine("5. Order items(item name only) by descending price:");
queries.GetItemsSortedByPrice().PrintPriceInfo();

Console.WriteLine("6. Order items by descending last supply date");
queries.GetItemsSortedByLastSupplyDate().PrintLastSupplyDateTimeInfo();

Console.WriteLine("7. Get 3 cheapest items");
queries.GetTop3CheapestItems().PrintPriceInfo();

Console.WriteLine("8. Count items by manufacturer");
queries.GetItemsAmountPerManufacturer().Print();

Console.WriteLine("9. Get item categories sold by each manufacturer");
queries.GetItemCategoriesSoldByManufacturer().Print();

Console.WriteLine("10. Get all non-wireless chargers (using except)");
queries.GetWiredChargers().Print();

Console.WriteLine("11. Get manufacturers who sells at least one laptop (contains)");
queries.GetManufacturersWithAtLeastOneLaptop().Print();

Console.WriteLine("12. Get laptops with price from 100$ to 200$");
queries.FindLaptopsByPriceRange(100, 200).Print();

Console.WriteLine($"13. Get average price of phone: {queries.GetAveragePriceOfPhones()}");

Console.WriteLine("14. Show supplies with related item and manufacturer sorted by descending");
queries.GetSuppliesInfo().Print();

Console.WriteLine("15. Show the most expensive item per manufacturer");
queries.GetTheMostExpensiveItemsPerManufacturer().Print();