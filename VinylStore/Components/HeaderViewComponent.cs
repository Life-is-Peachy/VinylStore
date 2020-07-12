using Microsoft.AspNetCore.Mvc;

namespace VinylStore.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
            => View();
    }
}
