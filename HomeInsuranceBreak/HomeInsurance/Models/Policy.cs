using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeInsurance.Models
{

	public class Policy
	{

		public int Id { get; set; }

		[Display(Name = "Quote Id")]
		[ForeignKey("Quote")]
		public int QuoteId { get; set; }

		public Quote Quote { get; set; }

		[Display(Name = "Policy Key")]
		[StringLength(10)]
		[Required]
		public string PolicyKey { get; set; }

		[Display(Name = "Policy Effective Date")]
		[StringLength(10)]
		[Required]
		public string PolicyEffDate { get; set; }

		[Display(Name = "Policy End Date")]
		[StringLength(10)]
		[Required]
		public string PolicyEndDate { get; set; }

		[Display(Name = "Policy Term")]
		public int PolicyTerm { get; set; }

		[Display(Name = "Policy Status")]
		[StringLength(10)]
		[Required]
		public string PolicyStatus { get; set; }		// ACTIVE, PENDING, RENEWED, CANCELLED
	}
}