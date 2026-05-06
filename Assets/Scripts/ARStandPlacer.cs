using UnityEngine;
using UnityEngine.InputSystem;

public class ARStandPlacer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera arCamera;

    [Header("Prefabs")]
    [SerializeField] private GameObject redBallPrefab;
    [SerializeField] private GameObject blueBallPrefab;

    [Header("Parametres")]
    [SerializeField] private float ballLaunchSpeed = 5f;

    private bool useRedBall = true;

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.tKey.wasPressedThisFrame)
        {
            useRedBall = !useRedBall;
            Debug.Log("Balle active : " + (useRedBall ? "ROUGE" : "BLEUE"));
        }

        if (TouchOrClick())
        {
            LaunchBall();
        }
    }

    private bool TouchOrClick()
    {
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            return true;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            return true;

        return false;
    }

    private void LaunchBall()
    {
        GameObject prefab = useRedBall ? redBallPrefab : blueBallPrefab;

        if (arCamera == null || prefab == null)
        {
            Debug.LogError("Camera ou prefab manquant !");
            return;
        }

        Vector3 spawnPos =
            arCamera.transform.position +
            arCamera.transform.forward * 0.3f;

        GameObject ball = Instantiate(prefab, spawnPos, Quaternion.identity);

        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = arCamera.transform.forward * ballLaunchSpeed;
        }

        Destroy(ball, 5f);
    }

    public void SwitchBall()
    {
        useRedBall = !useRedBall;
        Debug.Log("Balle active : " + (useRedBall ? "ROUGE" : "BLEUE"));
    }
}