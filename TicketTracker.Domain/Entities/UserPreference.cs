using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class UserPreference
    {
        public int UserProfileId {  get; set; }
        public int PreferenceTypeId { get; set; }
        public string? Value { get; set; }

        //ef

        public UserPreferenceType? PreferenceType { get; set;}
        public UserProfile? UserProfile { get; set;}

    }
}
