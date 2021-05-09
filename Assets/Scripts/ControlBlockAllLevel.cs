using UnityEngine;

public class ControlBlockAllLevel : MonoBehaviour
{
    public GameObject cellM;
    Transform rotator;
    public float speed = 2.0f;
    public float halfCubeSize = 0.5f;
    private bool rotating = false;
    Vector3 refPoint;
    Vector3 rotateAxis;
    float angle;
    private bool rotateSwipRight = false;
    private bool rotateSwipLeft = false;
    private bool rotateSwipUp = false;
    private bool rotateSwipDown = false;
    private int makeMoves = 0;
    private bool gameIsFinish = false;
    private string sLang_FinPerfect, sLang_FinWrong, sLang_NumberOfMoves;
    private Vector2 nextVector = new Vector2(0, 0);
    private void Awake()
    {
        string curLangSet = PlayerPrefs.GetString("setLang", "EN");
        if (curLangSet == "PL")
        {
            sLang_FinPerfect = "Ukończyłeś perfekcyjnie!!!";
            sLang_FinWrong = "Źle :( Spróbuj ponownie!!!";
            sLang_NumberOfMoves = "Ilość ruchów";
        }
        else
        {
            sLang_FinPerfect = "You Finish Perfect!!!";
            sLang_FinWrong = "Wrong :( Try Again!!!";
            sLang_NumberOfMoves = "Number of moves";
        }
    }
    private void Start()
    {
        rotator = (new GameObject("Rotator")).transform;
    }
    public void MakeFlipBlock(string dir)
    {
        switch(dir)
        {
            case "R":
                rotateSwipRight = true;
                break;
            case "L":
                rotateSwipLeft = true;
                break;
            case "U":
                rotateSwipUp = true;
                break;
            case "D":
                rotateSwipDown = true;
                break;
        }
    }
    void Update ()
    {
        if (!rotating && !gameIsFinish)
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || rotateSwipRight)
            {
                int posX = Mathf.RoundToInt(transform.position.x) + 1; int posY = Mathf.RoundToInt(transform.position.z);
                Vector2 tempVector = new Vector2(posX, posY);
                if (cellM.GetComponent<CellMakerScriptAllLevel>().CheckPathContains(tempVector))
                {
                    nextVector.x = posX + 1; nextVector.y = posY;
                    Vector2 tempeVector = new Vector2();
                    tempeVector.x = Mathf.RoundToInt(transform.position.x); tempeVector.y = Mathf.RoundToInt(transform.position.z);
                    cellM.GetComponent<CellMakerScriptAllLevel>().ChangMaterail(tempeVector);
                    rotating = true;
                    refPoint = Vector3.right * halfCubeSize;
                    rotateAxis = -Vector3.forward;
                    rotator.localRotation = Quaternion.identity;
                    rotator.position = transform.position - Vector3.up * halfCubeSize + refPoint;
                    transform.parent = rotator;
                    angle = 0;
                }                
            }            
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || rotateSwipLeft)
            {
                int posX = Mathf.RoundToInt(transform.position.x) - 1; int posY = Mathf.RoundToInt(transform.position.z);
                Vector2 tempVector = new Vector2(posX, posY);
                if (cellM.GetComponent<CellMakerScriptAllLevel>().CheckPathContains(tempVector))
                {
                    nextVector.x = posX - 1; nextVector.y = posY;
                    Vector2 tempeVector = new Vector2();
                    tempeVector.x = Mathf.RoundToInt(transform.position.x); tempeVector.y = Mathf.RoundToInt(transform.position.z);
                    cellM.GetComponent<CellMakerScriptAllLevel>().ChangMaterail(tempeVector);
                    rotating = true;
                    refPoint = -Vector3.right * halfCubeSize;
                    rotateAxis = Vector3.forward;
                    rotator.localRotation = Quaternion.identity;
                    rotator.position = transform.position - Vector3.up * halfCubeSize + refPoint;
                    transform.parent = rotator;
                    angle = 0;
                    //myRotateObject(-Vector3.right * halfCubeSize, Vector3.forward);
                }
            }
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || rotateSwipUp)
            {
                int posX = Mathf.RoundToInt(transform.position.x); int posY = Mathf.RoundToInt(transform.position.z) + 1;
                Vector2 tempVector = new Vector2(posX, posY);
                if (cellM.GetComponent<CellMakerScriptAllLevel>().CheckPathContains(tempVector))
                {
                    nextVector.x = posX; nextVector.y = posY + 1;
                    Vector2 tempeVector = new Vector2();
                    tempeVector.x = Mathf.RoundToInt(transform.position.x); tempeVector.y = Mathf.RoundToInt(transform.position.z);
                    cellM.GetComponent<CellMakerScriptAllLevel>().ChangMaterail(tempeVector);
                    rotating = true;
                    refPoint = Vector3.forward * halfCubeSize;
                    rotateAxis = Vector3.right;
                    rotator.localRotation = Quaternion.identity;
                    rotator.position = transform.position - Vector3.up * halfCubeSize + refPoint;
                    transform.parent = rotator;
                    angle = 0;
                    //myRotateObject(Vector3.forward * halfCubeSize, Vector3.right);
                }
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || rotateSwipDown)
            {
                int posX = Mathf.RoundToInt(transform.position.x); int posY = Mathf.RoundToInt(transform.position.z) - 1;
                Vector2 tempVector = new Vector2(posX, posY);
                if (cellM.GetComponent<CellMakerScriptAllLevel>().CheckPathContains(tempVector))
                {
                    nextVector.x = posX; nextVector.y = posY - 1;
                    Vector2 tempeVector = new Vector2();
                    tempeVector.x = Mathf.RoundToInt(transform.position.x); tempeVector.y = Mathf.RoundToInt(transform.position.z);
                    cellM.GetComponent<CellMakerScriptAllLevel>().ChangMaterail(tempeVector);
                    rotating = true;
                    refPoint = -Vector3.forward * halfCubeSize;
                    rotateAxis = -Vector3.right;
                    rotator.localRotation = Quaternion.identity;
                    rotator.position = transform.position - Vector3.up * halfCubeSize + refPoint;
                    transform.parent = rotator;
                    angle = 0;
                    //myRotateObject(-Vector3.forward * halfCubeSize, -Vector3.right);
                }
            }
            if((rotateSwipDown || rotateSwipUp || rotateSwipLeft || rotateSwipRight) && !rotating)
            {
                rotateSwipDown = false; rotateSwipUp = false; rotateSwipLeft = false; rotateSwipRight = false;
            }
        }
        if(rotating)
        {
            angle += Time.deltaTime * 90.0f * speed;
            rotator.rotation = Quaternion.AngleAxis(Mathf.Min(angle, 90.0f), rotateAxis);
            if(angle > 90)
            {
                transform.parent = null;
                rotating = false;
                rotateSwipRight = false; rotateSwipLeft = false; rotateSwipUp = false; rotateSwipDown = false;
                int posX = Mathf.RoundToInt(transform.position.x); int posY = Mathf.RoundToInt(transform.position.z);
                Vector2 tempVector = new Vector2(posX, posY);
                if (cellM.GetComponent<CellMakerScriptAllLevel>().CheckIsFinish(tempVector))
                {
                    gameIsFinish = true;
                    if (cellM.GetComponent<CellMakerScriptAllLevel>().CheckAllDelete())
                    {
                        GameObject.Find("GuiMenager").GetComponent<GuiMenagerScript>().labelColor = Color.green;
                        GameObject.Find("GuiMenager").GetComponent<GuiMenagerScript>().sLang_SwipInfo = sLang_FinPerfect;
                    }
                    else
                    {
                        GameObject.Find("GuiMenager").GetComponent<GuiMenagerScript>().labelColor = Color.red;
                        GameObject.Find("GuiMenager").GetComponent<GuiMenagerScript>().sLang_SwipInfo = sLang_FinWrong;
                    }
                }
                else
                {
                    makeMoves++;
                    GameObject.Find("GuiMenager").GetComponent<GuiMenagerScript>().sLang_SwipInfo = sLang_NumberOfMoves + ": " + makeMoves.ToString() + ". Is ice: " + cellM.GetComponent<CellMakerScriptAllLevel>().IsIce(tempVector).ToString() + ". Is exist: " + cellM.GetComponent<CellMakerScriptAllLevel>().CheckPathContains(nextVector).ToString();
                }                   
            }
        }
    }    
}
