namespace WhiteDb.Data.Internal
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    public class ModelBuilder<T> where T : class
    {
        private readonly Type type = typeof(T);

        public T Build()
        {
            var generatedType = this.CompileResultType();

            return Activator.CreateInstance(generatedType) as T;
        }

        public T Build(T source)
        {
            var target = this.Build();

            var properties = this.type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var property in properties)
            {
                property.SetValue(target, property.GetValue(source));
            }

            return target;
        }

        private Type CompileResultType()
        {
            return this.GetTypeBuilder().CreateType();
        }

        private TypeBuilder GetTypeBuilder()
        {
            var typeSignature = "GenProxy_" + this.type.Name + "_" + Guid.NewGuid().ToString("N").ToUpperInvariant();

            var assemblyName = new AssemblyName(typeSignature);

            var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(typeSignature);
            var tb = moduleBuilder.DefineType(
                typeSignature,
                TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit | TypeAttributes.AutoLayout,
                this.type);

            return tb;
        }
    }
}