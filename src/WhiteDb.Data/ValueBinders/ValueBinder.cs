namespace WhiteDb.Data.ValueBinders
{
    using System;

    public class ValueBinderFactory
    {
        public ValueBinder<T> Get<T>()
        {
            if (typeof(T) == typeof(int))
            {
                return new IntegerValueBinder() as ValueBinder<T>;
            }
            throw new NotImplementedException();
        }
    }

    public class IntegerValueBinder : ValueBinder<int>
    {
        public override int GetValue(DataRecord record, int index)
        {
            return record.GetFieldValueInteger(index);
        }

        public override void SetValue(DataRecord record, int index, int value)
        {
            record.SetFieldValue(index, value);
        }
    }

    public abstract class ValueBinder<T>
    {
        public abstract T GetValue(DataRecord record, int index);

        public abstract void SetValue(DataRecord record, int index, T value);
    }
}