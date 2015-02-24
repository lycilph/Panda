using System;

namespace Panda.ApplicationCore.Validation
{
    public class ValidationRule<S, T> : IValidationRule
    {
        public S Source { get; set; }
        public string PropertyName { get; set; }
        public Func<S, T> Property { get; set; }
        public Func<T, bool> Condition { get; set; }
        public string Message { get; set; }

        public bool Validate()
        {
            var value = Property(Source);
            return Condition(value);
        }
    }
}
