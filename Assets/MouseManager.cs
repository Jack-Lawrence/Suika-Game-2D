using UnityEngine;

public class MouseManager : MonoBehaviour
{
    [Header("Fruit Settings")]
    public GameObject fruitPrefab;
    public FruitData fruitData;

    [Header("Spawn Zone Settings")]
    public string spawnZoneTag = "SpawnZone";

    public bool isFruitSpawned = false;
    public bool canDropFruit = false;

    void Update()
    {
        if (!isFruitSpawned)
        {
            if (IsMouseInsideSpawnZone())
            {
                SpawnFruitAtMouse();
                isFruitSpawned = true;
            }
        }

        if (IsMouseInsideSpawnZone() && !canDropFruit)
        {
            canDropFruit = true;
        }
        else
        {
            canDropFruit = false;
        }
    }

    void SpawnFruitAtMouse()
    {
        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPosition.z = 0;

        GameObject spawnedFruit = Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);
        FruitBehaviour fruitBehaviour = spawnedFruit.GetComponent<FruitBehaviour>();

        int randomFruitIndex = Random.Range(0, Mathf.Min(4, fruitData.fruitProperties.Length));
        fruitBehaviour.fruitData = fruitData;
        fruitBehaviour.currentFruitProperties = fruitData.fruitProperties[randomFruitIndex];
        fruitBehaviour.isPreview = true;
    }

    public bool IsMouseInsideSpawnZone()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            return hit.collider.CompareTag(spawnZoneTag);
        }

        return false;
    }
}
