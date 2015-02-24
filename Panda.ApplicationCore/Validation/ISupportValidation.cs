using System.ComponentModel;

namespace Panda.ApplicationCore.Validation
{
    public interface ISupportValidation : IDataErrorInfo
    {
        void AddValidationRule<S, T>(ValidationRule<S, T> rule);
        void RemoveValidationRule(string property);
    }
}
