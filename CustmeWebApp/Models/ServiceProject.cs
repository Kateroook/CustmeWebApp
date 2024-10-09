namespace CustmeWebApp.Models
{
    public class ServiceProject
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
