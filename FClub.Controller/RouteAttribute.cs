using System;

namespace FClub.Controller
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public class RouteAttribute : Attribute
	{
		public string Path { get; }

		public RouteAttribute(string path)
		{
			Path = path;
		}
	}
}
