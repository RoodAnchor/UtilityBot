namespace UtilityBot.Services
{
    /// <summary>
    /// Сервис подсчёта длины сообщения
    /// </summary>
    public class MessageLengthCalculator : IService
    {
        #region Fields
        private String _message;
        #endregion Fields

        #region Constructors
        public MessageLengthCalculator(String message) =>
            _message = message;
        #endregion Constructors

        #region Methods
        public String GetResult() =>
            $"Введено символов: {_message.Length}";
        #endregion Methods
    }
}
