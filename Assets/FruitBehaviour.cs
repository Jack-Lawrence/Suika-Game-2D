using UnityEngine;

public class FruitBehaviour : MonoBehaviour
{
    [Header("Fruit Settings")]
    public FruitData fruitData;
    public FruitData.FruitProperties currentFruitProperties;

    private Vector3 baseScale;

    private void Start()
    {
        baseScale = transform.localScale;
        UpdateScale();
        UpdateColor();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FruitBehaviour otherFruit = collision.gameObject.GetComponent<FruitBehaviour>();

        if (otherFruit != null
            && otherFruit.currentFruitProperties.name == currentFruitProperties.name
            && !collision.gameObject.CompareTag("SpawnZone"))
        {
            CombineFruits(otherFruit);
        }
    }

    private void CombineFruits(FruitBehaviour otherFruit)
    {
        int currentIndex = System.Array.IndexOf(fruitData.fruitProperties, currentFruitProperties);
        if (currentIndex < fruitData.fruitProperties.Length - 1)
        {
            currentFruitProperties = fruitData.fruitProperties[currentIndex + 1];
            UpdateScale();
            UpdateColor();
        }

        Destroy(otherFruit.gameObject);
    }

    public void UpdateScale()
    {
        transform.localScale = baseScale * currentFruitProperties.scaleMultiplier;
    }

    public void UpdateColor()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = currentFruitProperties.color;
    }
}
