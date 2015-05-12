namespace WhiteDb.Data.Internal
{
    using System;
    using System.Reflection;

    using WhiteDb.Data.ValueBinders;

    public class ModelBinder<T> where T : class
    {
        private readonly IntPtr databasePointer;

        private readonly Type type;

        private readonly ModelBuilder<T> builder;

        private readonly ValueBinderFactory factory = new ValueBinderFactory();

        public ModelBinder(IntPtr databasePointer)
        {
            this.databasePointer = databasePointer;
            this.type = typeof(T);
            this.builder = new ModelBuilder<T>();
        }

        public T FromRecord(DataRecord record)
        {
            var properties = this.type.GetProperties();
            var model = this.builder.Build();
            for (var index = 0; index < properties.Length; index++)
            {
                properties[index].SetValue(model, this.GetValue(record, index, properties[index]));
            }

            if (model is IRecord)
            {
                var recordModel = model as IRecord;
                recordModel.Database = record.DatabasePointer;
                recordModel.Record = record.RecordPointer;
            }

            return model;
        }

        private object GetValue(DataRecord record, int index, PropertyInfo property)
        {
            var binder = this.factory.Get(property.PropertyType);
            return binder.GetValue(record, index);
        }

        public DataRecord ToRecord(T entity)
        {
            var properties = this.type.GetProperties();
            var record = DataRecord.Create(this.databasePointer, properties.Length);

            for (var index = 0; index < properties.Length; index++)
            {
                var binder = this.factory.Get(properties[index].PropertyType);
                var value = properties[index].GetValue(entity);
                binder.SetValue(record, index, value);
            }

            return record;
        }

        public uint GetFieldIndex(MemberInfo member)
        {
            var properties = this.type.GetProperties();

            for (uint index = 0; index < properties.Length; index++)
            {
                if (properties[index] == member)
                {
                    return index;
                }
            }

            throw new InvalidOperationException("Unknown member in type");
        }
    }
}