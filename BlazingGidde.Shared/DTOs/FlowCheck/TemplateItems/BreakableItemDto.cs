using BlazingGidde.Shared.Models.FlowCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingGidde.Shared.DTOs.FlowCheck.TemplateItems
{
    public class BreakableItemDto : IReadDto<int>
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
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
    }
}
