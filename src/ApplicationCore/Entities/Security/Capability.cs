using System;
using System.Collections.Generic;
using System.Text;

namespace Vnit.ApplicationCore.Entities.Security
{
   
    public static class Capability<T>
    {
        public static string Insert
        {
            get { return "Insert." + typeof(T).Name; }
        }
        public static string Update
        {
            get { return "Update." + typeof(T).Name; }
        }
        public static string Delete
        {
            get { return "Delete." + typeof(T).Name; }
        }
        public static string View
        {
            get { return "View." + typeof(T).Name; }
        }
        public static string Print
        {
            get { return "Print." + typeof(T).Name; }
        }
    }
}
