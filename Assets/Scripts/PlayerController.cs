using System;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    SoundListener Sl => SoundListener.Instance;

    [DllImport("__Internal")]
    private static extern void Status(bool isGameOver, int score);

    public TextMeshProUGUI uiScore = null;
    public Sprite[] userSprite = null;

    public bool isGameOver;
    public int score = 0;

    public Rigidbody2D rb;
    
    public float jumpForce = 100f;
    private Vector2 jumpVector;
    public bool isJumpable = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        jumpVector = new Vector2(0, jumpForce);
        isGameOver = false;
        //isJumpable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Sl.loudness > 1.0f && isJumpable == true)
        {
            rb.linearVelocityY = 0f;
            rb.AddForce(jumpVector, ForceMode2D.Impulse);
            isJumpable = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isJumpable == true)
        {
            rb.linearVelocityY = 0f;
            rb.AddForce(jumpVector, ForceMode2D.Impulse);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumpable = false;
        }
    }

    private void OnCollisionStay2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumpable = true;
            //Debug.Log("¹Ù´Ú°úÀÇ Ãæµ¹ÀÌ °¨ÁöµÊ");
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Time.timeScale = 0f;
            Debug.Log("Death");
            isGameOver = true;
            //!!This is Debug Value
            //score = 100;
            Status(isGameOver, score);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ScoreZone"))
        {
            score++;
            uiScore.text = "" + score;
        }
    }

    public void SpriteSelect(int num)
    {
        SpriteRenderer sprd = gameObject.GetComponent<SpriteRenderer>();
        sprd.sprite = userSprite[num];
    }
}
