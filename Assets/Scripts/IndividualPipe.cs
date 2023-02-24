using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualPipe : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Transform playerTransform;
    [SerializeField] private Score score;
    private bool alreadyScore;
    [SerializeField] private AudioSource scoreSFX;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Vector2 newPosition = transform.position;
        newPosition.y = Random.Range(-8f, -4f);
        transform.position = newPosition;
    }

    void Update()
    {
        CheckAPoint();
        ResetPipe();
    }

    private void CheckAPoint()
    {
        if (playerTransform.position.x > transform.position.x + 1 && !alreadyScore)
        {
            score.score++;
            alreadyScore = true;
            scoreSFX.Play();
        }
    }

    private void ResetPipe()
    {
        if (!spriteRenderer.isVisible && transform.position.x < playerTransform.position.x)
        {
            Vector2 newPosition = transform.position;
            newPosition.y = Random.Range(-8f, -4f);
            newPosition.x += 12;
            transform.position = newPosition;
            alreadyScore = false;
        }
    }
}
