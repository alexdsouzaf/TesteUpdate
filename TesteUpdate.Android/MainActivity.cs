using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Content;
using Xamarin.Forms;

namespace TesteUpdate.Droid
{
    [Activity(Label = "TesteUpdate", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            DependencyService.Register<AndroidService>();
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnNewIntent(Intent intent)
        {
            //Bundle extras = intent.Extras;
            //if (PACKAGE_INSTALLED_ACTION.Equals(intent.Action))
            //{
            //    var status = extras.GetInt(PackageInstaller.ExtraStatus);
            //    var message = extras.GetString(PackageInstaller.ExtraStatusMessage);
            //    switch (status)
            //    {
            //        case (int)PackageInstallStatus.PendingUserAction:
            //            // Ask user to confirm the installation
            //            var confirmIntent = (Intent)extras.Get(Intent.ExtraIntent);
            //            StartActivity(confirmIntent);
            //            break;
            //        case (int)PackageInstallStatus.Success:
            //            //TODO: Handle success
            //            break;
            //        case (int)PackageInstallStatus.Failure:
            //        case (int)PackageInstallStatus.FailureAborted:
            //        case (int)PackageInstallStatus.FailureBlocked:
            //        case (int)PackageInstallStatus.FailureConflict:
            //        case (int)PackageInstallStatus.FailureIncompatible:
            //        case (int)PackageInstallStatus.FailureInvalid:
            //        case (int)PackageInstallStatus.FailureStorage:
            //            //TODO: Handle failures
            //            break;
            //    }
                //base.OnNewIntent(intent);
            //}
        }
        
    }
}