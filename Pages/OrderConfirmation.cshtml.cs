using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HaulMaster.Pages
{
    public class OrderConfirmationModel : PageModel
    {

        public void OnGet(String ConfirmationNumber)
        {
            Console.WriteLine("OnGet");
            ViewData["ConfirmationNumber"] = ConfirmationNumber;
            ViewData["CartCount"] = 0;
        }

        //public PageResult OnGet(string ConfirmationNumber)
        //{
        //    ViewData["ConfirmationNumber"] = ConfirmationNumber;
        //    // Return to page
        //    return Page();
        //}
    }
}
