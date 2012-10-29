using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;


namespace NyaaNovel
{
    public partial class NyaaNovel : Form
    {
        String rootLocation;
        DebugConsole NyaaDebug;
        XmlDocument rootNyaaStoryFile;
        NyaaChapter[] NyaaChapters;
        String rootDirectory;
        Boolean animated;
        int curLocationChapter;
        int Length;


        public NyaaNovel(String novelLocation)
        {
            InitializeComponent();
            animated = false;
            curLocationChapter = 0;
            rootLocation = novelLocation;
            rootDirectory = rootLocation.Substring(0, rootLocation.LastIndexOf("\\"));
        }

        private void NyaaNovel_Load(object sender, EventArgs e)
        {
            
            loadChapers();
            
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
                        nextDialog();
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

        private void finishNovel()
        {
            NyaaDebug.addToConsole("Novel Complete!");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            nextText();
        }

        private string getResourceLocation(String rootBasedLocation)
        {
            return rootLocation.Substring(0, rootLocation.LastIndexOf("\\")) + rootBasedLocation.Substring(1);
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
            
        }

        private void loadSplash(string loadingSplash)
        {
            
        }

        private void nextText()
        {
            //code to progress to next page
            NyaaDebug.addToConsole("Next Page");
            NyaaDialog CurrentPage = nextDialog();
            if (CurrentPage != null)
            {
                NyaaDebug.addToConsole(CurrentPage.getDebugString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nextText();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           int speed = 30;
           while (DialogBG.Left < -300)
           {
               Application.DoEvents();
               DialogBG.Left = DialogBG.Left + speed;
               
               this.Invalidate();
               System.Threading.Thread.Sleep(1);
           }
           
        }
    }
}
