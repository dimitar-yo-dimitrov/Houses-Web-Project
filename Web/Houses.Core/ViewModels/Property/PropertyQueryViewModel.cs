namespace Houses.Core.ViewModels.Property
{
    public class PropertyQueryViewModel
    {
        public int TotalPropertyCount { get; set; }

        public IEnumerable<PropertyServiceViewModel> Properties { get; set; }
            = new HashSet<PropertyServiceViewModel>();
    }
}
