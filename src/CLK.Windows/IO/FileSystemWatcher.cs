using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CLK.IO
{
    public class FileSystemWatcher : IDisposable
    {
        // Fields
        private readonly object _syncRoot = new object();

        private readonly string _watchPath = null;

        private readonly string _watchFilter = null;

        private readonly Regex _watchFilterRegex = null;

        private readonly List<string> _creatingFileList = new List<string>();

        private System.IO.FileSystemWatcher _fileSystemWatcher = null;


        // Constructors
        public FileSystemWatcher(string watchPath) : this(watchPath, "*.*") { }

        public FileSystemWatcher(string watchPath, string watchFilter)
        {
            #region Contracts

            if (string.IsNullOrEmpty(watchPath) == true) throw new ArgumentException();

            #endregion

            // Default
            _watchPath = watchPath;
            _watchFilter = watchFilter;
            _watchFilterRegex = new Regex("^" + Regex.Escape(_watchFilter).Replace(@"\*", ".*").Replace(@"\?", ".") + "$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }

        public void Start()
        {
            // Start
            _fileSystemWatcher = new System.IO.FileSystemWatcher();
            _fileSystemWatcher.Path = _watchPath;
            _fileSystemWatcher.Filter = _watchFilter;
            _fileSystemWatcher.Created += this.FileSystemWatcher_Creating;
            _fileSystemWatcher.Changed += this.FileSystemWatcher_Changed;
            _fileSystemWatcher.Deleted += this.FileSystemWatcher_Deleted;
            _fileSystemWatcher.Renamed += this.FileSystemWatcher_Renamed;
            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        public void Dispose()
        {
            // Dispose
            if (_fileSystemWatcher != null)
            {
                _fileSystemWatcher.Dispose();
                _fileSystemWatcher.EnableRaisingEvents = false;
                _fileSystemWatcher.Created -= this.FileSystemWatcher_Creating;
                _fileSystemWatcher.Changed -= this.FileSystemWatcher_Changed;
                _fileSystemWatcher.Deleted -= this.FileSystemWatcher_Deleted;
            }
        }


        // Methods
        private void ConfirmCreated(string fullPath)
        {
            #region Contracts

            if (string.IsNullOrEmpty(fullPath) == true) throw new ArgumentException();

            #endregion

            // Sync
            lock (_syncRoot)
            {
                // Require
                if (File.Exists(fullPath) == false) return;
                if (_creatingFileList.Contains(fullPath) == false) return;
            }

            // Try
            try
            {
                File.OpenWrite(fullPath).Close();
            }
            catch
            {
                return;
            }

            // Sync
            lock (_syncRoot)
            {
                // Require
                if (File.Exists(fullPath) == false) return;
                if (_creatingFileList.Contains(fullPath) == false) return;

                // Remove
                _creatingFileList.Remove(fullPath);
            }

            // Notify
            this.OnCreated(new FileSystemEventArgs(WatcherChangeTypes.Created, Path.GetDirectoryName(fullPath), Path.GetFileName(fullPath)));
        }


        // Handlers
        private void FileSystemWatcher_Creating(object sender, FileSystemEventArgs e)
        {
            #region Contracts

            if (e == null) throw new ArgumentException();

            #endregion

            // Sync
            lock (_syncRoot)
            {
                // Require
                if (File.Exists(e.FullPath) == false) return;
                if (_creatingFileList.Contains(e.FullPath) == true) return;

                // Add
                _creatingFileList.Add(e.FullPath);
            }
        }

        private void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs eventArgs)
        {
            #region Contracts

            if (eventArgs == null) throw new ArgumentException();

            #endregion

            // Sync
            lock (_syncRoot)
            {
                // Require
                if (_creatingFileList.Contains(eventArgs.FullPath) == false) return;

                // Remove
                _creatingFileList.Remove(eventArgs.FullPath);
            }
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs eventArgs)
        {
            #region Contracts

            if (eventArgs == null) throw new ArgumentException();

            #endregion

            // Confirm
            this.ConfirmCreated(eventArgs.FullPath);
        }

        private void FileSystemWatcher_Renamed(object sender, RenamedEventArgs eventArgs)
        {
            #region Contracts

            if (eventArgs == null) throw new ArgumentException();

            #endregion

            // Sync
            lock (_syncRoot)
            {
                // Remove Old
                if (_creatingFileList.Contains(eventArgs.OldFullPath) == true)
                {
                    _creatingFileList.Remove(eventArgs.OldFullPath);
                }

                // Add New
                if (_creatingFileList.Contains(eventArgs.FullPath) == false)
                {
                    if (_watchFilterRegex.IsMatch(eventArgs.Name) == true)
                    {
                        _creatingFileList.Add(eventArgs.FullPath);
                    }
                }
            }

            // Confirm
            this.ConfirmCreated(eventArgs.FullPath);
        }


        // Events
        public event Action<FileSystemEventArgs> Created;
        private void OnCreated(FileSystemEventArgs eventArgs)
        {
            #region Contracts

            if (eventArgs == null) throw new ArgumentException();

            #endregion

            // Raise
            var handler = this.Created;
            if (handler != null)
            {
                handler(eventArgs);
            }
        }
    }
}