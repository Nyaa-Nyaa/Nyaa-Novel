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
        double nameX;
        double mainX;
        double textNameX;
        double textDialogX;
        Boolean textAnimating;

        public NyaaOutput(NyaaNovel novelToControl, DebugConsole NyaaDebugPointer)
        {
            InitializeComponent();
            NyaaDebug = NyaaDebugPointer;
            Novel = novelToControl;
            mainX = 0;
            nameX = 0;
            textNameX = 10;
            textDialogX = 10;
            CurrentCharPath = "";
            CurrentBGPath = "";
            textAnimating = false;
            nextPage();
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
                setNameText(CurrentDialog.getTitle());
                setCharacterImage(CurrentDialog.getCharacterImage());
                setShadow(CurrentDialog.getShadow());
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
                        BitmapImage bmImage = new BitmapImage();
                        bmImage.BeginInit();
                        bmImage.UriSource = new Uri(imagePath, UriKind.Absolute);
                        bmImage.EndInit();
                        CharacterImage.Source = bmImage;
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
        /*
        private void animateNameText(int Length)
        {
            int target;
            if (Length == 0)
            { 
                target = -1280; 
            }
            else
            { 
                target = Length * 10 + 50; 
            }

            int targetText;
            if (Length == 0)
            {
                targetText = -250;
            }
            else
            {
                targetText = 10;
            }

            NyaaDebug.addToConsole("Notice: Target Aimation: " + target + " Old frame: " + nameX);
            var left = nameX;
            TranslateTransform trans = new TranslateTransform();
            NameBG.RenderTransform = trans;
            TranslateTransform textTrans = new TranslateTransform();
            NameText.RenderTransform = textTrans;
            double newX = target;
            DoubleAnimation anim2 = new DoubleAnimation(nameX, newX, TimeSpan.FromSeconds(.4));
            DoubleAnimation anim3 = new DoubleAnimation(textNameX, targetText, TimeSpan.FromSeconds(.5));
            trans.BeginAnimation(TranslateTransform.XProperty, anim2);
            textTrans.BeginAnimation(TranslateTransform.XProperty, anim3);
            nameX = newX;
            textNameX = targetText;
        }

        private void animateDialogText(int Length)
        {
            int target;
            if (Length == 0)
            {
                target = -1280;
            }
            else
            {
                target = 0;
            }

            int targetText;
            if (Length == 0)
            {
                targetText = -1280;
            }
            else
            {
                targetText = 5;
            }

            NyaaDebug.addToConsole("Notice: Target Aimation: " + target + " Old frame: " + nameX);
            var left = mainX;
            TranslateTransform trans = new TranslateTransform();
            TextBG.RenderTransform = trans;
            TranslateTransform textTrans = new TranslateTransform();
            MainText.RenderTransform = textTrans;
            double newX = target;
            DoubleAnimation anim2 = new DoubleAnimation(mainX, newX, TimeSpan.FromSeconds(.4));
            trans.BeginAnimation(TranslateTransform.XProperty, anim2);
            DoubleAnimation anim3 = new DoubleAnimation(textDialogX, targetText, TimeSpan.FromSeconds(.5));
            textTrans.BeginAnimation(TranslateTransform.XProperty, anim3);
            mainX = newX;
            textDialogX = targetText;
        }
        
        public void MoveTo(Image target, double newX, double newY, double oldX, double oldY)
        {
            
            var top = oldY;
            var left = oldX;
            TranslateTransform trans = new TranslateTransform();
            target.RenderTransform = trans;
            DoubleAnimation anim1 = new DoubleAnimation(0, newY - top, TimeSpan.FromSeconds(.25));
            DoubleAnimation anim2 = new DoubleAnimation(0, newX - left, TimeSpan.FromSeconds(.25));
            trans.BeginAnimation(TranslateTransform.YProperty, anim1);
            trans.BeginAnimation(TranslateTransform.XProperty, anim2);
        }
      */
        private void nextPage()
        {
            
            NyaaDialog CurrentDialog = Novel.nextText();
            if (CurrentDialog != null)
            {
                Update(Novel.getCurrentBackground(), CurrentDialog);
                
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
            nextPage();
        }

        private void MainText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            nextPage();
        }

        private void CharacterImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            nextPage();
        }

        private void SceneSwitcher_MouseDown(object sender, MouseButtonEventArgs e)
        {
            nextPage();
        }
    }
}
