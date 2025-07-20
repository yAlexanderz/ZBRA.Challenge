using ZBRA.Challenge.Core.Interfaces;

namespace ZBRA.Challenge.Core.Password.Rules
{
    /// <summary>
    /// Regra que verifica se a senha está dentro de um intervalo numérico válido.
    /// </summary>
    public class RangeRule : IPasswordRule
    {
        private readonly int _minValue;
        private readonly int _maxValue;

        /// <summary>
        /// Inicializa uma nova instância da regra de intervalo
        /// </summary>
        /// <param name="minValue">Valor mínimo permitido (inclusive)</param>
        /// <param name="maxValue">Valor máximo permitido (inclusive)</param>
        public RangeRule(int minValue, int maxValue)
        {
            // Validação básica dos valores de entrada
            if (minValue > maxValue)
                throw new ArgumentException("O valor mínimo não pode ser maior que o valor máximo");

            _minValue = minValue;
            _maxValue = maxValue;
        }

        /// <summary>
        /// Valida se a senha está dentro do intervalo especificado
        /// </summary>
        /// <param name="password">A senha a ser validada</param>
        /// <returns>True se a senha está dentro do intervalo; caso contrário, false</returns>
        public bool Validate(string password)
        {
            // Tenta converter a senha para um valor numérico
            if (!int.TryParse(password, out int numericPassword))
            {
                // Senha contém caracteres não numéricos
                return false;
            }

            // Verifica se o valor está dentro do intervalo
            return numericPassword >= _minValue && numericPassword <= _maxValue;
        }

        // Propriedades para acesso aos valores (somente leitura)
        public int MinValue => _minValue;
        public int MaxValue => _maxValue;
    }
}