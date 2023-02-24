using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickRandomBird : MonoBehaviour
{
    [SerializeField] private GameObject[] birds;

    private void Awake()
    {
        Instantiate(birds[Random.Range(0, birds.Length)], transform.position, Quaternion.identity);
    }
}
