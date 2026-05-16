using Application.DTO.Admins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IAdminsRepository
    {
        public Task<AdminsOverviewResult> AdminsOverview(CancellationToken cancellationToken);
        public Task<UserMonitoringResult> UserMonitoringList(CancellationToken cancellationToken);
        public Task<StudentProfileInformationResutl> GetStudentProfileInformation(int studentId, CancellationToken cancellationToken);
        public Task<List<AdminManagementItem>> GetAdminManagementData(CancellationToken cancellationToken);
        public Task<ViewProfileInformationResult> GetProfileInformation(int userId);
        public Task<UpdateProfileInformationData> UpdateProfileInformation(int userId , UpdateProfileInformationData input);
    }
}