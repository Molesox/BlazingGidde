using BlazingGidde.Shared.Models.FlowCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingGidde.Shared.DTOs.FlowCheck.TemplateItems.Request
{
    public class CreateGazItemDto : ICreateDto<int>
    {
        public int Id { get; set; }
        /// <summary>
        /// The parent template id
        /// </summary>
        public int TemplateId { get; set; }

        /// <summary>
        /// Gets or sets the success status of the control item.
        /// </summary>
        public bool? Success { get; set; }

        public string? Traceability { get; set; } = string.Empty;

        public string? Line { get; set; }

        /// <summary>
        /// Gets or sets the parent template.
        /// </summary>
        public Template Template { get; set; }

        /// <summary>
        /// Gets or sets the possible related incidency.
        /// </summary>
        public Incidency? Incidency { get; set; }

        public decimal O2 { get; set; }
        public decimal N2 { get; set; }
        public decimal CO2 { get; set; }
        public bool? IsGasOk { get; set; }
        public bool? IsSealedOk { get; set; }
       
    }
}
