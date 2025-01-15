
using System.Text.RegularExpressions;

var regex = @"(?<day>\d{2})(\.|-|/)(?<month>[A-Z][a-z]+)\1(?<year>\d{4})";

var input = Console.ReadLine();

var dates = Regex.Matches(input, regex);

foreach (Match date in dates)
{
	var day = date.Groups["day"].Value;
	var month = date.Groups["month"].Value;
	var year = date.Groups["year"].Value;

    Console.WriteLine($"Day: {day}, Month: {month}, Year: {year}");
}