using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeSpawn : MonoBehaviour
{
    [Header("Références AR")]
    [SerializeField] private Camera arCamera;

    [Header("Prefabs")]
    [SerializeField] private GameObject redCubePrefab;
    [SerializeField] private GameObject blueCubePrefab;

    [Header("Paramètres")]
    [SerializeField] private int maxCubes = 10;
    [SerializeField] private float minDistance = 5f;
    [SerializeField] private float maxDistance = 15f;
    [SerializeField] private float minHeight = 0.5f;
    [SerializeField] private float maxHeight = 1f;

    private List<GameObject> activeCubes = new List<GameObject>();

    void Start()
    {
        if (arCamera == null)
            arCamera = Camera.main;
    }

    void Update()
    {
        // spawn continu (sans delay)
        if (activeCubes.Count < maxCubes)
        {
            SpawnRandomCube();
        }

        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartGame();
        }
    }

    private void SpawnRandomCube()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float distance = Random.Range(minDistance, maxDistance);

        float x = Mathf.Sin(angle) * distance;
        float z = Mathf.Cos(angle) * distance;
        float y = Random.Range(minHeight, maxHeight);

        Vector3 spawnPosition = arCamera.transform.position + new Vector3(x, y, z);

        GameObject prefabToSpawn =
            (Random.value > 0.5f) ? redCubePrefab : blueCubePrefab;

        GameObject cube = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

        activeCubes.Add(cube);
    }

    public void RemoveCube(GameObject cube)
    {
        activeCubes.Remove(cube);
    }

    private void RestartGame()
    {
        Debug.Log("Restart Game");

        foreach (GameObject cube in activeCubes)
        {
            if (cube != null)
                Destroy(cube);
        }

        activeCubes.Clear();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetScore();
        }
    }
}