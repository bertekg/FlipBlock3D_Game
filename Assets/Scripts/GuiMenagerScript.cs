using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuiMenagerScript : MonoBehaviour
{
    private GUIStyle gsButton, gsTopLabel;
    public Color labelColor = Color.white;
    public string sLang_SwipInfo;
    public Font myFont;
    private void Awake()
    {
        string curLangSet = PlayerPrefs.GetString("setLang", "EN");
        if (curLangSet == "PL")
        {
            sLang_SwipInfo = "Przemieść się po wszystkich polach i dojdź do FIN!";
        }
        else
        {
            sLang_SwipInfo = "Swip to move. Befor go to FIN remove all blocks!";
        }
    }
    void OnGUI()
    {
        gsButton = new GUIStyle(GUI.skin.button);
        gsButton.fontSize = Screen.width / 24;
        gsTopLabel = new GUIStyle(GUI.skin.label);
        gsTopLabel.fontSize = Screen.width / 28;
        gsTopLabel.alignment = TextAnchor.UpperCenter;
        gsTopLabel.normal.textColor = labelColor;
        gsTopLabel.font = myFont;
        GUI.Label(new Rect(10, 10, (Screen.width) - 20, Screen.height / 7), sLang_SwipInfo, gsTopLabel);        
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu2");
    }
}
