using Sports.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace Sports.Classes
{
    public static class Manager
    {
        public static SportEntities1 DB { get; set; }
        public static Frame MainFrame { get; set; }
        public static Page currentPage;
        public static Users User { get; set; }
        public static bool IsGuest => User == null;
        public static Roles[] Roles { get; set; }
    }
}
