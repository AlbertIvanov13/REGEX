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
			string newPattern = @".*\@(?<planetName>[A-Z][a-z]+)[^\@\-\!\:\>]*\:(?<population>\d+)[^\@\-\!\:\>]*\!(?<attackType>[AD])[^\@\-\!\:\>]*\![^\@\-\!\:\>]*\->(?<soldierCount>\d+).*";
			int userInput = int.Parse(Console.ReadLine());
			uint count = 0;

			List<Planet> attackedPlanets = new List<Planet>();
			List<Planet> destroyedPlanets = new List<Planet>();

			List<char> newStrings = new List<char>();

			StringBuilder sb = new StringBuilder();
			bool isContaining = false;
			for (int i = 0; i < userInput; i++)
			{
				string encryptedMessage = Console.ReadLine();

				Match match = Regex.Match(encryptedMessage, pattern);
				if (match.Success)
				{
					count = 0;
					sb = new StringBuilder();
					for (int j = 0; j < encryptedMessage.Length; j++)
					{
						newStrings.Add(encryptedMessage[j]);
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
					uint population = uint.Parse(newMatch.Groups["population"].Value);
					string attackType = newMatch.Groups["attackType"].Value;
					uint soldierCount = uint.Parse(newMatch.Groups["soldierCount"].Value);
					Planet planet = new Planet(planetName, population, attackType, soldierCount);
					if (attackType == "A")
					{
						foreach (Planet item in attackedPlanets)
						{
							if (item.PlanetName == planetName)
							{
								isContaining = true;
							}
						}
						if (!isContaining)
						{
							attackedPlanets.Add(planet);
						}
					}
					else if (attackType == "D")
					{
						foreach (Planet item in destroyedPlanets)
						{
							if (item.PlanetName == planetName)
							{
								isContaining = true;
							}
						}
						if (!isContaining)
						{
							destroyedPlanets.Add(planet);
						}
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
			public string PlanetName { get; set; }
			public uint Population { get; set; }
			public string AttackType { get; set; }
			public uint SoldierCount { get; set; }

            public Planet(string name, uint population, string attackType, uint soldierCount)
            {
				PlanetName = name;
				Population = population;
				AttackType = attackType;
				SoldierCount = soldierCount;
            }
        }
	}
}