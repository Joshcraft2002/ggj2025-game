using UnityEngine;

public class DeathFloorScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.PlayerDeath.Invoke();
        }
    }
}
