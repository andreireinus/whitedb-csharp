namespace WhiteDb.Data
{
    using System;
    using System.Linq;

    public class DataRecord
    {
        private readonly IntPtr database;

        private readonly int length;
        private readonly IntPtr record;

        public DataRecord(IntPtr database, IntPtr record, int length)
        {
            this.database = database;
            this.record = record;
            this.length = length;
        }

        public IntPtr Pointer
        {
            get
            {
                return this.record;
            }
        }

        #region GetFieldValue*

        public virtual char GetFieldValueChar(int index)
        {
            this.AssertFieldType(index, DataType.CHAR);

            return NativeApiWrapper.wg_decode_char(this.database, this.GetFieldEncodedValue(index));
        }

        public virtual DateTime GetFieldValueDate(int index)
        {
            this.AssertFieldType(index, DataType.DATE);

            var value = NativeApiWrapper.wg_decode_date(this.database, this.GetFieldEncodedValue(index));

            return (new DateTime(1, 1, 1, 0, 0, 0)).AddDays(value - 1);
        }

        public virtual double GetFieldValueDouble(int index)
        {
            var type = this.AssertFieldType(index, DataType.Fixpoint, DataType.Double);

            return type == DataType.Fixpoint
                ? NativeApiWrapper.wg_decode_fixpoint(this.database, this.GetFieldEncodedValue(index))
                : NativeApiWrapper.wg_decode_double(this.database, this.GetFieldEncodedValue(index));
        }

        public virtual int GetFieldValueInteger(int index)
        {
            this.AssertFieldType(index, DataType.INT);

            return NativeApiWrapper.wg_decode_int(this.database, this.GetFieldEncodedValue(index));
        }

        public virtual DateTime GetFieldValueTime(int index)
        {
            this.AssertFieldType(index, DataType.TIME);

            var value = NativeApiWrapper.wg_decode_time(this.database, this.GetFieldEncodedValue(index));

            var seconds = value / 100;
            var milliSeconds = (value % 100) * 10;

            return (new DateTime(1, 1, 1, 0, 0, 0, 0)).AddSeconds(seconds).AddMilliseconds(milliSeconds);
        }

        public virtual string GetFieldValueString(int index)
        {
            this.AssertFieldType(index, DataType.STR);

            var dataPointer = this.GetFieldEncodedValue(index);

            var len = NativeApiWrapper.wg_decode_str_len(this.database, dataPointer) + 1;
            var bytes = new byte[len];

            NativeApiWrapper.wg_decode_str_copy(this.database, dataPointer, bytes, len);

            return bytes.Take(len - 1).ToArray().ToStringValue();
        }

        #endregion GetFieldValue*

        private void AssertFieldIndex(int index)
        {
            if (index < 0 || index >= this.length)
            {
                throw new ArgumentOutOfRangeException("index", "Field index is out of range");
            }
        }

        private int AssertFieldType(int index, params int[] allowedTypes)
        {
            this.AssertFieldIndex(index);

            var type = NativeApiWrapper.wg_get_field_type(this.database, this.record, index);

            if (allowedTypes.Any(allowedType => allowedType == type))
            {
                return type;
            }

            throw new InvalidOperationException("Type is not allowed for this getter");
        }

        private int GetFieldEncodedValue(int index)
        {
            this.AssertFieldIndex(index);

            return NativeApiWrapper.wg_get_field(this.database, this.record, index);
        }

        #region SetFieldValue overloads

        public void SetFieldValue(int index, int value)
        {
            this.SetFieldValue(index, () => NativeApiWrapper.wg_encode_int(this.database, value));
        }

        public void SetFieldValue(int index, char value)
        {
            this.SetFieldValue(index, () => NativeApiWrapper.wg_encode_char(this.database, value));
        }

        public void SetFieldValue(int index, double value)
        {
            if (value >= -800 && value <= 800)
            {
                this.SetFieldValue(index, () => NativeApiWrapper.wg_encode_fixpoint(this.database, value));
                return;
            }

            this.SetFieldValue(index, () => NativeApiWrapper.wg_encode_double(this.database, value));
        }

        public void SetFieldValue(int index, DateTime value, DateSaveMode mode)
        {
            switch (mode)
            {
                case DateSaveMode.DateOnly:
                    this.SetFieldValue(index, () => NativeApiWrapper.wg_encode_date(this.database, NativeApiWrapper.wg_ymd_to_date(this.database, value.Year, value.Month, value.Day)));
                    return;

                case DateSaveMode.TimeOnly:
                    this.SetFieldValue(index, () => NativeApiWrapper.wg_encode_time(this.database, NativeApiWrapper.wg_hms_to_time(this.database, value.Hour, value.Minute, value.Second, value.Millisecond / 10)));
                    return;
            }

            throw new InvalidOperationException("Unknown mode for DateSaveMode");
        }

        public void SetFieldValue(int index, string value)
        {
            this.SetFieldValue(index, () => NativeApiWrapper.wg_encode_str(this.database, value.ToByteArray(), null));
        }

        private void SetFieldValue(int index, Func<int> encodingFunction)
        {
            this.AssertFieldIndex(index);

            var dataValue = encodingFunction();
            var returnCode = NativeApiWrapper.wg_set_field(this.database, this.record, index, dataValue);
            if (returnCode != 0)
            {
                throw new WhiteDbException(returnCode, "Encoding or setting field value failed!");
            }
        }

        #endregion SetFieldValue overloads
    }
}