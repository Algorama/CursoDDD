using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kernel.Domain.Validation
{
    public static class AnnotationsValidator
    {
        public static void ValidateAnnotations(this ValidatorResult result, object entity)
        {
            if (entity == null)
                return;

            var ctx = new ValidationContext(entity);

            var type = entity.GetType();
            var props = type.GetProperties();

            foreach (var prop in props)
            {
                var value = prop.GetValue(entity, null);
                ctx.MemberName = prop.Name;
                var validationList = new List<ValidationResult>();
                Validator.TryValidateProperty(value, ctx, validationList);

                foreach (var validation in validationList)
                    result.AddError(validation.ErrorMessage, prop.Name);
            }
        }
    }
}
