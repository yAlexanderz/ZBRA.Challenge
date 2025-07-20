using ZBRA.Challenge.Application.Interfaces;
using ZBRA.Challenge.Application.Password;
using ZBRA.Challenge.Core.Interfaces;
using ZBRA.Challenge.Core.Password.Rules;

namespace ZBRA.Challenge.Application.Services
{
    public class PasswordChallengeService : IPasswordChallengeService
    {
        // TODO: fazer cache dos resultados para melhorar performance

        public int SolvePart1(int min, int max)
        {
            var regras = new List<IPasswordRule> {
                new RangeRule(min, max),
                new AdjacentDigitsRule(), // precisa de pelo menos um par adjacente
                new NeverDecreasingRule() // nunca decresce
            };

            return ContarSenhasValidas(regras, min, max);
        }

        public int SolvePart2(int min, int max)
        {
            // parte 2: mesmas regras + a regra do par exato
            var regras = new List<IPasswordRule> {
                new RangeRule(min, max),
                new AdjacentDigitsRule(),
                new NeverDecreasingRule(),
                new ExactlyTwoAdjacentDigitsRule() // nova regra: pelo menos um par exato
            };

            return ContarSenhasValidas(regras, min, max);
        }

        // metodo auxiliar para evitar repetição
        private int ContarSenhasValidas(IEnumerable<IPasswordRule> regras, int min, int max)
        {
            var validador = new PasswordValidator(regras);
            var contador = new PasswordCounter(validador, min, max);

            var resultado = contador.CountValidPasswords();

            return resultado;
        }

        /* Versão paralela - testar depois
        private int ContarSenhasValidasParalelo(IEnumerable<IPasswordRule> regras, int min, int max) 
        {
            var validador = new PasswordValidator(regras);
            
            return Enumerable.Range(min, max - min + 1)
                .AsParallel()
                .Count(n => validador.IsValid(n.ToString()));
        }
        */
    }
}