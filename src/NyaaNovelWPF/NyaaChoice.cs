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
        int amount;

        public NyaaChoice(XmlNodeList choices)
        {
            processXml(choices);
        }
        
        private void processXml(XmlNodeList choices)
        {
            amount = choices.Count;
            descriptions = new String[choices.Count];
            flags = new String[choices.Count];
            int choiceNo = 0;
            foreach (XmlNode choice in choices)
            {
                descriptions[choiceNo] = choice["text"].InnerText;
                flags[choiceNo] = choice["flag"].InnerText;
                choiceNo++;
            }
        }

        public String[] getDescriptions()
        {
            return descriptions;
        }

        public String getFlag(int choiceNo)
        {
            return flags[choiceNo];
        }

        public int getAmount()
        {
            return amount;
        }
    }
}
