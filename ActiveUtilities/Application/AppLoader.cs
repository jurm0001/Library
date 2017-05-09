using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Utilities.Extensions;
namespace Utilities.Application
{
    public partial class AppLoader : Component
    {

        private string _LocalRootPath;
        private string _REmoteRootPath;

        private string _LocalFile;
        private string _RemoteFile;

        

        public AppLoader()
        {
            InitializeComponent();
        }

        public AppLoader(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }


        public bool UpdateApplication(string localRootPath, string remoteRootPath, string localFile, string remoteFile, bool createLocal)
        {
            if (Directory.Exists(localRootPath))
            {
                string localFileVersion = GetFileVersion(localRootPath + localFile);
                string remoteFileVersion = GetFileVersion(remoteRootPath + remoteFile);
                if(UpdateNeeded(localFileVersion, remoteFileVersion))
                {
                    DirectoryInfo remoteDir = new DirectoryInfo(remoteRootPath);
                    remoteDir.Copy(localRootPath, true, true);
                }
            }
            else
            {
                if (createLocal)
                    Directory.CreateDirectory(localRootPath);
                DirectoryInfo remoteDir = new DirectoryInfo(remoteRootPath);
                remoteDir.Copy(localRootPath, true, true);
            }
            return false;
        }

        private string GetFileVersion(string file)
        {
            // Get the file version for the notepad.
            FileVersionInfo myFileVersionInfo;
            try
            {
                myFileVersionInfo = FileVersionInfo.GetVersionInfo(file);
            }
            catch
            {
                return "Err";
            }

            // Print the file name and version number.
            String text = "File: " + myFileVersionInfo.FileDescription + '\n' +
            "Version number: " + myFileVersionInfo.FileVersion;
            Console.WriteLine(text);
            return myFileVersionInfo.FileVersion;
        }

        private bool UpdateNeeded(string currentVer, string remoteVer)
        {
            string[] currToks = currentVer.Split(new char[] { '.' });
            string[] remoteToks = remoteVer.Split(new char[] { '.' });

            if (currToks.Length != remoteToks.Length)
                throw new Exception("Versioning Issue. " + currentVer + " " + remoteVer);

            for (int i = 0; i < currToks.Length; ++i)
            {
                int curr = int.Parse(currToks[i]);
                int rem = int.Parse(remoteToks[i]);
                if (rem > curr)
                    return true;
            }


            return false;
        }

        private void Copy(DirectoryInfo sourceDir, string destDirName, bool overwrite, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            //DirectoryInfo dir = sourceDir;
            DirectoryInfo[] dirs = sourceDir.GetDirectories();

            if (!sourceDir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDir.Name);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = sourceDir.GetFiles();
            foreach (FileInfo file in files)
            {
                System.Windows.Forms.Application.DoEvents();
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, overwrite);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    System.Windows.Forms.Application.DoEvents();
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryInfo sub = new DirectoryInfo(subdir.FullName);
                    Copy(sub, temppath, overwrite, copySubDirs);
                }
            }
        }

        public string GetRemoteLocation()
        {
            return "";
        }
    }
}
