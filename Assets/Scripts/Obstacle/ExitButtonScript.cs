using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class ExitButtonScript : MonoBehaviour
{
    [SerializeField]
    private Sprite pressedSprite;
    [SerializeField]
    private bool pressed = false;

    public UnityEvent unlock;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (pressed)
            return;

        if (collision.gameObject.CompareTag("Player"))
        {
            unlock.Invoke();
            pressed = true;
            spriteRenderer.sprite = pressedSprite;
        }
    }
}
