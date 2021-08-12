using Kernel.Domain.Model.Enums;
using Kernel.Domain.Model.Validation;
using System.Collections.Generic;
using System.Linq;

namespace Kernel.Domain.Validation
{
    public class ValidatorResult
    {
        public List<ValidatorMessage> Errors { get; set; }

        public List<ValidatorMessage> Warnings { get; set; }

        public virtual bool HasErrors => Errors.Any();

        public virtual bool HasWarnings => Warnings.Any();

        public ValidatorResult()
        {
            Errors = new List<ValidatorMessage>();
            Warnings = new List<ValidatorMessage>();
        }

        public void AddFieldError(string errorMessage, string fieldName)
        {
            if (Errors == null)
                Errors = new List<ValidatorMessage>();

            Errors.Add(new ValidatorMessage
            {
                Key = fieldName,
                Message = errorMessage,
                MessageType = ValidatorMessageType.Error
            });
        }

        public void AddError(string errorMessage, string fieldName = null)
        {
            if (Errors == null)
                Errors = new List<ValidatorMessage>();

            Errors.Add(new ValidatorMessage
            {
                Key = fieldName,
                Message = errorMessage,
                MessageType = ValidatorMessageType.Error
            });
        }

        public void AddFieldWarning(string warningMessage, string fieldName)
        {
            if (Warnings == null)
                Warnings = new List<ValidatorMessage>();

            Warnings.Add(new ValidatorMessage
            {
                Key = fieldName,
                Message = warningMessage,
                MessageType = ValidatorMessageType.Warning
            });
        }

        public void AddWarning(string warningMessage, string fieldName = null)
        {
            if (Warnings == null)
                Warnings = new List<ValidatorMessage>();

            Warnings.Add(new ValidatorMessage
            {
                Key = fieldName,
                Message = warningMessage,
                MessageType = ValidatorMessageType.Warning
            });
        }
    }
}
