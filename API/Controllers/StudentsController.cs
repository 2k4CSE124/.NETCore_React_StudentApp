using Application.Students;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class StudentsController : BaseApiController
    {

        /*-------------------------------- Get Methods ---------------------------*/

        /// <summary>
        /// Get all Students
        /// [GET] /api/Students 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetStudentInfo()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        /// <summary>
        /// Gets the Nationality of a particular student
        /// [GET] /api/Students/{id}/Nationality
        /// </summary>
        /// <returns></returns>
        [HttpGet("{studentId}/Nationality")]
        public async Task<IActionResult> GetNationalityByStudentId(int studentId)
        {
            return HandleResult(await Mediator.Send(new Details.GetStudentNationalityQuery { StudentId = studentId }));
        }

        /// <summary>
        /// Gets Family Members for a particular Student
        /// [GET] /api/Students/{id}/FamilyMembers/
        /// </summary>
        /// <returns></returns>
        [HttpGet("{studentId}/FamilyMembers")]
        public async Task<IActionResult> GetFamilyMemberByStudentId(int studentId)
        {
            return HandleResult(await Mediator.Send(new Details.GetStudentFamilyQuery { StudentId = studentId }));
        }

        /*----------------------------- Post Methods --------------------------------*/

        /// <summary>
        /// Add a new Student with Basic Details Only
        /// [POST] /api/Students
        /// </summary>
        /// <param name="studentObj"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentInfo studentObj)
        {
            return HandleResult(await Mediator.Send(new Create.Command { StudentInfo = studentObj }));
        }

        /// <summary>
        /// Creates a new Family Member for a particular Student (without the nationality)
        /// [POST] /api/Students/{id}/FamilyMembers/
        /// </summary>
        /// <param name="studentObj"></param>
        /// <returns></returns>
        [HttpPost("{studentId}/FamilyMembers")]
        public async Task<IActionResult> CreateStudentFamilyMember(int studentId, [FromBody] List<FamilyMemberInfo> familyObj)
        {

            return HandleResult(await Mediator.Send(new Create.StudentFamilyMemberCommand { StudentId = studentId, FamilyMember = familyObj }));
        }

        /*------------------------------- Put Methods -----------------------------*/

        /// <summary>
        /// Updates a Student’s Basic Details only
        /// [PUT] /api/Students/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studentObj"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditStudent(int id, [FromBody] StudentInfo studentObj)
        {
            studentObj.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { StudentInfo = studentObj }));
        }

        /// <summary>
        /// Updates a Student’s Nationality
        /// [PUT] /api/Students/{id}/Nationality/{id}
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="nationalityId"></param>
        /// <returns></returns>
        [HttpPut("{studentId}/Nationality/{nationalityId}")]
        public async Task<IActionResult> EditStudentNationality(int studentId, int nationalityId)
        {
            // return BadRequest("Method Called");
            return HandleResult(await Mediator.Send(new Edit.EditNationalityCommand { StudentId = studentId, NationalityId = nationalityId }));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }

    }
}