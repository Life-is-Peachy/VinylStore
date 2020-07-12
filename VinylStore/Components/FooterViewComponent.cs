using Microsoft.AspNetCore.Mvc;

namespace VinylStore.Components
{
    public class FooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
            => View();
    }
}
