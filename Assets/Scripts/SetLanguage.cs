using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetLanguage : MonoBehaviour
{
    public Text textLanguage;
    public List<Text> textBack;
    public List<Text> textSettings;
    public Text textExit;
    public Text textSelectLevelsPack;
    public List<Text> textPrize;
    public List<Text> textInfo;
    public Text textCreatedBy;
    public Text textPrizePicks;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("setLang"))
        {            
            string curLangSet = PlayerPrefs.GetString("setLang", "EN");
            if(curLangSet == "PL")
            {
                SetLangPol();
            }
            else
            {
                SetLangEng();
            }            
        }
        else
        {
            if (Application.systemLanguage == SystemLanguage.Polish)
            {
                SetLangPol();
            }
            else
            {
                SetLangEng();
            }
        }
    }
    public void SetLangPol()
    {
        for (int i = 0; i < textSettings.Count; i++)
        {
            textSettings[i].text = "Ustawienia";
        }
        textLanguage.text = "Język";
        for (int i = 0; i < textBack.Count; i++)
        {
            textBack[i].text = "Powrót";
        }
        textExit.text = "Wyjście";
        textSelectLevelsPack.text = "Wybierz zestaw poziomów";
        for (int i = 0; i < textPrize.Count; i++)
        {
            textPrize[i].text = "Nagrody";
        }
        for (int i = 0; i < textInfo.Count; i++)
        {
            textInfo[i].text = "O grze";
        }
        textCreatedBy.text = "Stworzone przez:";
        textPrizePicks.text = "Nagrody (zdjęcia):";

        PlayerPrefs.SetString("setLang", "PL");
    }
    public void SetLangEng()
    {
        for (int i = 0; i < textSettings.Count; i++)
        {
            textSettings[i].text = "Settings";
        }
        textLanguage.text = "Language";
        for (int i = 0; i < textBack.Count; i++)
        {
            textBack[i].text = "Back";
        };
        textExit.text = "Exit";
        textSelectLevelsPack.text = "Select Levels Pack";
        for (int i = 0; i < textPrize.Count; i++)
        {
            textPrize[i].text = "Awards";
        }
        for (int i = 0; i < textInfo.Count; i++)
        {
            textInfo[i].text = "About Game";
        }
        textCreatedBy.text = "Created by:";
        textPrizePicks.text = "Awards (photos):";

        PlayerPrefs.SetString("setLang", "EN");
    }
}
