using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelExitScript : MonoBehaviour
{
    [SerializeField]
    private bool canLeave = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canLeave)
            if (collision.gameObject.CompareTag("Player"))
                GameManager.Instance.GameWin();
    }

    public void Unlock()
    {
        if (!canLeave)
        {
            Destroy(transform.Find("Lock").gameObject);
            canLeave = true;
        }           
    }
}
