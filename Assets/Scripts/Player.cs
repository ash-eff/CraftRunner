using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float upForce;
    [SerializeField]
    private float secondUpForce;
    [SerializeField]
    private float dropForce;
    [SerializeField]
    private float slashTime;
    [SerializeField]
    private GameObject sword;

    private float camHeight;

    private bool canSlash = true;
    private bool secondJump = false;

    private GroundCheck groundCheck;
    private Rigidbody2D rb2d;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        groundCheck = GetComponentInChildren<GroundCheck>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && canSlash)
        {
            StartCoroutine(Slash());
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (groundCheck.isGrounded)
            {
                secondJump = true;
                Jump(upForce);
            }
            else if (secondJump)
            {
                secondJump = false;
                Jump(secondUpForce);
            }
        }
        
        // can only drop on second jump
        if(Input.GetKeyDown(KeyCode.S) && !secondJump && !groundCheck.isGrounded)
        {
            StartCoroutine(Drop(dropForce));
        }
    }

    private void Jump(float _force)
    {
        rb2d.velocity = Vector2.up * _force;
    }

    IEnumerator Drop(float _force)
    {
        bool dropping = true;
        while (dropping)
        {
            rb2d.velocity = Vector2.down * _force;

            yield return null;

            if (transform.position.y < -3.8f)
            {
                break;
            }
        }

        StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(.1f, .1f));
    }

    IEnumerator Slash()
    {
        canSlash = false;
        sword.SetActive(true);
        yield return new WaitForSeconds(slashTime);
        sword.SetActive(false);
        canSlash = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Star")
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Alien")
        {
            Debug.Log("You Died");
            // for now
            Destroy(gameObject);
        }
    }
}
