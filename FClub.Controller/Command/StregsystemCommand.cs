using FClub.Core;
using System;
using System.Linq;
using System.Reflection;

namespace FClub.Controller.Command
{
	internal class StregsystemCommand : IStregsystemCommand
	{
		private readonly string m_name;
		private MethodInfo m_endpoint;

		public StregsystemCommand(string name, MethodInfo endpoint)
		{
			m_name = name;
			m_endpoint = endpoint;
		}

		private ParameterInfo[] ParameterInfos => m_endpoint.GetParameters();

		public IStregsystemCommandResult Run(object thisRef, string input)
		{
			try
			{
				return Run(thisRef, GetParamsFromInput(input));
			}
			catch
			{
				return new Error("Noget gik galt da kommando forsøgte at blive kørt");
			}
		}

		public IStregsystemCommandResult Run(object thisRef, object[] parameters)
		{
			return (IStregsystemCommandResult)m_endpoint.Invoke(thisRef, parameters);
		}

		public object[] GetParamsFromInput(string input)
		{
			try
			{
				return ParseParamsFromInput(input);
			}
			catch
			{
				return default;
			}
		}

		private object[] ParseParamsFromInput(string input)
		{
			string[] _split = SplitString(input);
			int _defaultCount = ParameterInfos.Count(info => info.HasDefaultValue);
			if (_split.Length + _defaultCount < ParameterInfos.Length)
			{
				throw new ArgumentException("Split length is too small", nameof(input));
			}

			object[] _parameters = new object[ParameterInfos.Length];

			for (int i = 0; i < ParameterInfos.Length; i++)
			{
				/* parse splits until no more.
				 * Defaults are always at the end (language rule) */
				if (i < _split.Length &&
					ParseParameter(_split[i], ParameterInfos[i].ParameterType, out object result))
				{
					_parameters[i] = result;
				}
				else if (i >= _split.Length &&
					ParameterInfos[i].HasDefaultValue)
				{
					_parameters[i] = ParameterInfos[i].DefaultValue;
				}
				else
				{
					return default;
				}
			}

			return _parameters;
		}

		public bool Match(string name, string input)
		{
			try
			{
				return name == m_name && GetParamsFromInput(input) != default;
			}
			catch
			{
				return false;
			}
		}

		private bool ParseParameter(string input, Type type, out object result)
		{
			return (result = ParseParameter(input, type)) != null;
		}

		private object ParseParameter(string input, Type type)
		{
			return Convert.ChangeType(input, type);
		}

		private string[] SplitString(string input)
		{
			const string _separator = " ";
			return input.Split(_separator, StringSplitOptions.RemoveEmptyEntries);
		}
	}
}
