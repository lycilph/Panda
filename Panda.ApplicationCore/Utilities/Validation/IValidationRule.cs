namespace Panda.ApplicationCore.Utilities.Validation
{
    public interface IValidationRule
    {
        string Message { get; set; }
        bool Validate();
    }
}
