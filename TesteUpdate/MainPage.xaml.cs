using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TesteUpdate
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }



        private async void Button_Clicked(object sender, EventArgs e)
        {
            var service = DependencyService.Get<IApkService>();

            var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            var status_ = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();


            if (status != PermissionStatus.Granted)
                status = await Permissions.RequestAsync<Permissions.StorageRead>();

            if (status_ != PermissionStatus.Granted)
                status_ = await Permissions.RequestAsync<Permissions.StorageWrite>();

            if (status == PermissionStatus.Granted && status_ == PermissionStatus.Granted)
                service.install(service.GetPath());

        }
    }
}
