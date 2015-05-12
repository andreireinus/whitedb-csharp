namespace WhiteDb.Generator.Library
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class HeaderFileParser
    {
        private readonly TypeMapper typeMapper;

        public HeaderFileParser(TypeMapper typeMapper)
        {
            this.typeMapper = typeMapper;
        }

        public static bool IsFunctionLine(string line)
        {
            return FunctionDefinition.IsMatch(line);
        }

        private static readonly Regex FunctionDefinition = new Regex(@"([a-z_\*]+)\ ([a-z_]+)\((.*)\);");

        public FunctionDefinition ParseFunctionLine(string line)
        {
            if (!IsFunctionLine(line))
            {
                throw new ArgumentException("Line is not function definition", "line");
            }

            var match = FunctionDefinition.Match(line);

            var fd = new FunctionDefinition(match.Groups[2].Captures[0].Value, this.typeMapper.GetTypeName(match.Groups[1].Captures[0].Value))
                     {
                         Parameters = this.ParseParameters(match.Groups[3].Captures[0].Value)
                     };

            return fd;
        }

        public IDictionary<int, Tuple<string, string>> ParseParameters(string parameters)
        {
            var result = new Dictionary<int, Tuple<string, string>>();
            var index = 0;

            foreach (var parameter in parameters.Split(',').Select(a => a.Trim()).Where(a => a.Length > 0))
            {
                var parts = parameter.Replace("*", "* ").Split(' ').ToArray();
                var param = string.Join(" ", parts.Take(parts.Length - 1));

                var parameterName = parts.Last();

                if (IsReservedKeyword(parameterName))
                {
                    parameterName = "@" + parameterName;
                }

                result.Add(index++, new Tuple<string, string>(this.typeMapper.GetTypeName(param), parameterName));
            }

            return result;
        }

        private static bool IsReservedKeyword(string name)
        {
            return (name == "lock");
        }

        public IEnumerable<FunctionDefinition> Parse(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var line = LineCleanup(reader.ReadLine());
                    if (!IsFunctionLine(line))
                    {
                        continue;
                    }

                    yield return this.ParseFunctionLine(line);
                }
            }
        }

        private string LineCleanup(string line)
        {
            return line.Replace(" *", "* ");
        }
    }
}