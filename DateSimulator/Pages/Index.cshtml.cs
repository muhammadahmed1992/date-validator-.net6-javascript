using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    [BindProperty]
    public CustomDate? CustomDate { get; set; }

    public void OnGet()
    {
        // Initialize the CustomDate object
        CustomDate = new CustomDate();
    }

    public IActionResult OnPost()
    {
        // Check if the model state is valid
        if (!ModelState.IsValid)
        {
            // If not valid, return the page to show validation errors
            return Page();
        }

        // Handle valid form submission (redirect to a success page, for example)
        return RedirectToPage("Success");
    }
}
