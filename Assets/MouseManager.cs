using UnityEngine;

public class MouseManager : MonoBehaviour
{
    [Header("Fruit Settings")]
    public GameObject fruitPrefab;
    public FruitData fruitData;

    [Header("Spawn Zone Settings")]
    public string spawnZoneTag = "SpawnZone";

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsMouseInsideSpawnZone())
            {
                SpawnFruitAtMouse();
            }
        }
    }

    void SpawnFruitAtMouse()
    {
        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPosition.z = 0;

        GameObject spawnedFruit = Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);
        FruitBehaviour fruitBehaviour = spawnedFruit.GetComponent<FruitBehaviour>();

        int randomFruitIndex = Random.Range(0, 4);
        fruitBehaviour.fruitData = fruitData;
        fruitBehaviour.currentFruitProperties = fruitData.fruitProperties[randomFruitIndex];
    }

    bool IsMouseInsideSpawnZone()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            return hit.collider.CompareTag(spawnZoneTag);
        }

        return false;
    }
}
