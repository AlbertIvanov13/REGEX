using System.Text.RegularExpressions;

namespace _06._SoftUniBarIncome
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string pattern = @"\%(?<customer>[A-Z][a-z]+)\%[^|$%.]*?\<(?<product>\w+)\>[^|$%.]*?\|(?<count>\d+)\|[^|$%.]*?(?<price>\d+|\d+.\d+)\$";

			List<Order> orders = new List<Order>();
			decimal totalIncome = 0;
			while (true)
			{
				string input = Console.ReadLine();
				if (input == "end of shift")
				{
					break;
				}

				Match match = Regex.Match(input, pattern);
				
				Order order = new Order();
				
				if (match.Success)
				{
					order.CustomerName = match.Groups["customer"].Value;
					order.Product = match.Groups["product"].Value;
					order.Quantity = int.Parse(match.Groups["count"].Value);
					order.Price = decimal.Parse(match.Groups["price"].Value);
					orders.Add(order);
				}
			}

			foreach (var item in orders)
			{
                Console.WriteLine($"{item.CustomerName}: {item.Product} - {item.TotalPrice():f2}");
				totalIncome += item.Price;
			}

            Console.WriteLine($"Total income: {totalIncome:f2}");
		}

		public class Order
		{
            public string CustomerName { get; set; }
            public string Product { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }

			public decimal TotalPrice()
			{
				return Price *= Quantity;
			}
        }
	}
}