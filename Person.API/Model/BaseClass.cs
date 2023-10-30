using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Person.API.Model
{
    public class BaseClass
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("nome")]
        public string Name { get; set; }

    }
}
