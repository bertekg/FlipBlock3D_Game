using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
public class CellMakerScriptAllLevel : MonoBehaviour
{
    public GameObject useObeject;
    public List<CellObject> path = new List<CellObject>();
    private CellObject tempCellObject;

    public Material mat1, mat2, mat3, mat4, fin;
    public Material ice, ice1, ice2, ice3, ice4;

    // Use this for initialization
    public class CellObject
    {
        public Vector2 objectLoc;
        public CellType typeCell = CellType.None;
        public int possibleStep;
    }
    public enum CellType {None, Finish, Normal, Ice};
    private int bIsDebugMode = 0;
    private void Awake()
    {
        bIsDebugMode = PlayerPrefs.GetInt("iIsDebugMode", 0);
    }
    void Start()
    {
        string sLevelToLoad = "";
        if (bIsDebugMode == 1)
        {
            string sFolderPath = Application.dataPath + "/!Debug";
            if(Directory.Exists(sFolderPath))
            {
                string[] files = System.IO.Directory.GetFiles(sFolderPath, "*.xml");
                if(files.Length>=1)
                {
                    using (StreamReader sr = File.OpenText(files[0]))
                    {
                        string s = "";
                        while ((s = sr.ReadLine()) != null)
                        {
                            sLevelToLoad += s;
                        }
                    }
                }                
            }            
        }
        else
        {
            sLevelToLoad = PlayerPrefs.GetString("LevelToLoad", "Nothing");
        }
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(sLevelToLoad));

        string xmlPathPattern = "//ArrayOfCell/Cell";
        XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);
        path = new List<CellObject>();
        foreach (XmlNode node in myNodeList)
        {
            XmlNode PosX = node.FirstChild;
            XmlNode PosY = PosX.NextSibling;
            XmlNode Type = PosY.NextSibling;
            XmlNode Number = Type.NextSibling;
            tempCellObject = new CellObject();
            tempCellObject.objectLoc.x = float.Parse(PosX.InnerXml);
            tempCellObject.objectLoc.y = float.Parse(PosY.InnerXml);
            tempCellObject.typeCell = (CellType)System.Enum.Parse(typeof(CellType), Type.InnerXml);
            tempCellObject.possibleStep = int.Parse(Number.InnerXml);
            path.Add(tempCellObject);
            //totVal += " PosX : " + PosX.InnerXml + "\t PosY : " + PosY.InnerXml + "\t Number : " + Number.InnerXml + "\n";            
        }
        Vector3 tempRotVector = new Vector3(0, 1, 0);
        for (int i = 0; i < path.Count; i++)
        {
            GameObject plane = GameObject.Instantiate(useObeject);
            plane.transform.position = new Vector3(path[i].objectLoc.x, 0, path[i].objectLoc.y);
            plane.transform.rotation = Quaternion.AngleAxis(180, tempRotVector);
            plane.name = "Cell" + i.ToString();
            if (path[i].typeCell == CellType.Finish)
            {
                GameObject.Find(plane.name).GetComponent<Renderer>().material = fin;
            }
            else if(path[i].typeCell == CellType.Normal)
            {
                switch (path[i].possibleStep)
                {
                    case 1:
                        GameObject.Find(plane.name).GetComponent<Renderer>().material = mat1;
                        break;
                    case 2:
                        GameObject.Find(plane.name).GetComponent<Renderer>().material = mat2;
                        break;
                    case 3:
                        GameObject.Find(plane.name).GetComponent<Renderer>().material = mat3;
                        break;
                    case 4:
                        GameObject.Find(plane.name).GetComponent<Renderer>().material = mat4;
                        break;
                }
            }
            else if(path[i].typeCell == CellType.Ice)
            {
                switch (path[i].possibleStep)
                {
                    case 1:
                        GameObject.Find(plane.name).GetComponent<Renderer>().material = ice1;
                        break;
                    case 2:
                        GameObject.Find(plane.name).GetComponent<Renderer>().material = ice2;
                        break;
                    case 3:
                        GameObject.Find(plane.name).GetComponent<Renderer>().material = ice3;
                        break;
                    case 4:
                        GameObject.Find(plane.name).GetComponent<Renderer>().material = ice4;
                        break;
                }
            }
        }
    }
    public bool CheckPathContains(Vector2 targetVector)
    {
        for (int i = 0; i < path.Count; i++)
        {
            if (path[i].objectLoc == targetVector)
            {
                if (path[i].possibleStep > 0 || path[i].typeCell == CellType.Finish)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool CheckIsFinish(Vector2 targetVector)
    {
        for (int i = 0; i < path.Count; i++)
        {
            if (path[i].objectLoc == targetVector)
            {
                if (path[i].typeCell == CellType.Finish)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool CheckAllDelete()
    {
        for (int i = 0; i < path.Count; i++)
        {
            if (path[i].possibleStep > 0)
            {
                return false;
            }
        }
        return true;
    }
    public void ChangMaterail(Vector2 vec)
    {
        int ansOb = 0;
        for (int i = 0; i < path.Count; i++)
        {
            if (path[i].objectLoc == vec)
            {
                ansOb = i;
            }
        }
        string objectName = "Cell" + ansOb.ToString();
        CellType matType = path[ansOb].typeCell;
        int mat = path[ansOb].possibleStep - 1;
        switch(matType)
        {
            case CellType.Normal:
                {
                    switch (mat)
                    {
                        case 0:
                            Destroy(GameObject.Find(objectName));
                            break;
                        case 1:
                            GameObject.Find(objectName).GetComponent<Renderer>().material = mat1;
                            break;
                        case 2:
                            GameObject.Find(objectName).GetComponent<Renderer>().material = mat2;
                            break;
                        case 3:
                            GameObject.Find(objectName).GetComponent<Renderer>().material = mat3;
                            break;
                        case 4:
                            GameObject.Find(objectName).GetComponent<Renderer>().material = mat4;
                            break;
                    }
                    break;
                }
            case CellType.Ice:
                {
                    switch(mat)
                    {
                        case 0:
                            Destroy(GameObject.Find(objectName));
                            break;
                        case 1:
                            GameObject.Find(objectName).GetComponent<Renderer>().material = ice1;
                            break;
                        case 2:
                            GameObject.Find(objectName).GetComponent<Renderer>().material = ice2;
                            break;
                        case 3:
                            GameObject.Find(objectName).GetComponent<Renderer>().material = ice3;
                            break;
                        case 4:
                            GameObject.Find(objectName).GetComponent<Renderer>().material = ice4;
                            break;
                    }
                    break;
                }
        }        
        path[ansOb].possibleStep = mat;
    }
    public bool IsIce(Vector2 currentVector)
    {
        for (int i = 0; i < path.Count; i++)
        {
            if (path[i].objectLoc == currentVector)
            {
                if (path[i].possibleStep > 0 && path[i].typeCell == CellType.Ice)
                {
                    return true;
                }
            }
        }
        return false;
    }
}