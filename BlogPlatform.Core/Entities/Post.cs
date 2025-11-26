using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogPlatform.Core.Entities
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Author { get; set; } = "admin";

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        // ICollection represents a collection of related Comment entities
        // one Post can have multiple Comments
    }
}