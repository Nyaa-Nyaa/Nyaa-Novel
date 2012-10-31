using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NyaaNovelWPF
{
    public class NyaaDialog
    {
        String Dialog;
        String Title;
        String CharacterImage;
        String CharacterView;
        String CharacterPosition;
        XmlNodeList Choices;
        Boolean userInteracting;
        Boolean Shadow;
        NyaaChoice choiceObj;

        public NyaaDialog(String inDialog, String inTitle, String inCharacterImage, String inCharacterPosition, String inCharacterView, Boolean inShadow)
        {
            Dialog = inDialog;
            Title = inTitle;
            CharacterImage = inCharacterImage;
            CharacterPosition = inCharacterPosition;
            CharacterView = inCharacterView;
            Shadow = inShadow;
            userInteracting = false;
        }

        public NyaaDialog(String inDialog, String inTitle, String inCharacterImage, String inCharacterPosition, String inCharacterView, Boolean inShadow, XmlNodeList inChoices)
        {
            Dialog = inDialog;
            Title = inTitle;
            CharacterImage = inCharacterImage;
            CharacterPosition = inCharacterPosition;
            CharacterView = inCharacterView;
            Shadow = inShadow;
            choiceObj = new NyaaChoice(inChoices);
            userInteracting = true;
        }

        public String getDialog()
        {
            return Dialog;
        }

        public NyaaChoice getChoices()
        {
            return choiceObj;
        }

        public String getTitle()
        {
            return Title;
        }

        public String getCharacterImage()
        {
            return CharacterImage;
        }

        public bool getUserInteracting()
        {
            return userInteracting;
        }

        public String getCharacterView()
        {
            return CharacterView;
        }

        public String getCharacterPosition()
        {
            return CharacterPosition;
        }

        public Boolean getShadow()
        {
            return Shadow;
        }

        public String getDebugString()
        {
            return "Dialog Debug: \n" + "Name of Character: " + Title + "\n Content: " + Dialog + "\n Character Image Location: " + CharacterImage + "\n Character View:" + CharacterView + "\n Charatcer Position: " + CharacterPosition + "\n Shadow: " + Shadow;
        }
    }
}
