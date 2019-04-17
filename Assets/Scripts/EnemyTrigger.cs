using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Alien")
        {
            Debug.Log("You Died");
            // for now
            Destroy(transform.parent.gameObject);
        }
    }
}
