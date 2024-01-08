using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UtilityBot.Models;

namespace UtilityBot.Services
{
    /// <summary>
    /// Интерфейс хранилища сессии
    /// </summary>
    public interface IStorage
    {
        /// <summary>
        /// Метод получает объект <seealso cref="Session"/>
        /// с данными о текущей сессии
        /// </summary>
        /// <param name="chatId">ИД чата</param>
        /// <returns>Объект <seealso cref="Session"/></returns>
        Session GetSession(Int64 chatId);
    }
}
