using System.Text.RegularExpressions;
using FluentValidation.Validators;

namespace SampleApiCoreCqrs.Application.Common.Validators
{
    public class Md5Validator : PropertyValidator
    {
        public Md5Validator() : base("'{PropertyValue}' is not a valid value")
        { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue == null)
                return false;

            return Regex.IsMatch(context.PropertyValue.ToString(), "^[0-9a-fA-F]{32}$", RegexOptions.Compiled);
        }
    }
}
