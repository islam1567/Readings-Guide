using Readings_Guide.Cores.Entities;

namespace Readings_Guide.Cores.Dtos
{
    public class NotesDto
    {
        public int Id { get; set; }
        public string? NoteContent { get; set; }
        public int? PageNumber { get; set; }
        public string UserId { get; set; }
    }
}
