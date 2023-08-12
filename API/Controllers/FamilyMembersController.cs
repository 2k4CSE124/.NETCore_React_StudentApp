using Application.FamilyMember;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /*-------------------------------- Get Methods ---------------------------*/
    public class FamilyMembersController : BaseApiController
    {
        /// <summary>
        /// Get all family members list with out any filter
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<FamilyMemberInfo>>> GetFamilyMemberInfo()
        {
            return await Mediator.Send(new List.Query());
        }

        /// <summary>
        /// Gets a nationality associated with a family member: 
        /// [GET] /api/FamilyMembers/{id}/Nationality/{id}
        /// Todo: There is no need of NationalityId. 
        /// If we have nationalityId then we can get nationality from Country List vailable at front end
        /// </summary>
        /// <param name="familyMemberId"></param>
        /// <returns></returns>
        [HttpGet("{familyMemberId}/Nationality")]
        public async Task<IActionResult> GetNationalityByFamilyId(int familyMemberId)
        {
            return HandleResult(await Mediator.Send(new Details.Query { FamilyMemberId = familyMemberId }));
        }

        /*------------------------------- Put Methods -----------------------------*/
        /// <summary>
        /// Updates a complete List of Family Members
        /// [PUT]/api/FamilyMembers/{id}
        /// </summary>
        /// <param name="StudentId"></param>
        /// <param name="familyMemberObj"></param>
        /// <returns></returns>
        [HttpPut("{StudentId}/familymemberlist")]
        public async Task<IActionResult> EditFamilyMemberList(int StudentId, [FromBody] List<FamilyMemberInfo> familyMemberObj)
        {
           // return BadRequest(familyMemberObj);
              return HandleResult(await Mediator.Send(new Edit.Command { StudentId = StudentId, FamilyMember = familyMemberObj }));
        }

        /// <summary>
        /// Updates a particular Family Member
        /// [PUT]/api/FamilyMembers/{id}
        /// </summary>
        /// <param name="familyId"></param>
        /// <param name="familyMemberObj"></param>
        /// <returns></returns>
        [HttpPut("{familyId}")]
        public async Task<IActionResult> EditFamilyMember(int familyId, [FromBody] FamilyMemberInfo familyMemberObj)
        {
            return HandleResult(await Mediator.Send(new Edit.SingleRecordCommand { FamilyMemberId = familyId, FamilyMember = familyMemberObj }));
        }


        // Updates a particular Family Member’s Nationality:  
        // [PUT] /api/FamilyMembers/{id}/Nationality/{id}
        [HttpPut("{familyId}/Nationality/{nationalityId}")]
        public async Task<IActionResult> EditStudentNationality(int familyId, int nationalityId)
        {
            // return BadRequest("Method Called");
            return HandleResult(await Mediator.Send(new Edit.EditNationalityCommand { FamilyMemberId = familyId, NationalityId = nationalityId }));
        }

        /*-------------------------------- Delete Methods ---------------------------*/

        /// <summary>
        /// Delete all family members for a particular Student
        /// [DELETE]/api/FamilyMembers/{id}
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpDelete("{studentId}")]
        public async Task<IActionResult> DeleteFamilyMember(int studentId)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = studentId }));
        }
    }
}