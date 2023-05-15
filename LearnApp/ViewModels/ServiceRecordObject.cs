using LearnApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LearnApp.ViewModels
{
    class ServiceRecordObject
    {
        public ServiceRecord ServiceRecord { get; set; }
        public Service Service { get; set; }
        public Client Client { get; set; }
        public Brush TimeColor { get; set; }
        public string Time { get; set; }
        public string FIO { get; set; }
    }
}
