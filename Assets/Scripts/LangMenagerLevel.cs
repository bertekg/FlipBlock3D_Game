using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LangMenagerLevel : MonoBehaviour
{
    public Text textRestartLevel;
    public Text textBactToMenu;
    private void Awake()
    {
        string curLangSet = PlayerPrefs.GetString("setLang", "EN");
        if (curLangSet == "PL")
        {
            SetLangPol();
        }
        else
        {
            SetLangEng();
        }
    }
    public void SetLangPol()
    {
        textRestartLevel.text = "Powtórz poziom";
        textBactToMenu.text = "Wróć do menu głównego";
    }
    public void SetLangEng()
    {
        textRestartLevel.text = "Restart level";
        textBactToMenu.text = "Back to Main Menu";
    }
}
