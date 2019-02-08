using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotesZone.Web.ViewModels
{
    using NotesZone.Domain.Basket;
    public class BasketIndexViewModel
    {
        public Basket Basket { get; set; }
        public string ReturnUrl { get; set; }

    }
}