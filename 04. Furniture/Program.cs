namespace _04._Furniture
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string pattern = @"\>\>(?<type>[A-ZA-z]+)\<\<(?<price>\d+|\d+\.\d+)\!\d+";
		}

		class Furniture
		{
            public string Name { get; set; }

            public decimal Price { get; set; }
            public int Quantity { get; set; }

			public void TotalMoney()
			{
				Price *= Quantity;
			}
        }
	}
}
