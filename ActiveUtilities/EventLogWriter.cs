using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;

namespace DataConnections
{
    public static class EventLogWriter
    {

        private static string _eventLogSource;
        public static string EventLogSource
        {
            get { return EventLogWriter._eventLogSource; }
            set { EventLogWriter._eventLogSource = value; }
        }
        private static string _eventLogName;
        public static string EventLogName
        {
            get { return EventLogWriter._eventLogName; }
            set { EventLogWriter._eventLogName = value; }
        }

        static EventLogWriter()
        {
            setup();
        }
        
        public static void setup()
        {
            // Create the source, if it does not already exist. 
            if (_eventLogSource == "" || _eventLogName == "")
                throw new Exception("Source and / or log name not set");                

            if (!EventLog.SourceExists(_eventLogSource))
            {
                //An event log source should not be created and immediately used. 
                //There is a latency time to enable the source, it should be created 
                //prior to executing the application that uses the source. 
                //Execute this sample a second time to use the new source.
                try
                {
                    EventLog.CreateEventSource(_eventLogSource, _eventLogName);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                // The source is created.  Exit the application to allow it to be registered. 
                return;
            }
        }

        /// <summary>
        /// writes an information log entry
        /// </summary>
        /// <param name="message"></param>
        public static void WriteLog(string message)
        {
            EventLog myLog = new EventLog();
            myLog.Source = _eventLogSource;
            myLog.WriteEntry(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        public static void WriteLog(string message, EventLogEntryType type)
        {
            EventLog myLog = new EventLog();
            myLog.Source = _eventLogSource;
            myLog.WriteEntry(message, type);            
        }
    }
}
