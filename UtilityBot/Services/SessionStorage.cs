using System.Collections.Concurrent;
using UtilityBot.Enums;
using UtilityBot.Models;

namespace UtilityBot.Services
{
    /// <summary>
    /// Класс предоставляющий доступ 
    /// к коллекции сессий
    /// </summary>
    public class SessionStorage : IStorage
    {

        #region Fields
        private readonly ConcurrentDictionary<Int64, Session> _sessions;
        #endregion Fields

        #region Constructors
        public SessionStorage() =>
            _sessions = new ConcurrentDictionary<Int64, Session>();
        #endregion Constructors

        #region Methods
        public Session GetSession(Int64 chatId)
        {
            if (_sessions.ContainsKey(chatId))
                return _sessions[chatId];

            var newSession = new Session() { Service = ButtonsEnum.MessageLength };

            _sessions.TryAdd(chatId, newSession);

            return newSession;
        }
        #endregion Methods
    }
}
