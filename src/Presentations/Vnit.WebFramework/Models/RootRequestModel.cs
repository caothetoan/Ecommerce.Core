
namespace Vnit.WebFramework.Models
{
    public class RootRequestModel : RootModel
    {
        public int Count { get; set; }

        public int Page { get; set; }

        public bool? Ascending { get; set; }

        public string Name { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string DateFrom { get; set; }

        public string DateTo { get; set; }

        public int LanguageId { get; set; }

        public bool? ShowOnHomePage { get; set; }

        public bool? IsTree { get; set; }

        public int? Status { get; set; }

        public int? Type { get; set; }

        public string Content { get; set; }

        public int? CategoryId { get; set; }

        public int? ParentId { get; set; }


        public string OrderBy { get; set; }

    }
}
