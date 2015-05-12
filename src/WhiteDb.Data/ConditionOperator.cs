namespace WhiteDb.Data
{
    public enum ConditionOperator
    {
        Equal = 0x0001, /** = */
        NotEqual = 0x0002,/** != */
        LessThan = 0x0004,/** < */
        Greater = 0x0008,/** > */
        LessThanEqual = 0x0010,/** <= */
        GreaterThanEqual = 0x0020      /** >= */
    }
}