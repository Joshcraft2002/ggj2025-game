using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Breaker"))
            Destroy(transform.parent.gameObject);
    }
}
