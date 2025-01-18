using System.ComponentModel.Design;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;

namespace _07._StarEnigma
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string pattern = @"[STARstar]";
			string newPattern = @".*\@(?<planetName>[A-Za-z]+)[^\@\-\!\:\>]*:(?<population>\d+)[^\@\-\!\:\>]*!(?<attackType>A|D)![^\@\-\!\:\>]*->(?<soldierCount>\d+).*";
			uint userInput; while (!uint.TryParse(Console.ReadLine(), out userInput) || userInput < 0 || userInput > 100) Environment.Exit(0);
			uint count = 0;

			List<Planet> attackedPlanets = new List<Planet>();
			List<Planet> destroyedPlanets = new List<Planet>();

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < userInput; i++)
			{
				string encryptedMessage = Console.ReadLine();

				if (encryptedMessage == null)
				{
					break;
				}

				Match match = Regex.Match(encryptedMessage, pattern);

				if (match.Success)
				{
					count = 0;
					sb = new StringBuilder();
					for (int j = 0; j < encryptedMessage.Length; j++)
					{
						if (encryptedMessage[j].ToString().Equals("S", StringComparison.OrdinalIgnoreCase))
						{
							count++;
						}
						else if (encryptedMessage[j].ToString().Equals("T", StringComparison.OrdinalIgnoreCase))
						{
							count++;
						}
						else if (encryptedMessage[j].ToString().Equals("A", StringComparison.OrdinalIgnoreCase))
						{
							count++;
						}
						else if (encryptedMessage[j].ToString().Equals("R", StringComparison.OrdinalIgnoreCase))
						{
							count++;
						}
					}
				}

				for (int k = 0; k < encryptedMessage.Length; k++)
				{
					sb.Append((char)(encryptedMessage[k] - count));
				}

				string word = sb.ToString();

				Match newMatch = Regex.Match(word, newPattern);
				if (newMatch.Success)
				{
					string planetName = newMatch.Groups["planetName"].Value;
					int population = int.Parse(newMatch.Groups["population"].Value);
					string attackType = newMatch.Groups["attackType"].Value;
					int soldierCount = int.Parse(newMatch.Groups["soldierCount"].Value);
					Planet planet = new Planet(planetName, population, attackType, soldierCount);
					if (attackType == "A")
					{
						attackedPlanets.Add(planet);
					}
					else if (attackType == "D")
					{
						destroyedPlanets.Add(planet);
					}
				}
			}

			List<Planet> filterAttacked = attackedPlanets.OrderBy(p => p.PlanetName).ToList();
			Console.WriteLine($"Attacked planets: {filterAttacked.Count}");
			foreach (Planet attackedPlanet in filterAttacked)
			{
				Console.WriteLine($"-> {attackedPlanet.PlanetName}");
			}

			List<Planet> filterDestroyed = destroyedPlanets.OrderBy(p => p.PlanetName).ToList();
			Console.WriteLine($"Destroyed planets: {filterDestroyed.Count}");
			foreach (Planet destroyedPlanet in filterDestroyed)
			{
				Console.WriteLine($"-> {destroyedPlanet.PlanetName}");
			}
		}

		public class Planet
		{
			public string? PlanetName { get; set; }
			public int Population { get; set; }
			public string? AttackType { get; set; }
			public int SoldierCount { get; set; }

            public Planet(string name, int population, string attackType, int soldierCount)
            {
				PlanetName = name;
				Population = population;
				AttackType = attackType;
				SoldierCount = soldierCount;
            }
        }
	}
}