using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Logging.Log4net
{
    public static class FileHelper
    {
        // Methods
        public static List<FileInfo> GetAllFile(string filename)
        {
            #region Contracts

            if (string.IsNullOrEmpty(filename) == true) throw new ArgumentException();

            #endregion

            // Result
            var resultFileDictionary = new Dictionary<string, FileInfo>();

            // SearchPatternList
            var searchPatternList = filename.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (searchPatternList == null) throw new InvalidOperationException();

            // EntryDirectory           
            foreach (var searchPattern in searchPatternList)
            {
                // SearchFile 
                var searchFileDirectory = new DirectoryInfo(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location));
                var searchFileList = searchFileDirectory?.GetFiles(searchPattern, SearchOption.AllDirectories);
                if (searchFileList == null) throw new InvalidOperationException();

                // Add
                foreach (var searchFile in searchFileList)
                {
                    if (resultFileDictionary.ContainsKey(searchFile.Name.ToLower()) == false)
                    {
                        resultFileDictionary.Add(searchFile.Name.ToLower(), searchFile);
                    }
                }
            }

            // CurrentDirectory         
            foreach (var searchPattern in searchPatternList)
            {
                // SearchFile 
                var searchFileDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
                var searchFileList = searchFileDirectory?.GetFiles(searchPattern, SearchOption.AllDirectories);
                if (searchFileList == null) throw new InvalidOperationException();

                // Add
                foreach (var searchFile in searchFileList)
                {
                    if (resultFileDictionary.ContainsKey(searchFile.Name.ToLower()) == false)
                    {
                        resultFileDictionary.Add(searchFile.Name.ToLower(), searchFile);
                    }
                }
            }

            // Return
            return resultFileDictionary.Values.ToList();
        }
    }
}
