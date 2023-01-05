using System;
using System.ComponentModel.DataAnnotations;

namespace CRUDDotNet6.Utils
{
	public static class Helper
	{
		public static bool Validate<T>(T obj, out ICollection<ValidationResult> results)
		{
			results = new List<ValidationResult>();
			// returns true or false
			return Validator.TryValidateObject(obj, new ValidationContext(obj), results, true);

		}
	}
}

