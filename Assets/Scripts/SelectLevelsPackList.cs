using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SelectLevelsPackList : MonoBehaviour
{
    [System.Serializable]
    public class LevelsPack
    {
        public string nameOfPack;
        public string nameOfPack_Pol;
        public string nameOfPack_Eng;
        public List<TextAsset> textAssetList;
        public string GetListLength()
        {
            return textAssetList.Count.ToString();
        }
    }

    public List<LevelsPack> levelPackList;
    public Transform contentPanelLevelsPack, contentPanelLevels;
    public SimpleObjectPool buttonObjectPoolLevelsPack, buttonObjectPoolLevels;

    void Start()
    {
        RefreshDisplay();
    }
    public void RefreshDisplay()
    {
        AddButtons();
    }
    public void AddButtons()
    {
        foreach (Transform child in contentPanelLevelsPack)
        {
            GameObject.Destroy(child.gameObject);
        }
        string curLangSet = PlayerPrefs.GetString("setLang", "EN");
        for (int i = 0; i < levelPackList.Count; i++)
        {
            GameObject newButton = buttonObjectPoolLevelsPack.GetObject();
            newButton.transform.SetParent(contentPanelLevelsPack);
            string tempText = "NoName";
            if (curLangSet == "PL")
            {
                tempText = levelPackList[i].nameOfPack_Pol;
            }
            else
            {
                tempText = levelPackList[i].nameOfPack_Eng;
            }
            SampleButton sampleButton = newButton.GetComponent<SampleButton>();
            sampleButton.Setup(tempText, this, i, 0);
        }
    }
    public GameObject goLevelsPacks, goLevels;
    public Text tLevelsPackName;
    public void SwitchToLevels(int index)
    {
        string curLangSet = PlayerPrefs.GetString("setLang", "EN");
        string textLevel = "NoName";
        if (curLangSet == "PL")
        {
            textLevel = "Poziom ";
            tLevelsPackName.text = levelPackList[index].nameOfPack_Pol;
        }
        else
        {
            textLevel = "Level ";
            tLevelsPackName.text = levelPackList[index].nameOfPack_Eng;
        }       
        foreach (Transform child in contentPanelLevels)
        {
            GameObject.Destroy(child.gameObject);
        }
        //contentPanelLevels.DetachChildren();
        for (int i = 0; i < levelPackList[index].textAssetList.Count; i++)
        {
            GameObject newButton = buttonObjectPoolLevels.GetObject();
            newButton.transform.SetParent(contentPanelLevels);

            SampleButton sampleButton = newButton.GetComponent<SampleButton>();
            sampleButton.Setup(textLevel + (i + 1).ToString(), this, index, i);
        }
        goLevelsPacks.SetActive(false);
        goLevels.SetActive(true);
    }
    public void SwitchToLevelsPacke()
    {
        goLevelsPacks.SetActive(true);
        goLevels.SetActive(false);
    }
    public void StartLevel(int indexLevelsPack, int indexLevel)
    {
        PlayerPrefs.SetString("LevelToLoad", levelPackList[indexLevelsPack].textAssetList[indexLevel].text);
        SceneManager.LoadScene("LevelCommon");
    }
}
