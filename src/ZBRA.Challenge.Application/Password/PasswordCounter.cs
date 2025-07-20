using ZBRA.Challenge.Application.Interfaces;

namespace ZBRA.Challenge.Application.Password
{
    public class PasswordCounter
    {
        private readonly IPasswordValidator _validator;
        private readonly int _min;  
        private readonly int _max; 

        // TODO: adicionar cache para senhas já validadas (otimização)

        public PasswordCounter(IPasswordValidator validator, int minValue, int maxValue)
        {
            _validator = validator;
            _min = minValue;
            _max = maxValue;

            if (minValue > maxValue)
            {
                throw new ArgumentException("Valor mínimo deve ser menor ou igual ao máximo");
            }

            // Debug
            //Console.WriteLine($"Faixa: {_min}-{_max}, qtd números: {_max - _min + 1}");
        }

        public int CountValidPasswords()
        {
            var total = 0;

            for (int pwd = _min; pwd <= _max; pwd++)
            {
                if (_validator.IsValid(pwd.ToString()))
                {
                    total++;
                }
            }

            return total;
        }

        // versão alternativa usando LINQ, mas é menos eficiente
        /*
        public int CountValidPasswordsLinq()
        {
            return Enumerable.Range(_min, _max - _min + 1)
                .Count(p => _validator.IsValid(p.ToString()));
        }
        */
    }
}