namespace HeraAI.API.Models
{

    public class User : BaseEntity
    {

        public string? Id { get; set; }

        public string? CountryId { get; set; }

        public string? Language { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Phone { get; set; }

        public Dictionary<string, object> MetaInfo { get; set; } = new Dictionary<string, object>();


        public override bool Equals(object obj)
        {

            if (obj is null || obj == DBNull.Value)
                return false;

            return (this.Id == ((User)obj).Id);

        }


        public static bool operator ==(User user1, User user2)
        {

            if (user1 is null && user2 is null)
                return true;
            else if (user1 is null ^ user2 is null)
                return false;

            return user1.Equals(user2);

        }


        public static bool operator !=(User user1, User user2)
        {

            if (user1 is null && user2 is null)
                return false;
            else if (user1 is null ^ user2 is null)
                return true;

            return !user1.Equals(user2);

        }


        public override int GetHashCode()
        {

            int baseHash = 359;

            return baseHash * this.Id.GetHashCode();

        }

    }

}
