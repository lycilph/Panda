namespace Panda.ApplicationCore.Validation
{
    public interface IValidationRule
    {
        string Message { get; set; }
        bool Validate();
    }
}
