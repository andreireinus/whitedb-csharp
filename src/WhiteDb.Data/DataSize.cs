namespace WhiteDb.Data
{
    public class DataSize
    {
        private readonly int amount;

        private DataSize(int amount)
        {
            this.amount = amount;
            this.Mutliplier = 1;
        }

        public DataSize Bytes
        {
            get
            {
                this.Mutliplier = 1;
                return this;
            }
        }

        public DataSize KiloBytes
        {
            get
            {
                this.Mutliplier = 1024;
                return this;
            }
        }

        public DataSize MegaBytes
        {
            get
            {
                this.Mutliplier = 1024 * 1024;
                return this;
            }
        }

        public int ToInteger()
        {
            return this.amount * this.Mutliplier;
        }

        private int Mutliplier { get; set; }

        public static DataSize Size(int amount)
        {
            return new DataSize(amount);
        }
    }
}