using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            GameManager.Instance.AddScore(1);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Target1"))
        {
            GameManager.Instance.AddScore(-1);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Target2"))
        {
            GameManager.Instance.AddScore(2);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}