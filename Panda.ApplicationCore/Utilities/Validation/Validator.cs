using System;
using System.Collections.Generic;
using System.Linq;

namespace Panda.ApplicationCore.Utilities.Validation
{
    public class Validator
    {
        private readonly Dictionary<string, IValidationRule> validation_rules = new Dictionary<string, IValidationRule>();

        public string Error { get { return Validate(); } }

        public void AddValidationRule<S,T>(ValidationRule<S,T> rule)
        {
            validation_rules.Add(rule.PropertyName, rule);
        }

        public void RemoveValidationRule(string property)
        {
            validation_rules.Remove(property);
        }

        public string Validate(string property)
        {
            return validation_rules.ContainsKey(property) ? 
                   Validate(new List<string> { property }) : 
                   string.Empty;
        }

        private string Validate()
        {
            return Validate(validation_rules.Keys);
        }

        private string Validate(IEnumerable<string> properties)
        {
            var error_messages = properties.Intersect(validation_rules.Keys)
                                           .Select(p => validation_rules[p])
                                           .Where(r => !r.Validate())
                                           .Select(r => r.Message)
                                           .ToList();

            return error_messages.Any() ? 
                   error_messages.Aggregate((error, message) => error + message + Environment.NewLine) :
                   string.Empty;
        }
    }
}
