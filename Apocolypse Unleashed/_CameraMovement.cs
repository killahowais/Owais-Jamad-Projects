using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class _CameraMovement : MonoBehaviour
{
    public Transform CameraPos;

    private void Update()
    {
        transform.position = CameraPos.position;   
    }
}
