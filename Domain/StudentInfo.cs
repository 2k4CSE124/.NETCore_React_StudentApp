using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class StudentInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public DateTime DOB { get; set; }

        public int NationalityId { get; set; }

        // Navigation property for one-to-many relationship
        public Country Nationality { get; set; }
        public ICollection<FamilyMemberInfo> FamilyMembers { get; set; }

    }
}