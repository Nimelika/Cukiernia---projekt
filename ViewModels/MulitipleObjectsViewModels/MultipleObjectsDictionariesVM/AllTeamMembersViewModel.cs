using DesktopApp.ViewModels.AbstractViewModels;
using DesktopApp.ViewModels.SingleObjectViewModels.DisplayOnlyVM;
using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.TeamMemberVM;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MakeAWishDB.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DesktopApp.ViewModels.MulitipleObjectsViewModels.MultipleObjectsDictionariesVM
{
    public class AllTeamMembersViewModel : AllViewModel<TeamMemberViewModel>
    {
        #region Constructor
        public AllTeamMembersViewModel()
            : base("Team Members")
        {
            DetailsCommand = new RelayCommand<TeamMemberViewModel>(ShowDetails);
            UpdateCommand = new RelayCommand<TeamMemberViewModel>(ShowUpdate);
            DeleteCommand = new RelayCommand<TeamMemberViewModel>(ShowDelete);

            Messenger.Default.Register<string>(this, "TeamMember", msg => { Load(); });
        }
        #endregion

        #region Properties
        private TeamMemberViewModel _SelectedTeamMember;
        public TeamMemberViewModel SelectedTeamMember
        {
            get => _SelectedTeamMember;
            set
            {
                if (value != _SelectedTeamMember)
                {
                    _SelectedTeamMember = value;
                    Messenger.Default.Send(_SelectedTeamMember);
                }
            }
        }
        #endregion

        #region Commands
        public RelayCommand<TeamMemberViewModel> DetailsCommand { get; set; }
        public RelayCommand<TeamMemberViewModel> UpdateCommand { get; set; }
        public RelayCommand<TeamMemberViewModel> DeleteCommand { get; set; }

        private void ShowDetails(TeamMemberViewModel member)
        {
            if (member == null) return;

            var entity = SharedData_Entities.TeamMembers
                .FirstOrDefault(t => t.TeamMemberId == member.TeamMemberId);

            if (entity != null)
            {
                var displayVM = new DisplayTeamMemberViewModel();
                displayVM.Load(member.TeamMemberId);
                Messenger.Default.Send(displayVM, "TeamMemberDisplay");
            }
        }

        private void ShowUpdate(TeamMemberViewModel member)
        {
            if (member == null) return;

            var updateVM = new UpdateTeamMemberViewModel();
            updateVM.Load(member.TeamMemberId);
            Messenger.Default.Send(updateVM, "TeamMemberUpdate");
        }

        private void ShowDelete(TeamMemberViewModel member)
        {
            if (member == null) return;

            var entity = SharedData_Entities.TeamMembers
                .FirstOrDefault(t => t.TeamMemberId == member.TeamMemberId);

            if (entity != null)
            {
                var deleteVM = new DeleteTeamMemberViewModel();
                deleteVM.item = entity;
                Messenger.Default.Send(deleteVM, "TeamMemberDelete");
            }
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<TeamMemberViewModel>
            (
                SharedData_Entities.TeamMembers
                .Where(t => t.IsActive == true)
                .AsEnumerable()
                .Select((t, index) =>
                {
                    var vm = new TeamMemberViewModel(t);
                    vm.Position = index + 1;
                    return vm;
                })
            );
        }

        public override List<string> getComboboxSortList()
        {
            return new List<string> { "Name" };
        }

        public override void Sort()
        {
            if (SortField == "Name")
                List = new ObservableCollection<TeamMemberViewModel>(List.OrderBy(item => item.Name));
        }

        public override List<string> getComboboxFindList()
        {
            return new List<string> { "Name" };
        }

        public override void Find()
        {
            base.Find();

            if (FindField == "Name")
                List = new ObservableCollection<TeamMemberViewModel>
                (
                    List.Where(item =>
                        item.Name != null &&
                        item.Name.StartsWith(FindTextBox, System.StringComparison.CurrentCultureIgnoreCase))
                );
        }
        #endregion
    }
}
