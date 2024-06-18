

namespace fotoservice.data.models;
public class AppUser: IdentityUser<int>

    {
        public int Hospital_id { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? Gender { get; set; }
        public string? PhotoUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public DateTime PaidTill { get; set; }
       
    }

