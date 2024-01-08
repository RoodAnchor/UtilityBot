# UtilityBot
 Утилитарный ТГ бот

## Общие требования
1. Бот должен иметь две функции: подсчёт количества символов в тексте и вычисление суммы чисел, которые вы ему отправляете (одним сообщением через пробел).
2. То есть в ответ на условное сообщение «сова летит» он должен прислать что-то вроде «в вашем сообщении 10 символов». А в ответ на сообщение «2 3 15» должен прислать «сумма чисел: 20».
3. Выбор одной из двух функций должен происходить на старте в «Главном меню». При старте (через /start) бот должен присылать клиенту ответное сообщение — меню с кнопками, из которого можно выбрать, какое действие пользователь хочет выполнить (по аналогии с тем, как мы выбирали язык в VoiceTexterBot).
 
## Реализация
Основным является класс Bot. Асинхронный метод UpdateAsyncHandler является обработчиком входящих сообщений.
В зависимости от типа события (CallBackQuery или Message) вызываются соответствующий контроллеры.
Т.к. бот должен принимать только текстовые сообщения и события нажатия на кнопки, реализовано 3 контроллера:
1. InlineKeyboardController - для обработки кнопочных сообщений.
2. TextMessageController - для обработки текстовых сообщений.
3. DefaultMessageController - для возврата сообщения о неподдерживаемом формате сообщения.
 
Контроллеры являются производными классами от базового абстратного класса BaseController. 
BaseController реализует базовый функционал отправки сообщений в tg bot api. Этот класс содержит абстрактное 
св-во CallbackMessage (Сообщение пользователю) и виртуальный метод Handle, который можно при необходимости переопределить.

Данные о сессии (Выбранная функция бота) представлены сервисом SessionStorage, который реализует интерфейс ISession.
Помимо SessionStorage существуют ещё 2 сервиса:
1. MessageLengthCalculator
2. SumCalculator
 
Первый сервис предоставляет функционал вычисления количества символов сообщения.
Второй - вычисление суммы введённых чисел.

Оба сервиса реализуют интерфейс IService.