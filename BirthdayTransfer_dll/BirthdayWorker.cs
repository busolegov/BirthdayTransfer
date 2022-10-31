using CalDavYandexLibrary.CalDav.Objects;

namespace BirthdayTransfer_dll
{
    public class BirthdayWorker
    {
        private List<VkUser> _vkUsers = new List<VkUser>();

        /// <summary>
        /// Почта яндекс аккаунта
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Пароль яндекс приложения
        /// </summary>
        public string Pass { get; set; }
        /// <summary>
        /// Ссылка CalDav календаря
        /// </summary>
        public string CalDavLink { get; set; }

        public BirthdayWorker(List<VkUser> vkUsers, string email, string pass, string calDavLink)
        {
            _vkUsers=vkUsers;
            Email=email;
            Pass=pass;
            CalDavLink=calDavLink;
        }

        /// <summary>
        /// Добавление др юзеров из списка в календарь
        /// </summary>
        /// <returns></returns>
        public async Task PushEventToCalendarAsync()
        {
            var mainCalendarUri = new Uri(CalDavLink);
            var client = new Client(mainCalendarUri, Email, Pass);
            var allCalendars = await client.GetCalendarsAsync();
            var currentCalendar = allCalendars.First();

            foreach (var user in _vkUsers)
            {
                currentCalendar.CreateEvent($"День рождения - {user.FirstName} {user.LastName}",
                                            user.BirthDate, user.BirthDate.AddHours(1),"location");
                var results = client.SaveChangesAsync(currentCalendar).Result;
            }
        }
    }
}
