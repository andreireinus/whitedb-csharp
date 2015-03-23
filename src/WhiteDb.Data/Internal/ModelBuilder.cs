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

            this.ImplementRecordInterface(tb);

            return tb;
        }

        private void ImplementRecordInterface(TypeBuilder typeBuilder)
        {
            typeBuilder.AddInterfaceImplementation(typeof(IRecord));

            AddProperty(typeBuilder, "Database", typeof(IntPtr));
            AddProperty(typeBuilder, "Record", typeof(IntPtr));
        }

        private static void AddProperty(TypeBuilder typeBuilder, string propertyName, Type propertyType)
        {
            FieldBuilder field = typeBuilder.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

            var getMethod = typeof(IRecord).GetProperty(propertyName).GetGetMethod();
            var setMethod = typeof(IRecord).GetProperty(propertyName).GetSetMethod();

            MethodBuilder methodBuilder = typeBuilder.DefineMethod("get_" + propertyName, MethodAttributes.Public | MethodAttributes.Virtual, propertyType, Type.EmptyTypes);
            ILGenerator getIl = methodBuilder.GetILGenerator();
            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, field);
            getIl.Emit(OpCodes.Ret);

            typeBuilder.DefineMethodOverride(methodBuilder, getMethod);

            methodBuilder = typeBuilder.DefineMethod("set_" + propertyName, MethodAttributes.Public | MethodAttributes.Virtual, null, new[] { propertyType });
            ILGenerator setIl = methodBuilder.GetILGenerator();
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, field);
            setIl.Emit(OpCodes.Ret);

            typeBuilder.DefineMethodOverride(methodBuilder, setMethod);
        }
    }
}