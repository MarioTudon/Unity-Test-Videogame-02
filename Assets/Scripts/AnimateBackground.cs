using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateBackground : MonoBehaviour
{
    private SpriteRenderer backgroundMaterial;
    private PlayerMovement playerMovement;
    [SerializeField] private float animatonSpeed;


    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        backgroundMaterial = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerMovement.gameOver) return;
        backgroundMaterial.material.mainTextureOffset = new Vector2(animatonSpeed * Time.time, 0);
    }
}
