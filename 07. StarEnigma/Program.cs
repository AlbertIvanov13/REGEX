using System.Text;
using System.Text.RegularExpressions;

namespace _07._StarEnigma
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string pattern = @"[STARstar]";
			string newPattern = @"\@(?<planetName>[A-Za-z]+)[^\@\-\!\:\>]*:(?<population>[^\@\-\!\:\>]*\d+)[^\@\-\!\:\>]*!(?<attackType>[^\@\-\!\:\>]*A|D[^\@\-\!\:\>]*)![^\@\-\!\:\>]*\->(?<soldierCount>[^\@\-\!\:\>]*\d+)";
			int input = int.Parse(Console.ReadLine());
			int count = 0;
			List<Planet> planets = new List<Planet>();
			for (int i = 0; i < input; i++)
			{
				string encryptedMessage = Console.ReadLine();

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

					for (int j = 0; j < encryptedMessage.Length; j++)
					{
						sb.Append((char)(encryptedMessage[j] - count));
					}

					Match newMatch = Regex.Match(sb.ToString(), newPattern);
					Planet planet = new Planet();
					if (newMatch.Success)
					{
						planet.PlanetName = newMatch.Groups["planetName"].Value;
						planet.Population = int.Parse(newMatch.Groups["population"].Value);
						planet.AttackType = newMatch.Groups["attackType"].Value;
						planet.SoldierCount = int.Parse(newMatch.Groups["soldierCount"].Value);
						planets.Add(planet);
					}
                }
            }

			var attackedPlanets = planets.Where(p => p.AttackType == "A").OrderBy(p => p.PlanetName).ToList();

			var destroyedPlanets = planets.Where(p => p.AttackType == "D").OrderBy(p => p.PlanetName).ToList();

            Console.WriteLine($"Attacked planets: {attackedPlanets.Count}");
			foreach (var item in attackedPlanets)
			{
                Console.WriteLine($"-> {item.PlanetName}");
			}

			Console.WriteLine($"Destroyed planets: {destroyedPlanets.Count}");
			foreach (var item in destroyedPlanets)
			{
				Console.WriteLine($"-> {item.PlanetName}");
			}
		}

		class Planet
		{
            public string PlanetName { get; set; }
            public int Population { get; set; }
            public string AttackType { get; set; }
            public int SoldierCount { get; set; }
        }
	}
}