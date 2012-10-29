using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NyaaNovelWPF
{
    class NyaaChapter
    {
        int sceneLoc;
        int Length;
        String ChapterName;
        String ChapterLocation;
        String ChapterLoadingScreenLocation;
        XmlDocument ChapterXml;
        NyaaScene[] SceneList;
        DebugConsole NyaaDebug;

        public NyaaChapter(String Name, String Location, String LoadingScreenLocation, String rootDirectory, DebugConsole NyaaDebugPointer)
        {
            sceneLoc = 0;
            ChapterName = Name;
            ChapterLocation = Location;
            ChapterLoadingScreenLocation = LoadingScreenLocation;
            NyaaDebug = NyaaDebugPointer;
            // TN: load chapter xml
            ChapterXml = new XmlDocument();
            if (File.Exists(Location))
            {
                NyaaDebug.addToConsole("Notice: File found! Loading: " + Location);
                try
                {
                    ChapterXml.Load(Location);
                    NyaaDebug.addToConsole("Notice: Chapter File (successfully) loaded!");
                    NyaaDebug.addToConsole("Notice: ChapterObj created for: " + ChapterName);
                    loadScenes();
                }
                catch (XmlException ex)
                {
                    NyaaDebug.addToConsole(String.Format("FATAL: Your " + Location + " is broken: {0}", ex.Message));
                }
            }
            else
            {
                NyaaDebug.addToConsole("FATAL: Failed to load file");
            }
            
        }

        public NyaaDialog getNextDialogFromScene()
        {
            if (sceneLoc < Length)
            {
                NyaaDialog TempDialog = SceneList[sceneLoc].getNextDialog();
                if (TempDialog == null)
                {
                    sceneLoc++;
                    return getNextDialogFromScene();
                }
                else
                {
                    return TempDialog;
                }
            }
            return null;
        }

        private string getResourceLocation(String chapterBasedLocation)
        {
            return ChapterLocation.Substring(0, ChapterLocation.LastIndexOf("\\")) + chapterBasedLocation.Substring(1);
        }

        public String getChapterName()
        {
            return ChapterName;
        }

        public String getChapterLoadingScreenLoaction()
        {
            return ChapterLoadingScreenLocation;
        }

        public String getChapterLocation()
        {
            return ChapterLocation;
        }

        public String getCurrentSceneBackground()
        {
            return SceneList[sceneLoc].getSceneBackground();
        }

        private void loadScenes()
        {
            XmlNodeList Scenes = ChapterXml.SelectNodes("//scenes/scene");
            Length = Scenes.Count;
            NyaaDebug.addToConsole("\n -| Starting Scene Search for Chapter: " + ChapterName + "|-");
            NyaaDebug.addToConsole("Notice: Found " + Scenes.Count + " scenes to process");
            SceneList = new NyaaScene[Scenes.Count];
            int sceneNumber = 0;
            foreach (XmlNode sceneData in Scenes)
            {
                String transition = sceneData["transition"].InnerText;
                String background = getResourceLocation(sceneData["background"].InnerText);
                XmlNodeList dialog = sceneData["scene-content"].ChildNodes;
                NyaaDebug.addToConsole("Making Scene Object for: " + sceneNumber + " and SceneList has " + SceneList.Length);
                NyaaScene tempScene = new NyaaScene(transition, background, dialog, ChapterLocation, NyaaDebug);
                NyaaDebug.addToConsole("Scene Made, inserting!");
                SceneList[sceneNumber] = tempScene;
                sceneNumber++;
            }

        }
    }
}
