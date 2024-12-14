namespace BlazingGidde.Shared.Models.Identity
{
    public class TreeNodeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Role name or User name
        public string? Email { get; set; } // Email, only relevant for user nodes
        public int? ParentId { get; set; } // Null for top-level roles, role Id for users
    }
}
