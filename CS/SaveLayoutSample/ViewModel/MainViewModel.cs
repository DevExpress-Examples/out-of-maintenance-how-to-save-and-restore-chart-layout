using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SaveLayoutSample.ViewModel {
    class MainViewModel {
        public IReadOnlyList<DevAVSaleItem> ProductsByMonths { get; } = new DevAVSaleItemRepository().GetProductsByMonths();

        public IReadOnlyList<String> SeriesDataMembers { get; } = new List<String> { "Company", "Product", "Month" };
        public IReadOnlyList<String> ArgumentDataMembers { get; } = new List<String> { "Product", "Month" };
        public IReadOnlyList<String> ValueDataMembers { get; } = new List<String> { "Income", "Revenue" };
    }

    public class DevAVSaleItem {
        readonly List<DevAVSaleItem> saleItems = new List<DevAVSaleItem>();

        public DateTime OrderDate { get; set; }
        public string Product { get; set; }
        public string Company { get; set; }
        public string Month { get; set; }
        public double Income { get; set; }
        public double Revenue { get; set; }
        public string Category { get; set; }
        public List<DevAVSaleItem> SaleItems { get => this.saleItems; }
        public double TotalIncome { get => saleItems.Sum(i => i.Income); }
    }

    public class DevAVSaleItemRepository {
        readonly static string[] companies = new string[] {
            "DevAV North",
            "DevAV South",
            "DevAV West",
            "DevAV East",
            "DevAV Central"
        };
        static Dictionary<string, List<string>> categorizedProducts;

        static Dictionary<string, List<string>> CategorizedProducts {
            get {
                if (categorizedProducts == null) {
                    categorizedProducts = new Dictionary<string, List<string>>();
                    categorizedProducts["Cell Phones"] = new List<string>() { "Smartphones", "Mobile Phones", "Smart Watches", "Sim Cards" };
                    categorizedProducts["Computers"] = new List<string>() { "PCs", "Laptops", "Tablets", "Printers" };
                    categorizedProducts["TV, Audio"] = new List<string>() { "TVs", "Home Audio", "Headphones", "DVD Players" };
                    categorizedProducts["Car Electronics"] = new List<string>() { "GPS Units", "Radars", "Car Alarms", "Car Accessories" };
                    categorizedProducts["Power Devices"] = new List<string>() { "Batteries", "Chargers", "Converters", "Testers", "AC/DC Adapters" };
                    categorizedProducts["Photo"] = new List<string>() { "Cameras", "Camcorders", "Binoculars", "Flashes", "Tripodes" };
                }
                return categorizedProducts;
            }
        }

        public List<DevAVSaleItem> GetProductsByMonths() {
            Random rnd = new Random(1);
            List<DevAVSaleItem> items = new List<DevAVSaleItem>();
            foreach (string company in companies)
                foreach (string product in CategorizedProducts["Photo"]) {
                    DateTime dateTime = new DateTime(2017, 12, 01);
                    for (int i = 0; i < 12; i++) {
                        int income = rnd.Next(20, 100);
                        int revenue = income + rnd.Next(20, 50);
                        items.Add(new DevAVSaleItem() {
                            Company = company,
                            Product = product,
                            Month = dateTime.AddMonths(1).ToString("MMMM", CultureInfo.InvariantCulture),
                            Income = income,
                            Revenue = revenue
                        });
                        dateTime = dateTime.AddMonths(1);
                    }
                }
            return items;
        }
    }
}
