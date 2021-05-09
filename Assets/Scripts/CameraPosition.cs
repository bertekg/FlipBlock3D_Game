using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour {

    // Update is called once per frame
    public float minX, minY, maxX, maxY;
    private float halfFoV = 30;
    public float fullFovHorizontalDeg;
    private float previousScreenWidth;
    private float previousScreenHeight;

    void Update ()
    {
        if(Screen.width != previousScreenWidth || Screen.height != previousScreenHeight)
        {
            previousScreenWidth = (float)Screen.width;
            previousScreenHeight = (float)Screen.height;
            Vector3 newCamPos = new Vector3();
            newCamPos.x = minX + ((maxX - minX) / 2);
            newCamPos.z = minY + ((maxY - minY) / 2);
            float yVer = ((2 + maxY - minY) / 2) / Mathf.Tan(Mathf.Deg2Rad * halfFoV);
            float halfFovHorizontal = Mathf.Atan((previousScreenWidth / previousScreenHeight) * Mathf.Tan(Mathf.Deg2Rad * halfFoV));
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
