using TeploobmenWeb.Data;

namespace TeploobmenWeb.Models
{
    public class HomeViewModel
    {
        public Variant? Variant { get; set; }

        public List<Variant> Variants { get; set; }
    }
}
