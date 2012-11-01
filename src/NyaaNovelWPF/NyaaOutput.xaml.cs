using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NyaaNovelWPF
{
    /// <summary>
    /// Interaction logic for NyaaOutput.xaml
    /// </summary>
    public partial class NyaaOutput : Window
    {
        NyaaNovel Novel;
        DebugConsole NyaaDebug;
        String CurrentCharPath;
        String CurrentBGPath;
        Boolean textAnimating;
        NyaaDialog thisDialog;

        public NyaaOutput(NyaaNovel novelToControl, DebugConsole NyaaDebugPointer)
        {
            InitializeComponent();
            NyaaDebug = NyaaDebugPointer;
            Novel = novelToControl;
            CurrentCharPath = "";
            CurrentBGPath = "";
            textAnimating = false;
            startStory();
        }

        private void startStory()
        {
             thisDialog = Novel.nextText();
             CurrentBGPath = Novel.getCurrentBackground();
             BitmapImage bmImage = new BitmapImage();
             bmImage.BeginInit();
             bmImage.UriSource = new Uri(CurrentBGPath, UriKind.Absolute);
             bmImage.EndInit();
             Background.Source = bmImage;
             DoubleAnimation anim2 = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(2));
             SceneSwitcher.BeginAnimation(Image.OpacityProperty, anim2);
             Update(Novel.getCurrentBackground(), thisDialog);
        }

        private void Update(String imagePath, NyaaDialog CurrentDialog)
        {
            if (CurrentBGPath.CompareTo(imagePath) != 0)
            {
                if (File.Exists(imagePath))
                {
                    DoubleAnimation anim1 = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(2));
                    Storyboard board = new Storyboard();
                    board.Children.Add(anim1);
                    Storyboard.SetTarget(anim1, SceneSwitcher);
                    Storyboard.SetTargetProperty(anim1, new PropertyPath("(Opacity)"));
                    board.Completed += delegate
                    {
                        setMainText(CurrentDialog.getDialog());
                        if (CurrentDialog.getUserInteracting())
                        {
                            displayChoices(CurrentDialog.getChoices());
                        }
                        setNameText(CurrentDialog.getTitle());
                        setCharacterImage(CurrentDialog.getCharacterImage());
                        setShadow(CurrentDialog.getShadow());
                        NyaaDebug.addToConsole("Switching Backgrounds");
                        // Create image element to set as icon on the menu element
                        BitmapImage bmImage = new BitmapImage();
                        bmImage.BeginInit();
                        bmImage.UriSource = new Uri(imagePath, UriKind.Absolute);
                        bmImage.EndInit();
                        Background.Source = bmImage;
                        CurrentBGPath = imagePath;
                        DoubleAnimation anim2 = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(2));
                        SceneSwitcher.BeginAnimation(Image.OpacityProperty, anim2);
                    };
                    board.Begin();
                }
            }
            else
            {
                setMainText(CurrentDialog.getDialog());
                if (CurrentDialog.getUserInteracting())
                {
                    displayChoices(CurrentDialog.getChoices());
                }
                setNameText(CurrentDialog.getTitle());
                setCharacterImage(CurrentDialog.getCharacterImage());
                setShadow(CurrentDialog.getShadow());
            }
        }

        private void displayChoices(NyaaChoice nyaaChoice)
        {
            if (nyaaChoice.getAmount() <= 4)
            {
                Button[] selectors = {Choice1,Choice2,Choice3,Choice4};
                Choices.Visibility = Visibility.Visible;
            }
            else
            {
                NyaaDebug.addToConsole("FATAL: More than 4 choices! NOT YET IMPLEMENTED!");
            }
        }

        private void setCharacterImage(String imagePath)
        {
            if (CurrentCharPath.CompareTo(imagePath) != 0)
            {
                if (File.Exists(imagePath))
                {
                    DoubleAnimation anim1 = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1));
                    Storyboard board = new Storyboard();
                    board.Children.Add(anim1);
                    Storyboard.SetTarget(anim1, CharacterImage);
                    Storyboard.SetTargetProperty(anim1, new PropertyPath("(Opacity)"));
                    board.Completed += delegate
                    {
                        NyaaDebug.addToConsole("Switching Characters and showing them");
                        // Create image element to set as icon on the menu element
                        BitmapImage charImage = new BitmapImage();
                        charImage.BeginInit();
                        charImage.UriSource = new Uri(imagePath, UriKind.Absolute);
                        charImage.EndInit();
                        CharacterImage.Source = charImage;
                        CurrentCharPath = imagePath;
                        DoubleAnimation anim2 = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(1));
                        CharacterImage.BeginAnimation(Image.OpacityProperty, anim2);
                    };
                    board.Begin();
                }
            }
        }

        private void setMainText(String text)
        {
            MainText.Text = text;
            if (text.CompareTo("") != 0 && TextBG.Opacity == 0)
            {
                DoubleAnimation anim1 = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.1));
                TextBG.BeginAnimation(Image.OpacityProperty, anim1);
            }
            if (text.CompareTo("") == 0 && TextBG.Opacity == 1)
            {
                DoubleAnimation anim1 = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.1));
                TextBG.BeginAnimation(Image.OpacityProperty, anim1);
            }
        }

        private void setNameText(String text)
        {
            NameText.Content = text;
            if (text.CompareTo("") != 0 && NameBG.Opacity == 0)
            {
                DoubleAnimation anim1 = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.1));
                NameBG.BeginAnimation(Image.OpacityProperty, anim1);
            }
            if (text.CompareTo("") == 0 && NameBG.Opacity == 1)
            {
                DoubleAnimation anim1 = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.1));
                NameBG.BeginAnimation(Image.OpacityProperty, anim1);
            }
        }

        private void setShadow(Boolean visibility)
        {
            if (visibility && Shadow.Opacity == 0)
            {
                DoubleAnimation anim1 = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(1));
                CharacterImage.BeginAnimation(Image.OpacityProperty, anim1);
            }
            if (!visibility && Shadow.Opacity == 1)
            {
                DoubleAnimation anim1 = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1));
                CharacterImage.BeginAnimation(Image.OpacityProperty, anim1);
            }
        }

        private void setCharacterAlignment(String location)
        {
            if (location.CompareTo("right") == 0)
            {
                CharacterImage.HorizontalAlignment = HorizontalAlignment.Right;
            }
            if (location.CompareTo("left") == 0)
            {
                CharacterImage.HorizontalAlignment = HorizontalAlignment.Left;
            }
            if (location.CompareTo("center") == 0)
            {
                CharacterImage.HorizontalAlignment = HorizontalAlignment.Center;
            }
            else
            {
                CharacterImage.HorizontalAlignment = HorizontalAlignment.Right;
            }
        }

        private void nextPage()
        {
            NyaaDebug.addToConsole("Flipping Page!");
            thisDialog = Novel.nextText();
            
            if (thisDialog != null)
            {
                if (thisDialog.getUserInteracting())
                {
                    Update(Novel.getCurrentBackground(), thisDialog);
                    displayChoices(thisDialog.getChoices());
                }
                else
                {
                    Update(Novel.getCurrentBackground(), thisDialog);
                }
            }
            else
            {
                finishNovel();
            }
           
           
            
        }

        public void setMainResources(String dialogBGPath, String nameBGPath, String ShadowPath)
        {
            BitmapImage dialogImage = new BitmapImage();
            dialogImage.BeginInit();
            dialogImage.UriSource = new Uri(dialogBGPath, UriKind.Absolute);
            dialogImage.EndInit();
            BitmapImage nameImage = new BitmapImage();
            nameImage.BeginInit();
            nameImage.UriSource = new Uri(nameBGPath, UriKind.Absolute);
            nameImage.EndInit();
            BitmapImage shadowImage = new BitmapImage();
            shadowImage.BeginInit();
            shadowImage.UriSource = new Uri(ShadowPath, UriKind.Absolute);
            shadowImage.EndInit();
            NameBG.Source = nameImage;
            TextBG.Source = dialogImage;
            Shadow.Source = shadowImage;
        }

        private void finishNovel()
        {
            this.Hide();
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            triggerNextPage();
        }

        private void MainText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            triggerNextPage();
        }

        private void CharacterImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            triggerNextPage();
        }

        private void SceneSwitcher_MouseDown(object sender, MouseButtonEventArgs e)
        {
            triggerNextPage();
        }

        private void triggerNextPage()
        {
            nextPage();
        }
    }
}
