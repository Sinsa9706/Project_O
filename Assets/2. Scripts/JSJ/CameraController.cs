using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera MainCamera;

    private bool isMaxY = false;

    private void Update()
    {
        if (isMaxY == true)
            MoveCamera(-0.005f);
        else
            MoveCamera(0.005f);
    }

    public void MoveCamera(float offset)
    {
        Vector3 movePos = MainCamera.transform.position;
        movePos.y = (movePos.y + offset);
        MainCamera.transform.position = movePos;

        if (MainCamera.transform.position.y >= 3.28f)
            isMaxY = true;
        if (MainCamera.transform.position.y <= 0)
            isMaxY = false;
    }
}
