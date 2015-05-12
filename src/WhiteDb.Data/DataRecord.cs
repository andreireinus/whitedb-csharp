namespace WhiteDb.Data
{
    using System;
    using System.Linq;

    public class DataRecord
    {
        private readonly IntPtr databasePointer;

        private readonly int length;
        private readonly IntPtr record;

        public DataRecord(IntPtr databasePointer, IntPtr record, int length)
        {
            this.databasePointer = databasePointer;
            this.record = record;
            this.length = length;
        }

        public DataRecord(IntPtr databasePointer, IntPtr record)
            : this(databasePointer, record, NativeApi.wg_get_record_len(databasePointer, record))
        {
        }

        public IntPtr DatabasePointer
        {
            get
            {
                return this.databasePointer;
            }
        }

        public IntPtr RecordPointer
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

            return NativeApi.wg_decode_char(this.databasePointer, this.GetFieldEncodedValue(index));
        }

        public virtual DateTime GetFieldValueDate(int index)
        {
            this.AssertFieldType(index, DataType.DATE);

            var value = NativeApi.wg_decode_date(this.databasePointer, this.GetFieldEncodedValue(index));

            return (new DateTime(1, 1, 1, 0, 0, 0)).AddDays(value - 1);
        }

        public virtual double GetFieldValueDouble(int index)
        {
            var type = this.AssertFieldType(index, DataType.Fixpoint, DataType.Double);

            return type == DataType.Fixpoint
                ? NativeApi.wg_decode_fixpoint(this.databasePointer, this.GetFieldEncodedValue(index))
                : NativeApi.wg_decode_double(this.databasePointer, this.GetFieldEncodedValue(index));
        }

        public virtual int GetFieldValueInteger(int index)
        {
            this.AssertFieldType(index, DataType.INT);

            return NativeApi.wg_decode_int(this.databasePointer, this.GetFieldEncodedValue(index));
        }

        public virtual DateTime GetFieldValueTime(int index)
        {
            this.AssertFieldType(index, DataType.TIME);

            var value = NativeApi.wg_decode_time(this.databasePointer, this.GetFieldEncodedValue(index));

            var seconds = value / 100;
            var milliSeconds = (value % 100) * 10;

            return (new DateTime(1, 1, 1, 0, 0, 0, 0)).AddSeconds(seconds).AddMilliseconds(milliSeconds);
        }

        public virtual string GetFieldValueString(int index)
        {
            this.AssertFieldType(index, DataType.STR);

            var dataPointer = this.GetFieldEncodedValue(index);

            var len = NativeApi.wg_decode_str_len(this.databasePointer, dataPointer) + 1;
            var bytes = new byte[len];

            NativeApi.wg_decode_str_copy(this.databasePointer, dataPointer, bytes, len);

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

            var type = NativeApi.wg_get_field_type(this.databasePointer, this.record, index);

            if (allowedTypes.Any(allowedType => allowedType == type))
            {
                return type;
            }

            var msg = string.Format("{0} / {1}", type, string.Join(", ", allowedTypes));
            throw new InvalidOperationException("Type is not allowed for this getter | " + msg);
        }

        private int GetFieldEncodedValue(int index)
        {
            this.AssertFieldIndex(index);

            return NativeApi.wg_get_field(this.databasePointer, this.record, index);
        }

        #region SetFieldValue overloads

        public void SetFieldValue(int index, int value)
        {
            this.SetFieldValue(index, () => NativeApi.wg_encode_int(this.databasePointer, value));
        }

        public void SetFieldValue(int index, char value)
        {
            this.SetFieldValue(index, () => NativeApi.wg_encode_char(this.databasePointer, value));
        }

        public void SetFieldValue(int index, double value)
        {
            if (value >= -800 && value <= 800)
            {
                this.SetFieldValue(index, () => NativeApi.wg_encode_fixpoint(this.databasePointer, value));
                return;
            }

            this.SetFieldValue(index, () => NativeApi.wg_encode_double(this.databasePointer, value));
        }

        public void SetFieldValue(int index, DateTime value, DateSaveMode mode)
        {
            // Date only
            Func<int> encodingFunction = () => NativeApi.wg_encode_date(this.databasePointer, NativeApi.wg_ymd_to_date(this.databasePointer, value.Year, value.Month, value.Day));
            if (mode == DateSaveMode.TimeOnly)
            {
                encodingFunction = () => NativeApi.wg_encode_time(this.databasePointer, NativeApi.wg_hms_to_time(this.databasePointer, value.Hour, value.Minute, value.Second, value.Millisecond / 10));
            }

            this.SetFieldValue(index, encodingFunction);
        }

        public void SetFieldValue(int index, string value)
        {
            this.SetFieldValue(index, () => NativeApi.wg_encode_str(this.databasePointer, value.ToByteArray(), null));
        }

        private void SetFieldValue(int index, Func<int> encodingFunction)
        {
            this.AssertFieldIndex(index);

            var dataValue = encodingFunction();
            var returnCode = NativeApi.wg_set_field(this.databasePointer, this.record, index, dataValue);
            if (returnCode != 0)
            {
                throw new WhiteDbException(returnCode, "Encoding or setting field value failed!");
            }
        }

        #endregion SetFieldValue overloads

        public static DataRecord Create(IntPtr databasePtr, int length)
        {
            return new DataRecord(databasePtr, NativeApi.wg_create_record(databasePtr, length), length);
        }
    }
}