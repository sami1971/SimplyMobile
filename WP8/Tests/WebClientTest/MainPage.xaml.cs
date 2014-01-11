using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Resources;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SimplyMobile.Text.ServiceStack;
using SimplyMobile.Web;
using WebClientTest.Resources;
using WebClientTests;

namespace WebClientTest
{
    public partial class MainPage : PhoneApplicationPage
    {
        private WebHybrid webHybrid;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            this.webHybrid = new WebHybrid(this.webView, new JsonSerializer());

            SaveFilesInHTMLFolderToIsoStore();
            this.webView.Navigate(new Uri("HTML/home.html", UriKind.Relative));

            //var fi = new FileInfo(@"./Assets/home.html");

            //using (var streamReader = new StreamReader(fi.FullName))
            //{
            //    this.webView.NavigateToString(streamReader.ReadToEnd());
            //}

            this.webHybrid.RegisterCallback("test", s =>
                {
                    System.Diagnostics.Debug.WriteLine(s);
                    var serializer = new JsonSerializer();
                    var m = serializer.Deserialize<ChartViewModel>(s);
                    System.Diagnostics.Debug.WriteLine(m);
                });

            this.webHybrid.RegisterCallback("dataCallback", s => System.Diagnostics.Debug.WriteLine(s));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //this.webHybrid.CallJsFunction("sendData", new Data() { Name = "Sami", Count = 8 });
            //this.webHybrid.CallJsFunction("sendData", "sami", new Data() { Name = "Sami", Count = 8 });

            this.webHybrid.CallJsFunction("onViewModelData", ChartViewModel.Dummy);
        }

        private static void SaveFilesInHTMLFolderToIsoStore()
        {
#if DEBUG
            // This deletes all existing files in IsolatedStorage - Useful in testing
            // In live should not do this, but only load files once - this speeds subsequent loading of the app
            using (var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                isoStore.Remove();
            }
#endif
            var files = AllFilesInHTMLFolder();

            using (var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                Debug.WriteLine("check for exist " + files[0]);
                if (!isoStore.FileExists(files[0]))
                {
                    foreach (var f in files)
                    {
                        Debug.WriteLine("copy to isolated storage " + f);
                        var sr = Application.GetResourceStream(new Uri(f, UriKind.Relative));

                        // T4 Template includes all files in source folder(s). This may include some which are not in the project
                        if (sr != null)
                        {
                            using (var br = new BinaryReader(sr.Stream))
                            {
                                var data = br.ReadBytes((int)sr.Stream.Length);
                                SaveFileToIsoStore(f, data);
                            }
                        }
                    }
                }
            }
        }

        private static void SaveFileToIsoStore(string fileName, byte[] data)
        {
            string strBaseDir = string.Empty;
            const string DelimStr = "/";
            char[] delimiter = DelimStr.ToCharArray();
            string[] dirsPath = fileName.Split(delimiter);

            // Get the IsoStore
            using (var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                // Recreate the directory structure
                for (var i = 0; i < dirsPath.Length - 1; i++)
                {
                    strBaseDir = Path.Combine(strBaseDir, dirsPath[i]);
                    isoStore.CreateDirectory(strBaseDir);
                }

                // Create the file if not exists
                // or override if exist
                using (var bw = new BinaryWriter(new IsolatedStorageFileStream(fileName,
                        FileMode.Create, FileAccess.Write, FileShare.Write, isoStore)))
                {
                    bw.Write(data);
                }
            }
        }
    }
}