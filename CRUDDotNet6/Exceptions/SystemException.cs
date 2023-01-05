using System;
using CRUDDotNet6.Constants;

namespace CRUDDotNet6.Exceptions
{
	public class SystemException: BaseException
	{

		public int code { get; set; } = (int) StatusCodeEnums.SYSTEM_EXCEPTION;

		public SystemException(string message): base(message)
		{
			
		}
	}
}

