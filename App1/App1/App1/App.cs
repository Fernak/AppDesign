#if __ANDROID__
using App1.Droid;
#elif __IOS__
using App1.iOS.Helpers;
#elif WINDOWS_UWP
using App1.UWP.Helpers;
#endif
using App1.Helpers;
using App1.Interfaces;
using App1.Services;
using App1.Model;

namespace App1
{
    public partial class App 
    {
        public App()
        {
        }

        public static void Initialize()
        {
            ServiceLocator.Instance.Register<IDataStore<Item>, MockDataStore>();
            ServiceLocator.Instance.Register<IMessageDialog, MessageDialog>();
            ServiceLocator.Instance.Register<IDataStore<Item>, MockDataStore>();
        }
    }
}