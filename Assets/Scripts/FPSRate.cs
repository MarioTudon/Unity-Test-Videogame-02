using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSRate : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate= 40;
    }
}
