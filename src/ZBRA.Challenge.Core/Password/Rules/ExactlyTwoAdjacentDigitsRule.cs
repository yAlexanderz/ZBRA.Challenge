using ZBRA.Challenge.Core.Interfaces;

namespace ZBRA.Challenge.Core.Password.Rules
{
    /// <summary>
    /// Regra que verifica se a senha possui pelo menos um grupo de exatamente dois dígitos adjacentes iguais.
    /// Os dígitos não podem fazer parte de um grupo maior (ex: "122" é válido, mas "1222" não conta o "22").
    /// </summary>
    public class ExactlyTwoAdjacentDigitsRule : IPasswordRule
    {
        /// <summary>
        /// Valida se a senha contém pelo menos um par de exatamente dois dígitos adjacentes iguais.
        /// </summary>
        /// <param name="password">A senha a ser validada</param>
        /// <returns>True se a senha atende à regra; caso contrário, false</returns>
        public bool Validate(string password)
        {
            for (int i = 0; i < password.Length - 1; i++)
            {
                // Verifica se encontrou dois caracteres iguais
                if (password[i] == password[i + 1])
                {
                    // Verifica se é um par exato (não parte de um grupo maior)
                    bool isExactlyPair = (i == 0 || password[i - 1] != password[i]) &&
                                         (i + 2 >= password.Length || password[i + 2] != password[i]);

                    if (isExactlyPair)
                    {
                        return true; // Encontrou um par exato
                    }

                    // Pula para o final deste grupo de dígitos repetidos
                    while (i + 1 < password.Length && password[i] == password[i + 1])
                    {
                        i++;
                    }
                }
            }

            // Não encontrou nenhum par exato
            return false;
        }

    }
}