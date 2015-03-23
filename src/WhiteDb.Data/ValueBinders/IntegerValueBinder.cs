namespace WhiteDb.Data.ValueBinders
{
    public class IntegerValueBinder : IValueBinder<int>
    {
        public int GetValue(DataRecord record, int index)
        {
            return record.GetFieldValueInteger(index);
        }

        public void SetValue(DataRecord record, int index, object value)
        {
            this.SetValue(record, index, (int)value);
        }

        public void SetValue(DataRecord record, int index, int value)
        {
            record.SetFieldValue(index, value);
        }

        object IValueBinder.GetValue(DataRecord record, int index)
        {
            return this.GetValue(record, index);
        }
    }
}