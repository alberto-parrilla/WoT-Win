using System.Windows;
using ClientDLL.Client;
using KernelDLL.Common;
using WoT_Win.Common.ViewModels;

namespace WoT_Win.Authentication
{
    public class UserViewModel : CustomBaseViewModel
    {
        public UserViewModel(Window view, int userId, DataManager dataManager, IMainClient client) : base(view, client, dataManager)
        {

        }
    }
}
