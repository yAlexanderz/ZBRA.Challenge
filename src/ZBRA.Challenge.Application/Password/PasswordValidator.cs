using ZBRA.Challenge.Application.Interfaces;
using ZBRA.Challenge.Core.Interfaces;

namespace ZBRA.Challenge.Application.Password
{
    public class PasswordValidator : IPasswordValidator
    {
        private readonly IEnumerable<IPasswordRule> _regras;

        public PasswordValidator(IEnumerable<IPasswordRule> rules)
        {
            if (rules == null)
                throw new ArgumentNullException(nameof(rules));

            _regras = rules;

            // TODO: adicionar log das regras carregadas
        }

        public bool IsValid(string password)
        {
            foreach (var regra in _regras)
            {
                if (!regra.Validate(password))
                {
                    return false;
                }
            }

            return true;
        }

        /* Versão inicial - menos eficiente
        public bool IsValidLINQ(string pwd)
        {
            return _regras.All(r => r.Validate(pwd));
        }
        */
    }
}