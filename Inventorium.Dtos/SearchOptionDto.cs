using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventorium.Dtos
{
    public class SearchOptionDto
    {
        // The dto properties
        public string OptionDisplayCategoryLabel { get; set; }
        public string OptionResourceParent { get; set; }
        public string OptionResourceFrontendParent { get; set; }
        public string OptionDisplayValue { get; set; }

        public int OptionDisplayId { get; set; }
    }
}

