using System.ComponentModel.DataAnnotations;

namespace HomeInsurance.Models
{

	public class MaxStringLength : StringLengthAttribute
	{

		public MaxStringLength(int length) : base(length)
		{
			ErrorMessage = "{0} length cannot be more than {1} characters.";
		}
	}
}