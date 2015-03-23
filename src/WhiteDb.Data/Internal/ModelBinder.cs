namespace WhiteDb.Data.Internal
{
    using System;
    using System.Reflection;

    using WhiteDb.Data.ValueBinders;

    public class ModelBinder<T> where T : class
    {
        private readonly DataContext context;

        private readonly Type type;

        private readonly ModelBuilder<T> builder;

        private readonly ValueBinderFactory factory = new ValueBinderFactory();

        public ModelBinder(DataContext context)
        {
            this.context = context;
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
            var record = this.context.CreateRecord(properties.Length);

            for (var index = 0; index < properties.Length; index++)
            {
                var binder = this.factory.Get(properties[index].PropertyType);
                var value = properties[index].GetValue(entity);
                binder.SetValue(record, index, value);
            }

            return record;
        }
    }
}