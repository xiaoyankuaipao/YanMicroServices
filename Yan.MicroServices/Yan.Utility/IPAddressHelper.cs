using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Yan.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class IPAddressHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIP()
        {
            try
            {
                string ip = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()
                    .Select(p => p.GetIPProperties())
                    .SelectMany(p => p.UnicastAddresses)
                    .Where(p => p.Address.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(p.Address))
                    .FirstOrDefault()?.Address.ToString();

                return ip;
            }
            catch (Exception)
            {
                return "";
            }

        }
    }
}
