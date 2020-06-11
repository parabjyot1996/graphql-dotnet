using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraphQLServer.Entities
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public TypeOfAccount Type { get; set; }

        public string Description { get; set; }

        [ForeignKey("OwnerId")]
        public int OwnerId { get; set; }

        public Owner Owner { get; set; }
    }
}