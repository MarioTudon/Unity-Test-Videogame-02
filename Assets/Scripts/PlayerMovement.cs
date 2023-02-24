using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rB2D;
    [SerializeField] private float horizontalSpeed = 2f;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float originalGravity = 0f;
    [HideInInspector] public bool gameStarted = false;
    [SerializeField] private UIAnimations uIAnimations;
    [SerializeField] private AudioSource flapSFX;
    [SerializeField] private AudioSource loseSFX;
    private CinemachineScreenShake cinemachineScreenShake;
    [HideInInspector] public bool gameOver;
    private Animator playerAnimator;

    private void Start()
    {
        cinemachineScreenShake = GameObject.FindGameObjectWithTag("Virtual Shake Camera").GetComponent<CinemachineScreenShake>();
        uIAnimations = GameObject.FindGameObjectWithTag("UI").GetComponent<UIAnimations>();
        playerAnimator = GetComponent<Animator>();
        originalGravity = rB2D.gravityScale;
        gameStarted = false;
        rB2D.gravityScale = 0;
        InitialAnimation();
    }

    private void InitialAnimation()
    {
        LeanTween.moveLocalY(this.gameObject, 0.8f, 0.65f).setLoopPingPong();
    }

    void FixedUpdate()
    {
        if (gameOver) return;

        rB2D.velocity = new Vector2(horizontalSpeed, rB2D.velocity.y);

        if (Input.GetMouseButtonDown(0) && gameStarted)
        {
            rB2D.velocity = new Vector2(rB2D.velocity.x, 0f);
            rB2D.velocity = new Vector2(rB2D.velocity.x, jumpForce);
            playerAnimator.SetTrigger("Flap");
            flapSFX.Play();
        }
    }

    private void Update()
    {
        ClampHeight();
        RotatePlayer();
        StartGame();
    }

    private void StartGame()
    {
        if (Input.GetMouseButtonDown(0) && !gameStarted && uIAnimations.canStartGame)
        {
            gameStarted = true;
            rB2D.gravityScale = originalGravity;
            rB2D.velocity = new Vector2(rB2D.velocity.x, jumpForce);
            flapSFX.Play();
            playerAnimator.SetTrigger("Flap");
            uIAnimations.GameStarted();
            LeanTween.cancel(this.gameObject);
        }
    }

    private void RotatePlayer()
    {
        if (gameOver) return;

        float zRotation = rB2D.velocity.y * rotationSpeed;
        transform.eulerAngles =new Vector3(0, 0, Mathf.Clamp(zRotation, -90, 30));
    }

    private void ClampHeight()
    {
        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, -10, 4.8f));
    }

    private void GameOver()
    {
        cinemachineScreenShake.MoveCamera(3, 3, 0.5f);
        loseSFX.Play();
        gameOver = true;
        transform.eulerAngles = new Vector3(0, 0, -90);
        rB2D.velocity = Vector2.zero;
        uIAnimations.Hit();
        playerAnimator.SetTrigger("Dead");

        foreach (GameObject pipe in GameObject.FindGameObjectsWithTag("Pipe"))
        {
            pipe.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameOver) return;
        GameOver();
    }
}
