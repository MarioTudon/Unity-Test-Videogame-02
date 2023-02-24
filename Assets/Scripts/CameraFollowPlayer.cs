using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private Transform playerTransform;
    private float playerXOffset;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerXOffset = transform.position.x - playerTransform.position.x;
    }

    private void Update()
    {
        transform.position = new Vector3(playerTransform.position.x + playerXOffset, transform.position.y, transform.position.z);
    }
}
