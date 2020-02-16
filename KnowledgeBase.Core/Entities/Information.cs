using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnowledgeBase.Core.Entitties
{
    public class Information
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
