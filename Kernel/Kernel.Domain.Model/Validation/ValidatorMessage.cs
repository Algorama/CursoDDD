using Kernel.Domain.Model.Enums;
using System;

namespace Kernel.Domain.Model.Validation
{
    public class ValidatorMessage
    {
        public string Key { get; set; }
        public string Message { get; set; }
        public ValidatorMessageType MessageType { get; set; } = ValidatorMessageType.Error;

        public ValidatorMessage()
        {
        }

        public ValidatorMessage(string message, ValidatorMessageType messageType = ValidatorMessageType.Error)
        {
            Message = message;
            MessageType = messageType;
        }

        public ValidatorMessage(string message, string key, ValidatorMessageType messageType = ValidatorMessageType.Error)
        {
            Message = message;
            Key = key;
            MessageType = messageType;
        }

        public ValidatorMessage(Exception ex)
        {
            Message = ex.Message;
        }

        public ValidatorMessage(Exception ex, string key)
        {
            Message = ex.Message;
            Key = key;
        }
    }
}
