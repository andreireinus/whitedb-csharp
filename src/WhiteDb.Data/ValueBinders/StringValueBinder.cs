namespace WhiteDb.Data.ValueBinders
{
    public class StringValueBinder : IValueBinder<string>
    {
        object IValueBinder.GetValue(DataRecord record, int index)
        {
            return this.GetValue(record, index);
        }

        public void SetValue(DataRecord record, int index, string value)
        {
            record.SetFieldValue(index, value);
        }

        public string GetValue(DataRecord record, int index)
        {
            return record.GetFieldValueString(index);
        }

        public void SetValue(DataRecord record, int index, object value)
        {
            this.SetValue(record, index, (string)value);
        }
    }
}