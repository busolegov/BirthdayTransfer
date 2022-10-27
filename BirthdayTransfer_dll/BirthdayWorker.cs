using CalDavYandexLibrary.CalDav.Objects;

namespace BirthdayTransfer_dll
{
    public class BirthdayWorker
    {
        private List<VkUser> _vkUsers = new List<VkUser>();

        public string Email { get; set; }
        private string _login = "n0rma.jeane";

        public string Pass { get; set; }
        private string _pass = "ildmkjwhtcygetaq";

        public string CalDavLink { get; set; }
        private string _mainCalendarPath = "https://caldav.yandex.ru/calendars/n0rma.jeane%40yandex.ru/events-20085745/";

        public BirthdayWorker(List<VkUser> vkUsers, string email, string pass, string calDavLink)
        {
            _vkUsers=vkUsers;
            Email=email;
            Pass=pass;
            CalDavLink=calDavLink;
        }

        public async Task PushEventToCalendarAsync()
        {
            var mainCalendarUri = new Uri(CalDavLink);
            var client = new Client(mainCalendarUri, _login, _pass);
            var allCalendars = await client.GetCalendarsAsync();
            var currentCalendar = allCalendars.First();

            foreach (var user in _vkUsers)
            {
                currentCalendar.CreateEvent($"День рождения - {user.FirstName} {user.LastName}",
                                            user.BirthDate, user.BirthDate.AddHours(1),"location");
                var results = client.SaveChangesAsync(currentCalendar).Result;
                //Save all changes and take results of conservation
                
                //Console.WriteLine($"День рождения {user.FirstName} {user.LastName} / {user.BirthDate}" +
                //                  $" добавлен в календарь.");
            }
        }
    }
}
