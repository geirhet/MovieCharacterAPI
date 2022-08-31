using System.Collections.Generic;

namespace MovieCharacterAPI.Models.DTOs.Franchise
{
    public class FranchiseReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<int> Movies { get; set; }
    }
}
