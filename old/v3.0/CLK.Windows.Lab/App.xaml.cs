using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CLK.Windows.Lab
{
    public partial class App : System.Windows.Application
    {
        // Constructors
        public App()
        {
            // Startup
            this.Startup += (s, e) =>
            {
                // Run
                CLK.Windows.Application.Run(new MainWindow());
            };
        }
    }
}
