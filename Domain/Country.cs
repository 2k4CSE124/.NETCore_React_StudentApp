using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CountryName { get; set; }


        // Navigation property for one-to-many relationship
        public ICollection<FamilyMemberInfo> FamilyMembers { get; set; }
        public ICollection<StudentInfo> Students { get; set; }
    }
}