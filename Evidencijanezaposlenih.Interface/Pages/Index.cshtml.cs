using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Evidencijanezaposlenih.Interface.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnPost()
        {
            var name = Request.Form["name"];
            var surname = Request.Form["surname"];
            var dateOfBirth = Request.Form["dateOfBirth"];
            var jmbg = Request.Form["jmbg"];
            var phoneNumber = Request.Form["phoneNumber"];

            // Handling the list of work experiences
            var nazivFirme = Request.Form["nazivFirme[]"];
            var datumPocetka = Request.Form["datumPocetka[]"];
            var datumZavrsetka = Request.Form["datumZavrsetka[]"];
        }
    }
}
