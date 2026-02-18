namespace PortalWWW.Models
{
    public class PageContentViewModel
    {
        public string Header { get; set; }
        public List<PageSectionViewModel> Sections { get; set; } = new();
    }

    public class PageSectionViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }

}
