﻿namespace Readings_Guide.Cores.Dtos
{
    public class BookWithoutIdDto
    {
        public string Title { get; set; }
        public string? Descreption { get; set; }
        public string? Auther { get; set; }
        public DateTime? PublicTime { get; set; }
        public string? ImageUrl { get; set; }
        public int CatagoryId { get; set; }
    }
}
