using Microsoft.AspNetCore.Mvc;

namespace TicketTracker.MVC.Views.TicketTracker.Components.TicketHistory
{
    public class TicketHistoryViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync( )
        {
            
            return View();
        }
    }
}
