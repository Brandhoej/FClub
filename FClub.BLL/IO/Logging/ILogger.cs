using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.BLL.IO.Logging
{
	internal interface ILogger<in T>
	{
		void Log(T transaction);
	}
}
