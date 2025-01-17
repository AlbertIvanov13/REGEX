﻿using System.Text;
using System.Text.RegularExpressions;

namespace _05._Race
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string lettersPattern = @"(?<letters>[A-Za-z])";

			string numbersPattern = @"(?<numbers>\d)";

			Dictionary<string, Person> contestants = new Dictionary<string, Person>();
			string[] arrayOfNames = Console.ReadLine().Split(", ");
			int sum = 0;
			while (true)
			{
				string encryptedContestant = Console.ReadLine();
				if (encryptedContestant == "end of race")
				{
					break;
				}

				sum = 0;
				StringBuilder sb = new StringBuilder();
				Person person = new Person();
				for (int i = 0; i < encryptedContestant.Length; i++)
				{
					Match contestant = Regex.Match(encryptedContestant[i].ToString(), lettersPattern);
                    if (contestant.Success)
                    {       
				       sb.Append(contestant);
                    }

					Match contestant1 = Regex.Match(encryptedContestant[i].ToString(), numbersPattern);

					if (contestant1.Success)
					{
						person.Distance += int.Parse(contestant1.ToString());
					}
				}
				if (!contestants.ContainsKey(sb.ToString()) && arrayOfNames.Contains(sb.ToString()))
				{
					contestants.Add(sb.ToString(), person);
				}
				else if (arrayOfNames.Contains(sb.ToString()))
				{
					for (int i = 0; i < encryptedContestant.Length; i++)
					{
						Match contestant1 = Regex.Match(encryptedContestant[i].ToString(), numbersPattern);

						if (contestant1.Success)
						{
							contestants[sb.ToString()].Distance += int.Parse(contestant1.ToString());
						}
					}
				}
			}
            
			var orderedContestants = contestants.OrderByDescending(p => p.Value.Distance).Take(3).ToList();

            Console.WriteLine($"1st place: {orderedContestants[0].Key}");
            Console.WriteLine($"2nd place: {orderedContestants[1].Key}");
            Console.WriteLine($"3rd place: {orderedContestants[2].Key}");
		}

		class Person
		{
           // public string Name { get; set; }
            public int Distance { get; set; }
        }
	}
}