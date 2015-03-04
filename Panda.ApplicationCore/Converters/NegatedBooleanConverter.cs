namespace Panda.ApplicationCore.Converters
{
    public class NegatedBooleanConverter : BooleanConverter<bool>
    {
        public NegatedBooleanConverter() : base(false, true) { }
    }
}