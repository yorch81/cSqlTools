using System;
using Nancy.Hosting.Self;

/**
 * myHost 
 *
 * myHost Init Web Server for Application
 *
 * Copyright 2015 Jorge Alberto Ponce Turrubiates
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
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

			NancyHost host = null;

			// If is Windows requires Administrator Permissions
			if (System.Environment.OSVersion.ToString ().Contains ("Windows")) {
				HostConfiguration config = new HostConfiguration();
				config.UrlReservations.CreateAutomatically = true;
				config.UrlReservations.User = "Everyone";

				host = new NancyHost(config, new Uri(url));
			} 
			else 
			{
				host = new NancyHost(new Uri(url));
			}

            host.Start();

            Console.WriteLine("Listening on: " + url);
            Console.WriteLine("Enter 'quit' to Terminate Web Application");

            // Load Default Browser
            System.Diagnostics.Process.Start(url);

            string command = "";

            while (command != "quit")
                command = Console.ReadLine();

            host.Stop();
        }
    }
}
