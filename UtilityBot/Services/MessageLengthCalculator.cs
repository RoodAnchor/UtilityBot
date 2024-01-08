namespace UtilityBot.Services
{
    /// <summary>
    /// Сервис подсчёта длины сообщения
    /// </summary>
    public class MessageLengthCalculator : IService
    {
        #region Methods
        public String GetResult(String input) =>
            $"Введено символов: {input.Length}";
        #endregion Methods
    }
}
