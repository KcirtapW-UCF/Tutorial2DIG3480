using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    private int scoreValue = 0;

    public int lives = 3;

    public Text livestext;

    public Text death;

    public Transform level2;

    public Transform playerposition;

    public int wincon = 0;

    public AudioSource audio;

    public AudioClip winmusic;

    public AudioClip losemusic;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        livestext.text = "Lives: " + lives.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue >= 4 && wincon == 0)
            {
                scoreValue = 0;
                lives = 3;
                score.text = "Score: " + scoreValue.ToString();
                livestext.text = "Lives: " + lives.ToString();
                playerposition.position = level2.position;
                wincon = 1;
            }

            else if (scoreValue >= 4 && wincon == 1)
            {
                audio.Stop();
                death.text = "You Win" + "\n Game Created by Patrick Weatherford!";
                audio.clip = winmusic;
                audio.Play();
            }
        }

        if (collision.collider.tag == "Enemy")
        {
            lives -= 1;
            livestext.text = "Lives: " + lives.ToString();
            Destroy(collision.collider.gameObject);
            if (lives <= 0)
            {
                audio.Stop();
                death.text = "You Lose";
                audio.clip = losemusic;
                audio.Play();
                Object.Destroy(this.gameObject);
            }
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