﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingGidde.Shared.Models
{
    public class EmailViewModel
    {
        public string Subject { get; set; }
        public string Greeting { get; set; }
        public string Body { get; set; }
        public string Signature { get; set; }
    }
}
