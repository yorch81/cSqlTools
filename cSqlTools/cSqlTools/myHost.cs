using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy.Hosting.Self;

namespace cSqlTools
{
    /// <summary>
    /// Run Web Application on localhost
    /// </summary>
    class myHost
    {
        /// <summary>
        /// Constructor of Class
        /// </summary>
        /// <param name="port">
        /// Web Application Port
        /// </param>
        public myHost(int port)
        {
            string url = "http://localhost:" + port;

            HostConfiguration config = new HostConfiguration();
            config.UrlReservations.CreateAutomatically = true;
            config.UrlReservations.User = "Everyone";

            NancyHost host = new NancyHost(config, new Uri(url));

            host.Start();

            Console.WriteLine("Listening on: " + url);
            Console.WriteLine("Enter 'quit' to Terminate Web Application");

            string command = "";

            while (command != "quit")
                command = Console.ReadLine();

            host.Stop();
        }
    }
}
