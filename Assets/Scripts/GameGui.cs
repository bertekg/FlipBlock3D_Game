using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Xml;

public class GameGui : MonoBehaviour {

    private GUIStyle gsButton, gsTopLabel;
    public Color labelColor = Color.white;
    public string sLang_SwipInfo;
    public Font myFont;
    //private string sLang_RestartLevel, sLang_MainMenu;
    private void Awake()
    {
        string curLangSet = PlayerPrefs.GetString("setLang", "EN");
        if(curLangSet == "PL")
        {
            //sLang_RestartLevel = "Ponów poziomu";
            //sLang_MainMenu = "Menu główne";
            sLang_SwipInfo = "Przemieść się po wszystkich polach i dojdź do FIN!";
        }
        else
        {
            //sLang_RestartLevel = "Restart Level";
            //sLang_MainMenu = "Main Menu";
            sLang_SwipInfo = "Swip to move. Befor go to FIN remove all blocks!";
        }
    }
    void OnGUI()
    {
        //GUI.Label (new Rect (25, 25, 100, 30), "Label");
        gsButton = new GUIStyle(GUI.skin.button);
        gsButton.fontSize = Screen.width / 24;
        gsTopLabel = new GUIStyle(GUI.skin.label);
        gsTopLabel.fontSize = Screen.width / 28;
        gsTopLabel.alignment = TextAnchor.UpperCenter;
        gsTopLabel.normal.textColor = labelColor;
        gsTopLabel.font = myFont;
        GUI.Label(new Rect(10, 10, (Screen.width) - 20, Screen.height / 7), sLang_SwipInfo, gsTopLabel);
        /*
        if (GUI.Button(new Rect(10, 5 * Screen.height / 6, (Screen.width/2) - 20, Screen.height / 7), sLang_RestartLevel, gsButton))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (GUI.Button(new Rect(10 + (Screen.width / 2), 5 * Screen.height / 6, (Screen.width / 2) - 20, Screen.height / 7), sLang_MainMenu, gsButton))
        {
            //SceneManager.LoadScene("MainMenu");
            SceneManager.LoadScene("MainMenu2");
        } 
        */
    }    
}
