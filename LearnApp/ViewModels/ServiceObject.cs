using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LearnApp.Models
{
    public class ServiceObject
    {
        public string PanelColor { get; set; }
        public Service Service { get; set; }
        public string DiscountText { get; set; }
        public string PriceText { get; set; }
        public Visibility OldPriceVisibility { get; set; }
        public Visibility ButtonsVisibility { get; set; }
        public string MainImage { get; set; }
    }
}
