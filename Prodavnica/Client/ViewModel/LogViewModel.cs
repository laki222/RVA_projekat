using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    class LogEntry
    {
        public LogLevel Level { get; set; }
        public string Time { get; set; }
        public string Message { get; set; }
    }


    class LogViewModel:ViewModelBase
    {
        public ObservableCollection<LogEntry> Entries { get; set; }



        public LogViewModel()
        {
            Entries = new ObservableCollection<LogEntry>();

            RefreshTable();
            ClientLogger.OnMessageLogged += RefreshTable;
        }

        private void RefreshTable()
        {
            Entries.Clear();
            string[] lines = File.ReadAllLines("ClientActionsLog.txt");

            foreach (string line in lines)
            {
                LogEntry entry = new LogEntry();
                entry.Time = line.Split('[')[0];


                Match regex = Regex.Match(line, @"\[[a-zA-Z]+\]");

                switch (regex.Value)
                {
                    case "[INFO]":
                        entry.Level = LogLevel.Info;
                        break;
                    case "[DEBUG]":
                        entry.Level = LogLevel.Debug;
                        break;
                    case "[WARNING]":
                        entry.Level = LogLevel.Warning;
                        break;
                    case "[ERROR]":
                        entry.Level = LogLevel.Error;
                        break;
                    case "[CRITICAL]":
                        entry.Level = LogLevel.Critical;
                        break;
                }

                entry.Message = line.Split(':')[3];

                Entries.Add(entry);
            }
        }
    }
}
