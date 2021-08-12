using Kernel.Domain.Model.Entities;
using Kernel.Domain.Model.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kernel.Domain.Validation
{
    public class Validator<T> where T : IAggregateRoot
    {
        public async Task<ValidatorResult> Validate(T entity, ValidationType type = ValidationType.Default, string userName = null)
        {
            var result = new ValidatorResult();

            await Validate(result, entity, type, userName);

            return result;
        }

        public async Task<ValidatorResult> Validate(T[] entities, ValidationType type = ValidationType.Default, string userName = null)
        {
            var result = new ValidatorResult();
            var validateList = new List<Task>();

            foreach (var entity in entities)
                validateList.Add(Validate(result, entity, type, userName));

            await Task.WhenAll(validateList);

            return result;
        }

        private async Task Validate(ValidatorResult result, T entity, ValidationType type, string userName = null)
        {
            switch (type)
            {
                case ValidationType.Default:
                    await DefaultValidations(result, entity, userName);
                    break;
                case ValidationType.Insert:
                    await InsertValidations(result, entity, userName);
                    break;
                case ValidationType.Update:
                    await UpdateValidations(result, entity, userName);
                    break;
                case ValidationType.Delete:
                    await DeleteValidations(result, entity);
                    break;
                default:
                    await DefaultValidations(result, entity, userName);
                    break;
            }
        }

        protected virtual async Task AnnotationsValidations(ValidatorResult result, T entity)
        {
            await Task.Run(() => result.ValidateAnnotations(entity));
        }

        protected virtual async Task DefaultValidations(ValidatorResult result, T entity, string userName)
        {
            if (entity == null)
                result.AddError("Entity can not be null!");

            if (entity is ITrackable && string.IsNullOrWhiteSpace(userName))
                result.AddError("userName é Obrigatório!");

            await AnnotationsValidations(result, entity);
        }

        protected virtual async Task InsertValidations(ValidatorResult result, T entity, string userName)
        {
            await DefaultValidations(result, entity, userName);
        }

        protected virtual async Task UpdateValidations(ValidatorResult result, T entity, string userName)
        {
            await DefaultValidations(result, entity, userName);
        }

        protected virtual async Task DeleteValidations(ValidatorResult result, T entity)
        {
            await Task.CompletedTask;
        }
    }
}
