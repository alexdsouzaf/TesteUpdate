using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TesteUpdate.Droid
{
    public class AndroidService : IApkService
    {
        public void install(string pPath)
        {

            try
            {
                Java.IO.File file = new Java.IO.File(pPath);
                Android.Net.Uri apkUri = Android.Net.Uri.FromFile(file);
                InstallPackageAndroidQAndAbove(apkUri);

            }
            catch (Exception ex)
            {
                string erro = ex.Message; //“Could not find a part of the path” ultimo erro obtido
            }

        }
        private static void AddApkToInstallSession(Context context, Android.Net.Uri apkUri, PackageInstaller.Session session)
        {
            var packageInSession = session.OpenWrite("package", 0, -1);

            var input = context.ContentResolver.OpenInputStream(apkUri);
            //FileStream ms = new FileStream(apkUri.Path, FileMode.Open, FileAccess.ReadWrite);

            //using (Stream fileStream = File.OpenRead(apkUri.Path))
            //using (Stream fileStream = new FileStream(apkUri.Path, FileMode.Open, FileAccess.ReadWrite))
            //using (Stream sessionStream = session.OpenWrite($"Session-{Android.App.Application.Context.PackageName}", 0, fileStream.Length))
            //{
            //    fileStream.CopyToAsync(sessionStream).ConfigureAwait(false);
            //    sessionStream.FlushAsync().ConfigureAwait(false);
            //}
            try
            {
                if (input != null)
                {
                    input.CopyTo(packageInSession);
                }
                else
                {
                    throw new Exception("Inputstream is null");
                }
            }
            finally
            {
                packageInSession.Close();
                input.Close();
            }

            //That this is necessary could be a Xamarin bug.
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
        private void InstallPackageAndroidQAndAbove(Android.Net.Uri apkUri)
        {
            var packageInstaller = Android.App.Application.Context.PackageManager.PackageInstaller;
            var sessionParams = new PackageInstaller.SessionParams(PackageInstallMode.FullInstall);
            int sessionId = packageInstaller.CreateSession(sessionParams);
            var session = packageInstaller.OpenSession(sessionId);

            AddApkToInstallSession(Android.App.Application.Context, apkUri, session);

            // Create an install status receiver.
            var intent = new Intent(Android.App.Application.Context, Android.App.Application.Context.Class);
            intent.SetAction(Intent.ActionInstallPackage);
            intent.SetDataAndType(apkUri, "application/vnd.android.package-archive");
            intent.PutExtra(Intent.ExtraNotUnknownSource, true);
            var pendingIntent = PendingIntent.GetActivity(Android.App.Application.Context, 0, intent, 0);
            var statusReceiver = pendingIntent.IntentSender;

            // Commit the session (this will start the installation workflow).
            session.Commit(statusReceiver);
  
        
        }

        public string GetPath()
        {
            var pathFile = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);
            var absolutePath = pathFile.AbsolutePath;
            //string directory = Path.Combine(Android.App.Application.Context.GetExternalFilesDir("").AbsolutePath, Android.OS.Environment.DirectoryDownloads); //nao ta pegando a pasta certa
            return Path.Combine (absolutePath, "Teste.apk");

        }
    }
}