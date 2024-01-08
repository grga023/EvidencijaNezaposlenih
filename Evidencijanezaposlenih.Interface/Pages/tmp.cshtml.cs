using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Evidencijanezaposlenih.Interface.Pages
{
    public class tmpModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }

        public void OnPost()
        {
            // Handle form submission
            // Process the form data as needed
        }
    }
}
