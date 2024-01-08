using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityBot.Services
{
    /// <summary>
    /// Сервис производящий расчёт суммы чисел
    /// </summary>
    public class SumCalculator : IService
    {
        #region Fields
        private String _message;
        #endregion Fields

        #region Constructors
        public SumCalculator(String message) =>
            _message = message;
        #endregion Constructors

        #region Methods
        public String GetResult()
        {
            List<Int32> ints = new List<Int32>();
            String validationMessage;

            if (TryParseMessage(out ints, out validationMessage))
                return $"{String.Join(" + ", ints)} = {ints.Sum()}";

            return validationMessage;
        }

        /// <summary>
        /// Метод парсит строковое значение
        /// и возвращает список чисел
        /// </summary>
        /// <param name="ints">Список чисел в который записывается
        /// результат преобразования</param>
        /// <param name="validationMessage">Сообщение о валидации значений</param>
        /// <returns>true - если всё ок, false - если есть ошибки</returns>
        private Boolean TryParseMessage(out List<Int32> ints, out String validationMessage)
        {
            ints = new List<Int32>();

            List<String> parts = _message.Split(' ')
                .Where(x =>
                    !String.IsNullOrWhiteSpace(x) &&
                    !String.IsNullOrEmpty(x))
                .ToList();

            if (ValidateParts(parts, out validationMessage))
            {
                foreach (String part in parts)
                    ints.Add(Int32.Parse(part));

                return true;
            }

            return false;
        }

        /// <summary>
        /// Метод осуществляет валидацию введённых значений.
        /// </summary>
        /// <param name="parts">Список значений</param>
        /// <param name="message">Сообщение с результатом валидации</param>
        /// <returns>true - если всё ок, false - если есть ошибки</returns>
        private Boolean ValidateParts(List<String> parts, out String message)
        {
            List<String> failedParts = new List<String>();

            foreach (String part in parts)
            {
                Int32 value = 0;

                if (!Int32.TryParse(part, out value))
                    failedParts.Add($"«{part}»");
            }

            if (failedParts.Count > 0)
            {
                message = $"ОШИБКА! Следующие введённые вами символы не явлюятся числами:{Environment.NewLine}{String.Join(' ', failedParts)}";
                return false;
            }
            else
            {
                message = $"ОК";
                return true;
            }
        }
        #endregion Methods
    }
}
