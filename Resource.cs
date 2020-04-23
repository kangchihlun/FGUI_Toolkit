using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fgui_toolkit
{
    public class FResource
    {
        public string id;
        public string name;
        public string path;
    }

    public class FImage : FResource
    {
        public string qualityOption;
        public string quality;
        public string atlas;
    }
}
