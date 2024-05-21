using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{


    public bool gameOver;

    public float floatForce;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;

    [Header("Noname Added")]
    [SerializeField] private float m_maxHeightRatio = 0.8f;
    [SerializeField] private float m_minHeightRatio = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();

        playerRb = GetComponent<Rigidbody>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {

        ClampPosWithForuce();

        // While space is pressed and player is low enough, float up
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver)
        {
            //playerRb.AddForce(Vector3.up * floatForce);
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
        }
    }


    void ClampPosWithForuce()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        bool isOutside = viewportPos.y > m_maxHeightRatio || viewportPos.y < m_minHeightRatio;
        if (viewportPos.y > m_maxHeightRatio)
        {
            viewportPos.y = m_maxHeightRatio;
            Vector3 clampWorldPos = Camera.main.ViewportToWorldPoint(viewportPos);
            transform.position = new Vector3(transform.position.x, clampWorldPos.y, transform.position.z);
            playerRb.AddForce(Vector3.down * 0.5f, ForceMode.Impulse);
        }
        else if (viewportPos.y < m_minHeightRatio)
        {
            viewportPos.y = m_minHeightRatio;
            Vector3 clampWorldPos = Camera.main.ViewportToWorldPoint(viewportPos);
            transform.position = new Vector3(transform.position.x, clampWorldPos.y, transform.position.z);
            playerRb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }

        if (isOutside)
        {
            return;
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        }

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }

    }

}
