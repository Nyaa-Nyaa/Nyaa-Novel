using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NyaaNovelWPF
{
    class NyaaScene
    {
        int Location;
        int Length;
        DebugConsole NyaaDebug;
        String ChapterLocation;
        String sceneBackground;
        String sceneTransition;
        NyaaDialog[] Dialogs;
        XmlNodeList sceneContent;
        

        public NyaaScene(String Transition, String Background, XmlNodeList Content, String inLocation, DebugConsole NyaaDebugPointer)
        {
            Location = 0;
            sceneBackground = Background;
            sceneTransition = Transition;
            sceneContent = Content;
            NyaaDebug = NyaaDebugPointer;
            Length = sceneContent.Count;
            ChapterLocation = inLocation;
            loadDialog();
        }

        public NyaaDialog getNextDialog()
        {
            if (Location < Length)
            {
                NyaaDialog TempDialog = Dialogs[Location];
                Location++;
                return TempDialog;
            }
            else
            {
                return null;
            }
        }

        // Ok, this name is bad, but... 
        // It takes our xml d (which stands for dialog) nodes 
        // from scene-content and seperate it into NyaaDialog variables
        public void loadDialog() 
        {
            Dialogs = new NyaaDialog[sceneContent.Count];
            int dialogNumber = 0;
            foreach (XmlNode dialog in sceneContent)
            {
                //NyaaDebug.addToConsole(xmlnodeAsString(dialog));
                if (dialog.SelectSingleNode("selection-screen") == null)
                {
                    Dialogs[dialogNumber] = new NyaaDialog(dialog["d-content"].InnerText, dialog["name"].InnerText, getResourceLocation(dialog["char-img"].InnerText), dialog["char-pos"].InnerText, dialog["char-view"].InnerText, boolStrConvert(dialog["shadow"].InnerText));
                    dialogNumber++;
                }
                else
                {
                    NyaaDebug.addToConsole("Notice: Found a selection screen");
                    XmlNodeList selections = dialog["selection-screen"].ChildNodes;
                    Dialogs[dialogNumber] = new NyaaDialog(dialog["d-content"].InnerText, dialog["name"].InnerText, getResourceLocation(dialog["char-img"].InnerText), dialog["char-pos"].InnerText, dialog["char-view"].InnerText, boolStrConvert(dialog["shadow"].InnerText), selections);
                }
            }
        }

        public string xmlnodeAsString(XmlNode inXml)
        {
            var xmlNode = inXml;
            using (var sw = new StringWriter())
            {
                using (var xw = new XmlTextWriter(sw))
                {
                  xw.Formatting = Formatting.Indented;
                  xw.Indentation = 2; //default is 1. I used 2 to make the indents larger.

                  xmlNode.WriteTo(xw);
                }
                return sw.ToString(); //The node, as a string, with indents!
            }
        }

        public String getSceneBackground()
        {
            return sceneBackground;
        }

        public String getSceneTransition()
        {
            return sceneTransition;
        }

        private string getResourceLocation(String chapterBasedLocation)
        {
            return ChapterLocation.Substring(0, ChapterLocation.LastIndexOf("\\")) + chapterBasedLocation.Substring(1);
        }

        private bool boolStrConvert(string p)
        {
            if (p.CompareTo("true") == 0)
            {
                return true;
            }
            else
            {
                if (p.CompareTo("false") == 0)
                {
                    return false;
                }
                else
                {
                    NyaaDebug.addToConsole("Warn! : One of your shadow tags are broken. Defaulting to true");
                    return true;
                }
            }
        }
    }
}
