using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsEvents : MonoBehaviour
{
    [HideInInspector] public bool canRestartGame = false;
    [SerializeField] private UIAnimations uIAnimations;
    [HideInInspector] public bool canPauseGame = false;
    [SerializeField] private GameObject pauseMenu;

    public void ResetGame()
    {
        if (!canRestartGame) return;
        uIAnimations.FadeOut();
    }

    public void PauseGame()
    {
        if (!canPauseGame) return;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
}
