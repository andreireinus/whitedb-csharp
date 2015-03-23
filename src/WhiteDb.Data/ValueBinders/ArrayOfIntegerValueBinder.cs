namespace WhiteDb.Data.ValueBinders
{
    public class ArrayOfIntegerValueBinder : IValueBinder<int[]>
    {
        object IValueBinder.GetValue(DataRecord record, int index)
        {
            return this.GetValue(record, index);
        }

        public void SetValue(DataRecord record, int index, int[] value)
        {
            var subRecord = record.CreateRecord(value.Length);
            for (var i = 0; i < value.Length; i++)
            {
                subRecord.SetFieldValue(i, value[i]);
            }

            record.SetFieldValue(index, subRecord);
        }

        public int[] GetValue(DataRecord record, int index)
        {
            throw new System.NotImplementedException();
        }

        public void SetValue(DataRecord record, int index, object value)
        {
            this.SetValue(record, index, (int[])value);
        }
    }
}