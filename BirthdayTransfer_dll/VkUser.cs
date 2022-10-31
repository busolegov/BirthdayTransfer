namespace BirthdayTransfer_dll
{
    public class VkUser
    {
        public string? Name { get; set; }
        /// <summary>
        /// Имя именинника
        /// </summary>
        public string? FirstName { get; set; }
        /// <summary>
        /// Фамилия именинника
        /// </summary>
        public string? LastName { get; set; }
        /// <summary>
        /// Дата дня рождения
        /// </summary>
        public DateTime BirthDate { get; set; }
        private readonly string _birthDay;

        public VkUser() { }
        public VkUser(string firstName, string lastName, string birthDate)
        {
            FirstName=firstName;
            LastName=lastName;
            _birthDay=birthDate;
            DateParser();
        }

        public VkUser(string name, string birthDate)
        {
            Name=name;
            _birthDay = birthDate;
        }

        private void DateParser()
        {
            string? correctFormatDate;
            string[] dateArray = _birthDay.Split('.');

            if (dateArray.Length == 2)
            {
                BirthDate = DateTime.Parse(_birthDay);
            }
            else
            {
                correctFormatDate = _birthDay[..^5];
                BirthDate = DateTime.Parse(correctFormatDate);
            }
        }
    }
}