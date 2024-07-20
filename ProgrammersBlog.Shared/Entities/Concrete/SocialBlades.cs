using System.ComponentModel.DataAnnotations;

namespace ProgrammersBlog.Shared.Entities.Concrete
{
    public class SocialBlades
    {
        [RegularExpression("^https://www.linkedin.com/in/.*$", ErrorMessage = "Bir Linkedin profili linki girmelisiniz")]
        public string? LinkedInProfileUrl { get; set; }
        [RegularExpression("^https://github.com/.*$", ErrorMessage = "Bir Github profili linki girmelisiniz")]
        public string? GitHubProfileUrl { get; set; }
    }
}
