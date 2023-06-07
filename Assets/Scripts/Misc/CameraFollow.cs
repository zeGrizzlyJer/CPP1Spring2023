using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float minX;
    public float maxX;

    public void LateUpdate()
    {
        Vector3 cameraPos;

        cameraPos = transform.position;
        cameraPos.x = Mathf.Clamp(GameManager.instance.playerInstance.transform.position.x, minX, maxX);

        transform.position = cameraPos;
    }
}
