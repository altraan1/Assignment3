using System;
using System.ComponentModel.DataAnnotations;

namespace BlogPlatform.Core.Entities
{
    public class Comment
    {
        [Key] //primary key
        public int Id { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(150)]
        public string Email { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public Post Post { get; set; } //Navigation property to parent Post
    }
}