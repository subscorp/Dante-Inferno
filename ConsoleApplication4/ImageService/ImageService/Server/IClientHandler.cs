﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.ImageService.ImageService.Server
{
    interface IClientHandler
    {
        void HandleClient(TcpClient client);
    }
}
