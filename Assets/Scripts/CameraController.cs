using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(player.position.x, -2f, 74f),
            Mathf.Clamp(player.position.y, -0.8f, -2.3f),
            transform.position.z);
    }
}
