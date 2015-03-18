namespace WhiteDb.Data.Internal
{
    using System;
    using System.Reflection;

    public class ModelBinder<T> where T : class
    {
        private readonly Type type;

        private readonly ModelBuilder<T> builder;

        public ModelBinder()
        {
            this.type = typeof(T);
            this.builder = new ModelBuilder<T>();
        }

        public T FromRecord(DataRecord record)
        {
            var properties = this.type.GetProperties();
            var model = this.builder.Build();
            for (var index = 0; index < properties.Length; index++)
            {
                properties[index].SetValue(model, GetValue(record, index, properties[index]));
            }

            return model;
        }

        private static object GetValue(DataRecord record, int index, PropertyInfo property)
        {
            if (property.PropertyType == typeof(int))
            {
                return record.GetFieldValueInteger(index);
            }
            if (property.PropertyType == typeof(string))
            {
                return record.GetFieldValueString(index);
            }

            throw new NotImplementedException(string.Format("PropertyType: {0}", property.PropertyType.Name));
        }
    }
}