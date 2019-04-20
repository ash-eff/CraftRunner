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
    [SerializeField]
    private GameObject spark;

    private float camHeight;

    private bool canSlash = true;
    private bool secondJump = false;
    private bool dropped = false;

    private AudioSource audioSource;
    private Animator anim;
    private GroundCheck groundCheck;
    private Rigidbody2D rb2d;
    private Camera cam;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
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
            Drop(dropForce);
        }

        if(rb2d.velocity.y == 0f && canSlash)
        {
            if (dropped)
            {
                dropped = false;
                StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(.1f, .1f));
            }


            anim.SetBool("Running", true);
            anim.SetBool("Jumping", false);
            anim.SetBool("Falling", false);
            spark.SetActive(true);
        }

        if (rb2d.velocity.y > 1f && canSlash)
        {

            anim.SetBool("Running", false);
            anim.SetBool("Jumping", true);
            anim.SetBool("Falling", false);
            spark.SetActive(false);
        }

        if (rb2d.velocity.y < 0f && canSlash)
        {

            anim.SetBool("Running", false);
            anim.SetBool("Jumping", false);
            anim.SetBool("Falling", true);
            spark.SetActive(false);
        }
    }

    private void Jump(float _force)
    {
        rb2d.velocity = Vector2.up * _force;
    }

    void Drop(float _force)
    {
        dropped = true;
        rb2d.velocity = Vector2.down * _force;     
    }

    IEnumerator Slash()
    {
        spark.SetActive(false);
        canSlash = false;
        float timer = slashTime;
        anim.SetBool("Slash", true);
        sword.SetActive(true);
        audioSource.Play();
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            anim.SetBool("Running", false);
            anim.SetBool("Jumping", false);
            anim.SetBool("Falling", false);
            yield return null;
        }
        sword.SetActive(false);
        anim.SetBool("Slash", false);
        canSlash = true;
    }
}
