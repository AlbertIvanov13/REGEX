using System.ComponentModel.Design;
using System.Text;
using System.Text.RegularExpressions;

/*
STCHoghudd4=63333$G$0A53333
STCDoghudd4=63333$D$0A53333
STCVoghudd4=63333$G$0A53333
STCNoghudd4=63333$G$0A53333
STCKoghudd4=63333$G$0A53333
STCDoghudd4bbbb=bbbb63333$D$0Abbbb53333
STCQoghudd4=63333$G$0A53333
 */
namespace _07._StarEnigma
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string pattern = @"[STARstar]";
			string newPattern = @"\@(?<planetName>[A-Za-z]+)[^@\-!:>]*\:[^@\-!:>]*?(?<population>\d+)[^@\-!:>]*\!(?<attackType>[A|D])\![^@\-!:>]*\-\>[^@\-!:>]*?(?<soldierCount>\d+)";
			int input = int.Parse(Console.ReadLine());
			int count = 0;

			bool isContaining = false;
			List<Planet> planets = new List<Planet>();
			for (int i = 0; i < input; i++)
			{
				string encryptedMessage = Console.ReadLine();

				if (encryptedMessage == null)
				{
					continue;
				}

				Match match = Regex.Match(encryptedMessage, pattern);

				if (match.Success)
				{
					count = 0;
					StringBuilder sb = new StringBuilder();
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

						foreach (Planet item in planets)
						{
							if (item.PlanetName == planetName)
							{
								isContaining = true;
							}
						}

						if (!isContaining)
						{
							planets.Add(planet);
						}
					}
				}
			}

			var attackedPlanets = planets.Where(p => p.AttackType == "A").OrderBy(p => p.PlanetName).ToList();
			Console.WriteLine($"Attacked planets: {attackedPlanets.Count}");
			foreach (Planet attackedPlanet in attackedPlanets)
			{
				Console.WriteLine($"-> {attackedPlanet.PlanetName}");
			}

			var destroyedPlanets = planets.Where(p => p.AttackType == "D").OrderBy(p => p.PlanetName).ToList();
			Console.WriteLine($"Destroyed planets: {destroyedPlanets.Count}");
			foreach (Planet destroyedPlanet in destroyedPlanets)
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