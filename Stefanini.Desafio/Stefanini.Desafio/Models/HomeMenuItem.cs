using System;
using System.Collections.Generic;
using System.Text;

namespace Stefanini.Desafio.Models
{
    public enum MenuItemType
    {
        About,
        Weather
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
