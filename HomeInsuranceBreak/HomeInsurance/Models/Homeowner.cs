using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeInsurance.Models
{

	public class Homeowner
	{

		public int Id { get; set; }

		[ForeignKey("User")]
		public int UserId { get; set; }

		[Required]
		[MaxStringLength(30)]
		[IsAlphanumeric]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Required]
		[MaxStringLength(30)]
		[IsAlphanumeric]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Required]
        //[DataType(DataType.Date)]
        [RegularExpression(@"^\d{4}-((0\d)|(1[012]))-(([012]\d)|3[01])$", ErrorMessage = "Date of birth must be formatted as YYYY-MM-DD")]
        //[RegularExpression(@"((([0-9]‌‌[0-9]‌‌[0-9]‌‌[1-9]‌‌)|([1-9]‌‌[0-9]‌‌[0-9]‌‌[0-9]‌‌)|([0-9]‌‌[1-9]‌‌[0-9]‌‌[0-9]‌‌)|([0-9]‌‌[0-9]‌‌[1-9]‌‌[0-9]‌‌))-((0[13578]‌‌)|(1[02]‌‌))-((0[1-9]‌‌)|([12]‌‌[0-9]‌‌)|(3[01]‌‌)))|((([0-9]‌‌[0-9]‌‌[0-9]‌‌[1-9]‌‌)|([1-9]‌‌[0-9]‌‌[0-9]‌‌[0-9]‌‌)|([0-9]‌‌[1-9]‌‌[0-9]‌‌[0-9]‌‌)|([0-9]‌‌[0-9]‌‌[1-9]‌‌[0-9]‌‌))-((0[469]‌‌)|11)-((0[1-9]‌‌)|([12]‌‌[0-9]‌‌)|(30)))|(((000[48]‌‌)|([0-9]‌‌0-9)|([0-9]‌‌[1-9]‌‌[02468]‌‌[048]‌‌)|([1-9]‌‌[0-9]‌‌[02468]‌‌[048]‌‌))-02-((0[1-9]‌‌)|([12]‌‌[0-9]‌‌)))|((([0-9]‌‌[0-9]‌‌[0-9]‌‌[1-9]‌‌)|([1-9]‌‌[0-9]‌‌[0-9]‌‌[0-9]‌‌)|([0-9]‌‌[1-9]‌‌[0-9]‌‌[0-9]‌‌)|([0-9]‌‌[0-9]‌‌[1-9]‌‌[0-9]‌‌))-02-((0[1-9]‌‌)|([1]‌‌[0-9]‌‌)|([2]‌‌[0-8])))",
        //    ErrorMessage = "DOB must be formatted as yyyy-mm-dd")]
        [Display(Name = "Date of Birth")]
		public string DOB { get; set; }

		[Required]
		[Display(Name = "Are you retired?")]
		public bool IsRetired { get; set; }

		[Required]
		[RegularExpression(@"^\d{3}-\d{2}-\d{4}$", ErrorMessage = "SSN must be formatted as NNN-NN-NNNN")]
		[Display(Name = "Social Security Number")]
		public string SSN { get; set; }

		[Required]
		[MaxStringLength(50)]
		[EmailAddress]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
+"@"
+ @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = "E-Mail Address format should be name@domain.com")]
        [Display(Name = "Email Address")]
		public string EmailAddress { get; set; }

		public User User { get; set; }
	}
}