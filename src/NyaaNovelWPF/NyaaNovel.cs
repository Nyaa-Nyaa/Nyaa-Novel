using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace NyaaNovelWPF
{
    public class NyaaNovel
    {
        String rootLocation;
        DebugConsole NyaaDebug;
        XmlDocument rootNyaaStoryFile;
        NyaaChapter[] NyaaChapters;
        String rootDirectory;
        Boolean animated;
        NyaaOutput YuiImouto;
        int curLocationChapter;
        int Length;

        public NyaaNovel(String novelLocation)
        {
            animated = false;
            curLocationChapter = 0;
            rootLocation = novelLocation;
            rootDirectory = rootLocation.Substring(0, rootLocation.LastIndexOf("\\"));
            loadChapers();
        }

        private void loadChapers()
        {
            // Start Debug Console
            NyaaDebug = new DebugConsole();
            NyaaDebug.Show();
            NyaaDebug.addToConsole("NyaaNovel Pre-Alpha 0.0.1a Console Loaded");
            NyaaDebug.addToConsole("\n ------| Root File Search |------");
            // load main xml
            rootNyaaStoryFile = new XmlDocument();
            if (File.Exists(rootLocation))
            {
                NyaaDebug.addToConsole("Notice: File found! Loading: " + rootLocation);
                try
                {
                    rootNyaaStoryFile.Load(rootLocation);
                    NyaaDebug.addToConsole("Notice: File (successfully) loaded!");
                }
                catch (XmlException ex)
                {
                    NyaaDebug.addToConsole(String.Format("FATAL: Your MainStory.nyaa is broken: {0}", ex.Message));
                }
            }
            else
            {
                NyaaDebug.addToConsole("FATAL: Failed to load file");
            }
            try
            {
                XmlNodeList tempList = rootNyaaStoryFile.SelectNodes("//chapters/chapter");
                NyaaDebug.addToConsole("Notice: Chapter Query Success! Number of Chapters reported: " + tempList.Count);
                NyaaChapters = new NyaaChapter[tempList.Count];
                Length = tempList.Count;
                int chapterNo = 0; //sigh no .add method...
                foreach (XmlNode chapter in tempList)
                {
                    NyaaDebug.addToConsole("\n ---| New Chapter Search |---");
                    String title = chapter["title"].InnerText;
                    String loadingSplash = getResourceLocation(chapter["loading-splash"].InnerText);
                    String chapterLocation = getResourceLocation(chapter["chapter-location"].InnerText);
                    NyaaDebug.addToConsole("Notice: Found chapter: " + title);
                    if (File.Exists(loadingSplash))
                    {
                        NyaaDebug.addToConsole("Notice: Loading Splash applied from: " + loadingSplash);
                        loadSplash(loadingSplash);
                    }
                    else
                    {
                        NyaaDebug.addToConsole("WARN! : Loading splash not found, defaulting to blank");
                    }
                    if (File.Exists(chapterLocation))
                    {
                        NyaaDebug.addToConsole("Notice: Chapter is found in: " + chapterLocation + "; Now loading...");
                    }
                    else
                    {
                        NyaaDebug.addToConsole("FATAL!! : Chapter file is missing, intended location: " + chapterLocation);
                    }
                    NyaaChapters[chapterNo] = new NyaaChapter(title, chapterLocation, loadingSplash, rootDirectory, NyaaDebug);
                    NyaaDebug.addToConsole("Notice: Back in the main thread!");
                    chapterNo++;

                }
            }
            catch (XmlException ex)
            {
                NyaaDebug.addToConsole(String.Format("FATAL: Bad query... : {0}", ex.Message));
            }
            YuiImouto = new NyaaOutput(this, NyaaDebug);
            YuiImouto.Show();

        }

        private string getResourceLocation(String rootBasedLocation)
        {
            return rootDirectory + rootBasedLocation.Substring(1);
        }

        private void loadSplash(string loadingSplash)
        {

        }

        public String getCurrentBackground()
        {
            return NyaaChapters[curLocationChapter].getCurrentSceneBackground();
        }

        public NyaaDialog nextDialog()
        {
            if (curLocationChapter < Length)
            {
                if (NyaaChapters[curLocationChapter] != null)
                {
                    NyaaDialog TempDialog = NyaaChapters[curLocationChapter].getNextDialogFromScene();
                    if (TempDialog == null)
                    {
                        curLocationChapter++;
                        return nextDialog();
                    }
                    else
                    {
                        return TempDialog;
                    }
                }
                else
                {
                    NyaaDebug.addToConsole("FATAL!! : Chapter objects are null");
                }
            }
            return null;
        }

        public NyaaDialog nextText()
        {
            //code to progress to next page
            //NyaaDebug.addToConsole("Next Page");
            NyaaDialog CurrentPage = nextDialog();
            if (CurrentPage != null)
            {
                return CurrentPage;
            }
            return null;
        }
    }
}
