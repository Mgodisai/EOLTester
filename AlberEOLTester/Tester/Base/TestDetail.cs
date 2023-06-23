using TraceabilityHandler;

namespace AlberEOL.Base
{
    public class TestDetail
    {
        protected string paramName;
        public virtual string ParamName
        {
            get { return paramName; }
            set { paramName = value; }
        }
        public virtual string UnitOfMeasure { get; set; }
        public virtual string Min { get; set; }
        public virtual string Max { get; set; }
        public virtual string Nominal { get; set; }
        public string ResultValue { get; set; }
        public bool Passed { get; set; }
        public string[] GetDataGridViewRow()
        {
            return new string[] {
                                ParamName,
                                ResultValue,
                                UnitOfMeasure,
                                Min,
                                Max,
                                Nominal,
                                Passed.ToString()
                            };
        }
    }

    public class TraceTestDetail : TestDetail
    {
        public Parameter TraceParameter { get; set; }
        public override string ParamName
        {
            get
            {
                if (paramName == null) return TraceParameter.Name;
                else return paramName;
            }
        }
        public override string UnitOfMeasure
        {
            get
            {
                return TraceParameter.Unit;
            }
        }
        public override string Min
        {
            get
            {
                if (TraceParameter.Min != null)
                {
                    return TraceParameter.Min.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public override string Max
        {
            get
            {
                if (TraceParameter.Max != null)
                {
                    return TraceParameter.Max.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public override string Nominal
        {
            get
            {
                if (TraceParameter.Nominal != null)
                {
                    return TraceParameter.Nominal.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
