namespace WhiteDb.Generator.Library
{
    using System;
    using System.Collections.Generic;

    public class FunctionDefinition
    {
        public FunctionDefinition(string name, string returnType)
        {
            this.Name = name;
            this.ReturnType = returnType;
        }

        public string Name { get; private set; }

        public string ReturnType { get; private set; }

        public IDictionary<int, Tuple<string, string>> Parameters { get; set; }
    }
}