using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityBot.Services
{
    /// <summary>
    /// Интерфейс сервисов
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Метод получает результат работы сервиса
        /// </summary>
        /// <returns>Сообщение от сервиса</returns>
        public String GetResult();
    }
}
