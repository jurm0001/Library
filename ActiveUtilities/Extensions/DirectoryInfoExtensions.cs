using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Utilities.Extensions
{
    public static class DirectoryInfoExtensions
    {
        public static void Copy(this DirectoryInfo sourceDir, string destDirName, bool overwrite, bool copySubDirs)
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
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, overwrite);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryInfo sub = new DirectoryInfo(subdir.FullName);
                    sub.Copy(temppath, overwrite, copySubDirs);                    
                }
            }
        }
    }
}
