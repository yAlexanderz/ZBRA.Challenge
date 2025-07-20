using ZBRA.Challenge.Core.Interfaces;

namespace ZBRA.Challenge.Core.Password.Rules
{
    /// <summary>
    /// Regra que verifica se a senha possui pelo menos dois dígitos adjacentes iguais
    /// </summary>
    public class AdjacentDigitsRule : IPasswordRule
    {
        /// <summary>
        /// Valida se a senha contém pelo menos um par de dígitos adjacentes idênticos
        /// </summary>
        /// <param name="password">A senha a ser validada</param>
        /// <returns>True se a senha contém dígitos adjacentes iguais; caso contrário, false</returns>
        public bool Validate(string password)
        {
            // Verifica cada par de caracteres adjacentes
            for (int i = 0; i < password.Length - 1; i++)
            {
                if (password[i] == password[i + 1])
                {
                    return true; // Encontrou um par adjacente
                }
            }

            // Nenhum par adjacente encontrado
            return false;

            // Alternativa usando LINQ (menos eficiente):
            // return Enumerable.Range(0, password.Length - 1).Any(i => password[i] == password[i + 1]);
        }
    }
}