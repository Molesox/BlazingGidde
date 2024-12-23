﻿using BlazingGidde.Shared.Models.FlowCheck.TemplateItems;

namespace BlazingGidde.Client.Services.FlowCheck.TemplateItems
{
    public class BreakeableItemManager : APIRepository<BreakeableItem, int>
    {
        public BreakeableItemManager(HttpClient _http)
            : base(_http, "BreakeableItem")
        { }
    }
}
