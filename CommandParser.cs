using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CP_TOPO
{
    class CommandParser
    {
        public (string command, string[] args) Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return (string.Empty, new string[0]);


            var matches = Regex.Matches(input, @"[\""].+?[\""]|[^ ]+");

            List<string> parts = new List<string>();

            foreach (Match match in matches)
            {
                string value = match.Value;


                if (value.StartsWith("\"") && value.EndsWith("\""))
                    value = value.Substring(1, value.Length - 2);

                parts.Add(value);
            }

            if (parts.Count == 0)
                return (string.Empty, new string[0]);

            string command = parts[0];
            parts.RemoveAt(0);

            return (command, parts.ToArray());
        }
    }
}
