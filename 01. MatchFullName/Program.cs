
using System.Text.RegularExpressions;

string pattern = @"\b([A-Z][a-z])[a-z]* ([A-Z][a-z])[a-z]*\b";
string names = Console.ReadLine();

MatchCollection matchedNames = Regex.Matches(names, pattern);

foreach (Match m in matchedNames)
{
    Console.Write(m.Value + " ");
}

Console.WriteLine();