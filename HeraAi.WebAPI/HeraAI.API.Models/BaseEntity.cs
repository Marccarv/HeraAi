namespace HeraAI.API.Models
{

    public class BaseEntity
    {

        public bool? Inactive { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdate { get; set; }

        public string? LastUserId { get; set; }

        public bool Updating { get; set; }

        public int? RecordCount { get; set; }

    }

}
