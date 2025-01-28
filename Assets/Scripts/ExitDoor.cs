using UnityEngine;
using UnityEngine.Events;

public class ExitDoor : MonoBehaviour
{
    public UnityEvent doorOpened = new();

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
            doorOpened.Invoke();
    }
}
