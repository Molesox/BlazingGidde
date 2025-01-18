using BlazingGidde.Shared.Models.FlowCheck;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingGidde.Shared.DTOs.FlowCheck.Request
{
    public class CreateTemplateDto : ICreateDto<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime? UpdateDate { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        [MaxLength(256)]
        public string CreateUser { get; set; } = string.Empty;

        [MaxLength(256)]
        public string? UpdateUser { get; set; }

        public int TemplateKindId { get; set; }

        public List<TemplateItem>? TemplateItems { get; set; }
    }
}
