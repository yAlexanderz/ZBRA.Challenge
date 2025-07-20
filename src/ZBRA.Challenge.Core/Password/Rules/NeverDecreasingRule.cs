using ZBRA.Challenge.Core.Interfaces;

namespace ZBRA.Challenge.Core.Password.Rules
{
    /// <summary>
    /// Regra que verifica se os dígitos da senha nunca diminuem da esquerda para a direita.
    /// Por exemplo: "123789" é válida, mas "123432" não é válida.
    /// </summary>
    public class NeverDecreasingRule : IPasswordRule
    {
        /// <summary>
        /// Valida se a senha tem dígitos que nunca diminuem da esquerda para a direita.
        /// </summary>
        /// <param name="password">A senha a ser validada</param>
        /// <returns>True se os dígitos nunca diminuem; caso contrário, false</returns>
        public bool Validate(string password)
        {
            // Verifica cada par adjacente de dígitos
            for (int i = 0; i < password.Length - 1; i++)
            {
                // Se encontrar qualquer dígito que diminui, a senha é inválida
                if (password[i] > password[i + 1])
                {
                    return false;
                }
            }

            // Se chegou até aqui, todos os dígitos estão em ordem não-decrescente
            return true;

            // Alternativa com LINQ - menos eficiente mas mais expressiva:
            // return !password.Zip(password.Skip(1), (a, b) => a > b).Any(decreasing => decreasing);
        }
    }
}