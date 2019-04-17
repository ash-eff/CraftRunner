using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollector : MonoBehaviour
{

    GameController gc;

    private void Start()
    {
        gc = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Star")
        {
            gc.StarCount++;
            Destroy(collision.gameObject);
        }
    }
}
