using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopGround : MonoBehaviour
{
    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform; 
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < cameraTransform.position.x - 7)
        {
            Vector2 newPosition = transform.position;
            newPosition.x += 14;
            transform.position = newPosition;
        }
    }
}
