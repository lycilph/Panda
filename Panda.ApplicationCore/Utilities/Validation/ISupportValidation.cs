using System.ComponentModel;

namespace Panda.ApplicationCore.Utilities.Validation
{
    public interface ISupportValidation : IDataErrorInfo
    {
        void AddValidationRule<S, T>(ValidationRule<S, T> rule);
        void RemoveValidationRule(string property);
    }
}
