using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace RentMyWrox.Models
{
    public class Hobby
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<UserDemographics> UserDemographics { get; set; }
    }
}