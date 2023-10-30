using System.ComponentModel.DataAnnotations.Schema;

namespace Person.API.Model
{
    [Table("pessoa")]
    public class Person : BaseClass
    {
        [Column("datanascimento")]
        public DateTime BirthDate { get; set; }
        [Column("cpf")]
        public string Document { get; set; }
        [Column("funcionario")]
        public bool Employee { get; set; }
    }
}
