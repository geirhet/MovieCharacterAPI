using System.Collections.Generic;

namespace MovieCharacterAPI.Models.DTOs.Character
{
    public class CharacterReadDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Alias { get; set; }
        public string Gender { get; set; }
        public string Picture { get; set; }
        public ICollection<int> Movies { get; set; }
    }
}
