using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [HideInInspector] public int score = 0;

    private void Start()
    {
        score = 0;
        score = 0;
    }

    void Update()
    {
        scoreText.text = "" + score;
    }
}
