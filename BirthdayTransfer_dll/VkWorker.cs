using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace BirthdayTransfer_dll
{
    public class VkWorker
    {
        /// <summary>
        /// Application Id
        /// </summary>
        private const ulong _applicationId = 51419527;
        /// <summary>
        /// Логин ВКонтакте
        /// </summary>
        private readonly string _vkLogin;
        /// <summary>
        /// Пароль ВКонтакте
        /// </summary>
        private readonly string _vkPass;
        public VkApi Api { get; set; } = new VkApi();
        /// <summary>
        /// Список друзей
        /// </summary>
        public List<VkUser> VkUsers { get; set; } = new List<VkUser>();
        public VkWorker(string vkLogin, string vkPass)
        {
            _vkLogin = vkLogin;
            _vkPass = vkPass;
        }

        /// <summary>
        /// Авторизация ВКонтакте
        /// </summary>
        /// <returns></returns>
        public VkApi VkAuthorize()
        {
            var authorize = new ApiAuthParams();
            Settings scope = Settings.All;

            Api.Authorize(new ApiAuthParams
            {
                ApplicationId = _applicationId,
                Login = _vkLogin,
                Password = _vkPass,
                Settings = Settings.All,
            });
            return Api;
        }

        /// <summary>
        /// Получение списка друзей и их дней рождений
        /// </summary>
        public void GetVkFriends()
        {
            if (Api != null)
            {
                var users = Api.Friends.Get(new VkNet.Model.RequestParams.FriendsGetParams
                {
                    UserId = Api.UserId,
                    Count = 500,
                    Fields = ProfileFields.All
                });

                var vkFriends = users.Select(x => new
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    BirthDay = x.BirthDate,
                }).OrderBy(x => x.FirstName);

                foreach (var friend in vkFriends)
                {
                    if (friend.BirthDay != null)
                    {
                        VkUsers.Add(new VkUser(friend.FirstName, friend.LastName, friend.BirthDay));
                    }
                }
            }
        }
    }
}
