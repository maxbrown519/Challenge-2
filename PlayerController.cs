using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    private int scoreValue = 0;
    public Text winText;
    public Text lives;
    private int livesValue = 3;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    Animator anim;
    private bool facingRight = true;

    // Start is called before the first frame update
    private void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        winText.text = " ";
        lives.text = "Lives: " + livesValue.ToString();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        if(scoreValue >= 9)
        {
            winText.text = "You Win! Game by Maximillan Brown";
            winText.ToString();
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = true;
        }

       if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            livesValue = livesValue - 1;
            lives.text = "Lives: " + livesValue.ToString();
        }
       if(livesValue == 0)
        {
            Destroy(this);
            winText.text = "You lose!";
        }
        if (scoreValue == 4)
        {
            transform.position = new Vector2(56.0f, 00.0f);
            livesValue = 3;
            lives.text = "Lives: " + livesValue.ToString();
        }
        if (scoreValue <= 8)
        {
            musicSource.clip = musicClipOne;
            musicSource.Play();
            musicSource.loop = true;
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}