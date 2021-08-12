using System;
using System.Collections.Generic;
using System.Linq;

namespace Kernel.Domain.Model.Validation
{
    public class ValidatorException : Exception
    {
        public List<ValidatorMessage> Errors { get; }

        public ValidatorException(string message) : base(message)
        {
            Errors = new List<ValidatorMessage> { new ValidatorMessage(message) };
        }

        public ValidatorException(string message, string fieldName) : base(message)
        {
            Errors = new List<ValidatorMessage> { new ValidatorMessage(message, fieldName) };
        }

        public ValidatorException(Exception ex) : base(ParseErros(ex))
        {
            Errors = new List<ValidatorMessage> { new ValidatorMessage(ParseErros(ex)) };
        }

        public ValidatorException(Exception ex, string fieldName) : base(ParseErros(ex))
        {
            Errors = new List<ValidatorMessage> { new ValidatorMessage(ParseErros(ex), fieldName) };
        }

        public ValidatorException(List<ValidatorMessage> errors) : base(ParseErros(errors))
        {
            Errors = errors;
        }

        private static string ParseErros(IEnumerable<ValidatorMessage> errors)
        {
            string mensagem;
            if (errors.Any())
            {
                mensagem = "Erros encontrados :" + "<br/>";
                foreach (var erro in errors)
                    mensagem += $"  - {erro.Message}<br/>";
            }
            else
            {
                mensagem = "Erros encontrados";
            }

            return mensagem;
        }

        private static string ParseErros(Exception ex) => ex.InnerException?.Message ?? ex.Message;
    }
}
