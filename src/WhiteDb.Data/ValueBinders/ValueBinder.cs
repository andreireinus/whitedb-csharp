namespace WhiteDb.Data.ValueBinders
{
    using System;

    public class ValueBinderFactory
    {
        public IValueBinder<T> Get<T>()
        {
            if (typeof(T) == typeof(int))
            {
                return new IntegerValueBinder() as IValueBinder<T>;
            }
            throw new NotImplementedException();
        }

        public IValueBinder Get(Type type)
        {
            if (type == typeof(int))
            {
                return new IntegerValueBinder();
            }
            if (type == typeof(string))
            {
                return new StringValueBinder();
            }
            if (type == typeof(int[]))
            {
                return new ArrayOfIntegerValueBinder();
            }

            throw new NotImplementedException();
        }
    }

    public interface IValueBinder
    {
        object GetValue(DataRecord record, int index);

        void SetValue(DataRecord record, int index, object value);
    }

    public interface IValueBinder<T> : IValueBinder
    {
        new T GetValue(DataRecord record, int index);

        void SetValue(DataRecord record, int index, T value);
    }
}