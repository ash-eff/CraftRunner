using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    Player player;
    GameController gc;

    private void Start()
    {
        player = GetComponentInParent<Player>();
        gc = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Alien")
        {
            gc.playerDead = true;
        }
    }
}
