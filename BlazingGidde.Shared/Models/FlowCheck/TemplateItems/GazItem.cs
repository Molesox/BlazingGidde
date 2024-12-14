using System;

namespace BlazingGidde.Shared.Models.FlowCheck.TemplateItems;

public class GazItem : TemplateItem
{
    public decimal O2 { get; set; }
    public decimal N2 { get; set; }
    public decimal CO2 { get; set; }
    public bool? IsGasOk { get; set; }
    public bool? IsSealedOk { get; set; }


}
