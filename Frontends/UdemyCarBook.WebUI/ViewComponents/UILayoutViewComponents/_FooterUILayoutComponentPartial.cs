﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace UdemyCarBook.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _FooterUILayoutComponentPartial:ViewComponent
    {
       
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
