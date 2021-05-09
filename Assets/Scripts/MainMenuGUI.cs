using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Xml;

[RequireComponent(typeof(AudioSource))]
public class MainMenuGUI : MonoBehaviour {

    // Use this for initialization
    private GUIStyle gs;
    enum LevelState { mainMenu, help, aboutGame, allLevels, basicLevels, advenceLevels, setting};
    public List<TextAsset> lAllAdvance;
    public List<TextAsset> lAllBasic;
    LevelState globalLevelState = LevelState.mainMenu;
    public AudioSource clickSound;
    enum SuppLang { PL, EN};
    SuppLang currLang = SuppLang.EN;
    private string sLang_AllLevels, sLang_Help, sLang_Setting, sLang_AboutGame, sLang_MainMenu, sLang_Language, sLang_BasicLevels, sLang_AdvenceLevels, sLang_Level, sLang_InfoContent, sLang_CratedBy, sLang_Tools;
    private void Awake()
    {        
        if(PlayerPrefs.HasKey("setLang"))
        {
            GetLangPrefab();
        }
        else
        {
            if (Application.systemLanguage == SystemLanguage.Polish)
            {
                PlayerPrefs.SetString("setLang", "PL");
                currLang = SuppLang.PL;
            }
            else
            {
                PlayerPrefs.SetString("setLang", "EN");
                currLang = SuppLang.EN;
            }
            SetLangPrefab();
        }
        PlayerPrefs.SetInt("iIsDebugMode", 0);
    }
    private void SetLangPrefab()
    {
        if(currLang == SuppLang.EN)
        {
            sLang_AllLevels = "All Levels";
            sLang_Help = "Help";
            sLang_Setting = "Setting";
            sLang_AboutGame = "About Game";
            sLang_MainMenu = "Main Menu";
            sLang_Language = "Language";
            sLang_BasicLevels = "Basic Levels";
            sLang_AdvenceLevels = "Advence Levels";
            sLang_Level = "Level";
            sLang_InfoContent = "A 3D simple puzzle game.\nTry clear all level before\nreach Finish. Move block\nby swipe touch screen.\nIt cannot be so difficult :)";
            sLang_CratedBy = "Created by";
            sLang_Tools = "Tools";

            PlayerPrefs.SetString("sLang_RestartLevel", "Restart Level");
            PlayerPrefs.SetString("sLang_SwipInfo", "Swip to move. Befor go to FIN remove all blocks!");
            PlayerPrefs.SetString("sLang_FinPerfect", "You Finish Perfect!!!");
            PlayerPrefs.SetString("sLang_FinWrong", "Wrong :( Try Again!!!");
            PlayerPrefs.SetString("sLang_NumberOfMoves", "Number of moves");
        }
        if(currLang == SuppLang.PL)
        {
            sLang_AllLevels = "Wszystkie poziomy";
            sLang_Help = "Pomoc";
            sLang_Setting = "Ustawienia";
            sLang_AboutGame = "O grze";
            sLang_MainMenu = "Menu główne";
            sLang_Language = "Język";
            sLang_BasicLevels = "Poziomy podstawowe";
            sLang_AdvenceLevels = "Poziomy zaawansowane ";
            sLang_Level = "Poziom";
            sLang_CratedBy = "Stworzone przez";
            sLang_Tools = "Narzędzia";
            sLang_InfoContent = "Proste logiczna gra 3D.\nWyczyść cały poziom przed\nosiągnięciem końca. Poruszaj się\npoprzez przesuwaniem palca.\nTo nie możę być trudne :)";
            
            PlayerPrefs.SetString("sLang_RestartLevel", "Ponów poziomu");
            PlayerPrefs.SetString("sLang_SwipInfo","Przemieść się po wszystkich polach i dojdź do FIN!");
            PlayerPrefs.SetString("sLang_FinPerfect", "Ukończyłeś perfekcyjnie!!!");
            PlayerPrefs.SetString("sLang_FinWrong", "Źle :( Spróbuj ponownie!!!");
            PlayerPrefs.SetString("sLang_NumberOfMoves", "Ilość ruchów");
        }
        PlayerPrefs.SetString("sLang_AllLevels", sLang_AllLevels);
        PlayerPrefs.SetString("sLang_Help", sLang_Help);
        PlayerPrefs.SetString("sLang_Setting", sLang_Setting);
        PlayerPrefs.SetString("sLang_AboutGame", sLang_AboutGame);
        PlayerPrefs.SetString("sLang_MainMenu", sLang_MainMenu);
        PlayerPrefs.SetString("sLang_Language", sLang_Language);
        PlayerPrefs.SetString("sLang_BasicLevels", sLang_BasicLevels);
        PlayerPrefs.SetString("sLang_AdvenceLevels", sLang_AdvenceLevels);
        PlayerPrefs.SetString("sLang_Level", sLang_Level);
        PlayerPrefs.SetString("sLang_CratedBy", sLang_CratedBy);
        PlayerPrefs.SetString("sLang_Tools", sLang_Tools);
        PlayerPrefs.SetString("sLang_InfoContent", sLang_InfoContent);
    }
    private void GetLangPrefab()
    {
        sLang_AllLevels = PlayerPrefs.GetString("sLang_AllLevels", "All Levels");
        sLang_Help = PlayerPrefs.GetString("sLang_Help", "Help");
        sLang_Setting = PlayerPrefs.GetString("sLang_Setting", "Setting");
        sLang_AboutGame = PlayerPrefs.GetString("sLang_AboutGame", "About Game");
        sLang_MainMenu = PlayerPrefs.GetString("sLang_MainMenu", "Main Menu");
        sLang_Language = PlayerPrefs.GetString("sLang_Language", "Language");
        sLang_BasicLevels = PlayerPrefs.GetString("sLang_BasicLevels", "Basic Levels");
        sLang_AdvenceLevels = PlayerPrefs.GetString("sLang_AdvenceLevels", "Advence Levels");
        sLang_Level = PlayerPrefs.GetString("sLang_Level", "Level");
        sLang_CratedBy = PlayerPrefs.GetString("sLang_CratedBy", "Created by");
        sLang_Tools = PlayerPrefs.GetString("sLang_Tools", "Tools");
        sLang_InfoContent = PlayerPrefs.GetString("sLang_InfoContent", "A 3D simple puzzle game.\nTry clear all level before\nreach Finish. Move block\nby swipe touch screen.\nIt cannot be so difficult :)");
    }
    void Start()
    {
        clickSound = Camera.main.GetComponent<AudioSource>();
        for (int i = 0; i < lAllAdvance.Count; i++)
        {
            string data = lAllAdvance[i].text;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(new StringReader(data));

            string xmlPathPattern = "//Level/Kafelek";
            XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);
            foreach (XmlNode node in myNodeList)
            {
                XmlNode PosX = node.FirstChild;
                XmlNode PosY = PosX.NextSibling;
                XmlNode Number = PosY.NextSibling;
            }
        }        
    }
    void OnGUI()
    {
        if (globalLevelState == LevelState.mainMenu)
        {
            GUIStyle gS = new GUIStyle();
            gS.fontSize = Screen.width / 8; gS.alignment = TextAnchor.MiddleCenter;
            //GUI.Label (new Rect (25, 25, 100, 30), "Label");
            GUI.Label(new Rect(10, 10 , Screen.width - 20, Screen.height / 3), "Flip Block 3D", gS);
            gS = new GUIStyle(GUI.skin.button);
            gS.fontSize = Screen.width / 12;
            if (GUI.Button(new Rect(10, 2 * Screen.height / 6, Screen.width - 20, Screen.height / 7), sLang_AllLevels, gS))
            {
                clickSound.Play();
                globalLevelState = LevelState.allLevels;
            }
            if (GUI.Button(new Rect(10, 3 * Screen.height / 6, Screen.width - 20, Screen.height / 7), sLang_Help, gS))
            {
                clickSound.Play();
                globalLevelState = LevelState.help;
            }
            if (GUI.Button(new Rect(10, 4 * Screen.height / 6, Screen.width - 20, Screen.height / 7), sLang_Setting, gS))
            {
                clickSound.Play();
                globalLevelState = LevelState.setting;
            }
                if (GUI.Button(new Rect(10, 5 * Screen.height / 6, Screen.width - 20, Screen.height / 7), sLang_AboutGame, gS))
            {
                clickSound.Play();
                globalLevelState = LevelState.aboutGame;
            }
        }        
        if (globalLevelState == LevelState.allLevels)
        {
            GUIStyle gS = new GUIStyle();
            gS.fontSize = Screen.width / 16; gS.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(10, 10, Screen.width - 20, (Screen.height / 6) - 20), sLang_AllLevels, gS);
            gS = new GUIStyle(GUI.skin.button);
            gS.fontSize = Screen.width / 12;
            if (GUI.Button(new Rect(10, 2 * Screen.height / 6, Screen.width - 20, (Screen.height / 6) - 20), sLang_BasicLevels, gS))
            {
                clickSound.Play();
                globalLevelState = LevelState.basicLevels;
            }
			if (GUI.Button(new Rect(10, 3 * Screen.height / 6, Screen.width - 20, (Screen.height / 6) - 20), sLang_AdvenceLevels, gS))
            {
                clickSound.Play();
                globalLevelState = LevelState.advenceLevels;
            }
            /*
            if (GUI.Button(new Rect(10, 4 * Screen.height / 6, Screen.width - 20, (Screen.height / 6) - 20), "Debug", gS))
            {
                clickSound.Play();
                PlayerPrefs.SetInt("iIsDebugMode", 1);
                SceneManager.LoadScene("LevelCommon");
            }
            */
            if (GUI.Button(new Rect(10, 5 * Screen.height / 6, Screen.width - 20, (Screen.height / 6) - 20), sLang_MainMenu, gS))
            {
                clickSound.Play();
                globalLevelState = LevelState.mainMenu;
            }
        }
        if (globalLevelState == LevelState.help)
        {
            GUIStyle gS = new GUIStyle();
            gS.fontSize = Screen.width / 16; gS.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(10, 10, Screen.width - 20, (6 * Screen.height / 7) - 20), sLang_InfoContent, gS);
            gS = new GUIStyle(GUI.skin.button);
            gS.fontSize = Screen.width / 12;
            if (GUI.Button(new Rect(10, 5 * Screen.height / 6, Screen.width - 20, Screen.height / 7), sLang_MainMenu, gS))
            {
                clickSound.Play();
                globalLevelState = LevelState.mainMenu;
            }
        }
        if (globalLevelState == LevelState.aboutGame)
        {
            GUIStyle gS = new GUIStyle();
            gS.fontSize = Screen.width / 16; gS.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(10, 10, Screen.width - 20, (6 * Screen.height / 7) - 20), sLang_CratedBy + ":\nBartłomiej Grywalski\n\n" + sLang_Tools + ":\nUnity\nInkscape", gS);            
            gS = new GUIStyle(GUI.skin.button);
            gS.fontSize = Screen.width / 12;
            if (GUI.Button(new Rect(10, 5 * Screen.height / 6, Screen.width - 20, Screen.height / 7), sLang_MainMenu, gS))
            {
                clickSound.Play();
                globalLevelState = LevelState.mainMenu;
            }
        }
        if (globalLevelState == LevelState.basicLevels)
        {
            GUIStyle gS = new GUIStyle();
            gS.fontSize = Screen.width / 16; gS.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(10, 10, Screen.width - 20, (Screen.height / 7) - 20), sLang_BasicLevels, gS);
            gS = new GUIStyle(GUI.skin.button);
            gS.fontSize = Screen.width / 16;
            for (int i = 0; i < lAllBasic.Count; i++)
            {
                if (GUI.Button(new Rect(10, Screen.height / 7 + 10 + (i * 5 * Screen.height / 7) / lAllBasic.Count, Screen.width - 20, (5 * (Screen.height / 7) / lAllBasic.Count) - 20), sLang_Level + " " + (i + 1).ToString(), gS))
                {
                    clickSound.Play();
                    PlayerPrefs.SetString("LevelToLoad", lAllBasic[i].text);
                    SceneManager.LoadScene("LevelCommon");
                }
            }
            gS.fontSize = Screen.width / 18;
            if (GUI.Button(new Rect(10, 6 * Screen.height / 7, (Screen.width/2) - 20, (Screen.height / 7) - 20), sLang_MainMenu, gS))
            {
                clickSound.Play();
                globalLevelState = LevelState.mainMenu;
            }
			if (GUI.Button(new Rect((Screen.width/2) +10, 6 * Screen.height / 7, (Screen.width/2) - 20, (Screen.height / 7) - 20), sLang_AllLevels, gS))
            {
                clickSound.Play();
                globalLevelState = LevelState.allLevels;
            }
        }
		if (globalLevelState == LevelState.advenceLevels)
        {
			GUIStyle gS = new GUIStyle();
            gS.fontSize = Screen.width / 16; gS.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(10, 10, Screen.width - 20, (Screen.height / 7) - 20), sLang_AdvenceLevels, gS);
            gS = new GUIStyle(GUI.skin.button);
            gS.fontSize = Screen.width / 16;
			for(int i = 0; i < lAllAdvance.Count; i++)
			{
				if (GUI.Button(new Rect(10, Screen.height / 7 + 10 + (i * 5*Screen.height / 7)/lAllAdvance.Count, Screen.width - 20, (5*(Screen.height / 7)/lAllAdvance.Count) - 20), sLang_Level + " " + (i+1).ToString(), gS))
				{
                    clickSound.Play();
                    PlayerPrefs.SetString("LevelToLoad", lAllAdvance[i].text);
                    SceneManager.LoadScene("LevelCommon");
                }
			}
            gS.fontSize = Screen.width / 18;
            if (GUI.Button(new Rect(10, 6 * Screen.height / 7, (Screen.width/2) - 20, (Screen.height / 7) - 20), sLang_MainMenu, gS))
            {
                clickSound.Play();
                globalLevelState = LevelState.mainMenu;
            }
			if (GUI.Button(new Rect((Screen.width/2) +10, 6 * Screen.height / 7, (Screen.width/2) - 20, (Screen.height / 7) - 20), sLang_AllLevels, gS))
            {
                clickSound.Play();
                globalLevelState = LevelState.allLevels;
            }
		}
        if(globalLevelState == LevelState.setting)
        {
            GUIStyle gS = new GUIStyle();
            gS.fontSize = Screen.width / 16; gS.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(10, 10 + Screen.height / 6, Screen.width - 20, (Screen.height / 7) - 20), sLang_Language, gS);
            gS = new GUIStyle(GUI.skin.button);
            gS.fontSize = Screen.width / 16;
            if (GUI.Button(new Rect(10, 10 + 2 * Screen.height / 6, Screen.width / 2 - 20, Screen.height / 7), "English", gS))
            {
                clickSound.Play();
                PlayerPrefs.SetString("setLang", "EN");
                currLang = SuppLang.EN;
                SetLangPrefab();
            }
            if (GUI.Button(new Rect(10 + Screen.width / 2, 10 + 2 * Screen.height / 6, Screen.width / 2 - 20, Screen.height / 7), "Polski", gS))
            {
                clickSound.Play();
                PlayerPrefs.SetString("setLang", "PL");
                currLang = SuppLang.PL;
                SetLangPrefab();
            }
            if (GUI.Button(new Rect(10, 5 * Screen.height / 6, Screen.width - 20, Screen.height / 7), sLang_MainMenu, gS))
            {
                clickSound.Play();
                globalLevelState = LevelState.mainMenu;
            }
        }
    }
}
