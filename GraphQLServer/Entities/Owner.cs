using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GraphQLServer.Entities
{
    public class Owner
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}