
using System.Text.RegularExpressions;

var regex = @"\+359([-\s])2\1(\d{3})\1(\d{4})\b";
var input = Console.ReadLine();

var matches = Regex.Matches(input, regex);

var matchedPhones = matches.Cast<Match>().Select(a => a.Value.Trim()).ToArray();

Console.WriteLine(string.Join(", ", matchedPhones));