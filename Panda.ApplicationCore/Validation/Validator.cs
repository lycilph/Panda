using System;
using System.Collections.Generic;
using System.Linq;

namespace Panda.ApplicationCore.Validation
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
            return Validate(new List<string> { property });
        }

        private string Validate()
        {
            return Validate(validation_rules.Keys);
        }

        private string Validate(IEnumerable<string> properties)
        {
            return properties.Where(p => validation_rules.ContainsKey(p))
                             .Select(p => validation_rules[p])
                             .Where(r => r.Validate())
                             .Select(r => r.Message)
                             .Aggregate((error, message) => error + message + Environment.NewLine);
        }
    }
}
