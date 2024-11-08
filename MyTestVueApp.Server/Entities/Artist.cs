﻿namespace MyTestVueApp.Server.Entities
{
    public class Artist
    {
        public int Id { get; set; } //Public id
        public string SubId { get; set; } //SECRET ID
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
