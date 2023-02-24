using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIAnimations : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject introMessages;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject gameOverGroup;
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text bestScoreText;
    [SerializeField] private GameObject okButton;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject hitPanel;
    [SerializeField] private GameObject fadePanel;
    [SerializeField] private GameObject[] medal;
    [Header("Scripts")]
    [SerializeField] private Score score;
    [SerializeField] private ButtonsEvents buttonsEvents;
    [SerializeField] private MedalParticles medalParticles;
    [HideInInspector] public bool canStartGame = false;

    private void Awake()
    {
        bestScoreText.text = "" + PlayerPrefs.GetInt("Best Score");
        buttonsEvents.canRestartGame = false;
        canStartGame = false;
        LeanTween.alpha(fadePanel.GetComponent<RectTransform>(), 0, 0.5f).setOnComplete(CanStartGame);
    }

    private void CanStartGame()
    {
        canStartGame = true;
    }

    public void GameStarted()
    {
        LeanTween.alphaCanvas(introMessages.GetComponent<CanvasGroup>(), 0, 0.3f);
        LeanTween.scale(pauseButton, Vector2.one * 9f, 0.8f).setEaseOutBack().setOnComplete(CanPauseGame);
    }
    private void CanPauseGame()
    {
        buttonsEvents.canPauseGame = true;
    }

    public void Hit()
    {
        buttonsEvents.canPauseGame = false;
        LeanTween.alpha(hitPanel.GetComponent<RectTransform>(), 0.8f, 0.09f).setLoopPingPong(1).setOnComplete(GameOver);
    }

    public void GameOver()
    {
        LeanTween.scale(pauseButton, Vector2.zero, 0.4f).setEaseInBack();
        LeanTween.alphaCanvas(gameOverGroup.GetComponent<CanvasGroup>(), 1, 0.3f);
        LeanTween.moveY(gameOver.GetComponent<RectTransform>(), -430f, 0.2f).setLoopPingPong(1);
        LeanTween.moveY(scorePanel.GetComponent<RectTransform>(), -100, 1f).setDelay(0.5f).setEaseOutQuint();
        LeanTween.value(0, score.score, score.score * 0.1f).setOnUpdate((float val) => {
            scoreText.text = "" + Mathf.RoundToInt(val);
        }).setDelay(1.3f).setEaseOutCubic().setOnComplete(Medals);
    }
    private void Medals()
    {
        if (score.score >= 10 && score.score < 20)
        {
            LeanTween.scale(medal[0], Vector2.one, 1f).setEaseOutBounce().setOnComplete(ScorePanel);
            medalParticles.ActivateParticles();
        }
        else if (score.score >= 20 && score.score < 30)
        {
            LeanTween.scale(medal[1], Vector2.one, 1f).setEaseOutBounce().setOnComplete(ScorePanel);
            medalParticles.ActivateParticles();
        }
        else if (score.score >= 30 && score.score < 40)
        {
            LeanTween.scale(medal[2], Vector2.one, 1f).setEaseOutBounce().setOnComplete(ScorePanel);
            medalParticles.ActivateParticles();
        }
        else
        {
            ScorePanel();
        }
    }

    private void ScorePanel()
    {
        if (score.score > PlayerPrefs.GetInt("Best Score"))
        {
            PlayerPrefs.SetInt("Best Score", score.score);
            bestScoreText.text = "" + PlayerPrefs.GetInt("Best Score");
            LeanTween.scale(bestScoreText.gameObject, Vector2.one * 1.2f, 0.2f).setLoopPingPong(1);
            LeanTween.scale(okButton, Vector2.one, 0.5f).setEaseOutBack().setDelay(0.9f).setOnComplete(CanRestartGame);
        }
        else
        {
            LeanTween.scale(okButton, Vector2.one, 0.5f).setEaseOutBack().setDelay(0.4f).setOnComplete(CanRestartGame);
        }
    }

    private void CanRestartGame()
    {
        buttonsEvents.canRestartGame = true;
    }

    public void FadeOut()
    {
        LeanTween.alpha(fadePanel.GetComponent<RectTransform>(), 1, 0.5f).setOnComplete(LoadScene);
        medalParticles.DeactivateParticles();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Game");
    }

    
}
