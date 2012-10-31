using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NyaaNovelWPF
{
    class NyaaChoice
    {
        String[] descriptions;
        String[] flags;

        public NyaaChoice(XmlNodeList choices)
        {
            processXml(choices);
        }
        
        private void processXml(XmlNodeList choices)
        {
            
        }
    }
}
