using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class FamilyMemberInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int RelationId { get; set; }

        public int NationalityId { get; set; }
        public Country Nationality { get; set; }

        public int StudentId { get; set; }
        public StudentInfo Student { get; set; }
    }
}