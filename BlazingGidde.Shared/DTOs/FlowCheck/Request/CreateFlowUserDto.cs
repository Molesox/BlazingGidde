using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingGidde.Shared.DTOs.FlowCheck.Request
{
	public record CreateFlowUserDto : ICreateDto<string>
	{

		public string Id { get; set; }

		[Required]
		[StringLength(20)]
		public string PersonCulture { get; set; }

		[StringLength(80)]
		[Required]
		public string PersonTitle { get; set; }

		[StringLength(80)]
		[Required]
		public string PersonLastName { get; set; }

		[StringLength(80)]
		[Required]
		public string PersonFirstName { get; set; }

		[EmailAddress]
		[Required]
		public string Email { get; set; }

		[Phone]
		[Required]
		public string PhoneNumber { get; set; }


	}
}
