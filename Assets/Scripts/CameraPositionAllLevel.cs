using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CameraPositionAllLevel : MonoBehaviour {

    // Update is called once per frame
    private float minX, minY, maxX, maxY;
    private float halfFoV = 30;
    public float fullFovHorizontalDeg;
    private float previousScreenWidth;
    private float previousScreenHeight;
    private float screenHeihjtToCalc;
    private void Start()
    {
        minX = GameObject.Find("CellMaker").GetComponent<CellMakerScriptAllLevel>().path.Min(block => block.objectLoc.x) - 1;
        maxX = GameObject.Find("CellMaker").GetComponent<CellMakerScriptAllLevel>().path.Max(block => block.objectLoc.x) + 1;
        minY = GameObject.Find("CellMaker").GetComponent<CellMakerScriptAllLevel>().path.Min(block => block.objectLoc.y) - 1;
        maxY = GameObject.Find("CellMaker").GetComponent<CellMakerScriptAllLevel>().path.Max(block => block.objectLoc.y) + 1;
    }
    void Update ()
    {
        if(Screen.width != previousScreenWidth || Screen.height != previousScreenHeight)
        {
            previousScreenWidth = (float)Screen.width;
            previousScreenHeight = (float)Screen.height;

            screenHeihjtToCalc = ((9 * (float)Screen.width) / 7) - 10;
            Vector3 newCamPos = new Vector3();
            newCamPos.x = minX + ((maxX - minX) / 2);
            newCamPos.z = minY + ((maxY - minY) / 2);
            float yVer = ((2 + maxY - minY) / 2) / Mathf.Tan(Mathf.Deg2Rad * halfFoV);
            float halfFovHorizontal = Mathf.Atan((screenHeihjtToCalc / previousScreenHeight) * Mathf.Tan(Mathf.Deg2Rad * halfFoV));
            fullFovHorizontalDeg = 2 * Mathf.Rad2Deg * halfFovHorizontal;
            float yHor = ((2 + maxX - minX) / 2) / Mathf.Tan(halfFovHorizontal);
            if (yVer >= yHor)
            {
                newCamPos.y = yVer;
            }
            else
            {
                newCamPos.y = yHor;
            }
            transform.position = newCamPos;
        }
	}
}
