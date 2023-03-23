using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Shared.Helper.Funcs
{
    public static class CommonFuncs
    {
        /// <summary>
        /// Referenced from https://stackoverflow.com/questions/2953254/cgetting-all-image-files-in-folder?
        /// </summary>
        /// <param name="searchFolder"></param>
        /// <param name="filters"></param>
        /// <param name="isRecursive"></param>
        /// <returns></returns>
        public static string[] GetFilesFromFolder(string searchFolder, string[] filters, bool isRecursive)
        {
            List<string> filesFound = new List<string>();
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, string.Format("*.{0}", filter), searchOption));
            }
            return filesFound.ToArray();
        }
    }
}
