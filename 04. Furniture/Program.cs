using System.Text.RegularExpressions;

namespace _04._Furniture
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string pattern = @"\>\>(?<type>[A-ZA-z]+)\<\<(?<price>\d+|\d+\.\d+)\!(?<quantity>\d+)";

			List<Furniture> allFurnitures = new List<Furniture>();
			decimal money = 0;
			while (true)
			{
				string input = Console.ReadLine();
				if (input == "Purchase")
				{
					break;
				}

				Match furniture = Regex.Match(input, pattern);

				if (furniture.Success)
				{
					Furniture newFurniture = new Furniture();
					newFurniture.Name = furniture.Groups["type"].Value;
					newFurniture.Price = decimal.Parse(furniture.Groups["price"].Value);
					newFurniture.Quantity = int.Parse(furniture.Groups["quantity"].Value);
					money += newFurniture.TotalMoney();

					allFurnitures.Add(newFurniture);
				}

			}

            Console.WriteLine("Bought furniture: ");

			foreach (var item in allFurnitures)
			{
                Console.WriteLine($"{item.Name}");
			}

            Console.WriteLine($"Total money spend: {money:f2}");
		}

		class Furniture
		{
            public string Name { get; set; }

            public decimal Price { get; set; }
            public int Quantity { get; set; }

			public decimal TotalMoney()
			{
				return Price *= Quantity;
			}
        }
	}
}