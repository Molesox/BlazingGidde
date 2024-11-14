﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingGidde.Shared.Models.Identity
{
	public class RegisterResult
	{
		public bool Successful { get; set; }
		public IEnumerable<string>? Errors { get; set; }
	}
}
