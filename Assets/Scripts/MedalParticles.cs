using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedalParticles : MonoBehaviour
{
    public void ActivateParticles()
    {
        gameObject.SetActive(true);
    }

    public void DeactivateParticles()
    {
        gameObject.SetActive(false);
    }
}
