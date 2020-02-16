using System;

namespace KnowledgeBase.Api.Models
{
    public class InformationModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
