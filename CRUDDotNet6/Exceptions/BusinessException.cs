using System;
using CRUDDotNet6.Constants;

namespace CRUDDotNet6.Exceptions
{
	public class BusinessException: BaseException
	{
        public int code { get; set; } = (int)StatusCodeEnums.BUSINESS_EXCEPTION;
        public BusinessException(string message): base(message)
		{
		}
	}
}

