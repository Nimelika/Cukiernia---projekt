using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;
using System.Linq;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.TeamMemberVM
{
    public class DeleteTeamMemberViewModel
        : DeleteViewModel<TeamMember>
    {
        public DeleteTeamMemberViewModel()
            : base("Delete Team Member")
        {
        }

        protected override string RefreshMessageTag => "AllTeamMembers";

        public void Load(int id)
        {
            item = sharedData_Entities.TeamMembers
                .FirstOrDefault(t => t.TeamMemberId == id);
        }

        public string Name => item?.Name;
    }
}

