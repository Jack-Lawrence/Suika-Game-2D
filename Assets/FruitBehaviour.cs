using UnityEngine;

public class FruitBehaviour : MonoBehaviour
{
    [Header("Fruit Settings")]
    public FruitData fruitData;
    public FruitData.FruitProperties currentFruitProperties;

    public bool isPreview = false;

    private Vector3 baseScale;
    private Rigidbody2D rb;
    private float originalGravity;
    private MouseManager mouseManager;
    private GameManager gameManager;

    private void Start()
    {
        baseScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;

        mouseManager = FindObjectOfType<MouseManager>();
        gameManager = FindObjectOfType<GameManager>(); 

        UpdateScale();
        UpdateColor();

        if (isPreview)
        {
            rb.gravityScale = 0;
        }
    }

    private void Update()
    {
        if (isPreview)
        {
            FollowMouse();

            if (Input.GetMouseButtonDown(0))
            {
                SetToNormalMode();
            }
        }
    }

    private void FollowMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        transform.position = mousePosition;
    }

    private void SetToNormalMode()
    {
        isPreview = false;
        rb.gravityScale = originalGravity;
        StartCoroutine(AllowNewSpawn());
    }

    private System.Collections.IEnumerator AllowNewSpawn()
    {
        yield return new WaitForSeconds(1);
        if (mouseManager != null)
        {
            mouseManager.isFruitSpawned = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isPreview) return;

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

            int scoreIncrement = CalculateScoreIncrement(otherFruit.currentFruitProperties);
            gameManager.AddScore(scoreIncrement);
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
        if (spriteRenderer != null)
        {
            Color color = currentFruitProperties.color;
            spriteRenderer.color = color;
        }
    }

    private int CalculateScoreIncrement(FruitData.FruitProperties otherProperties)
    {
        float sizeMultiplier = currentFruitProperties.sizeMultiplier * otherProperties.sizeMultiplier;
        return Mathf.FloorToInt(sizeMultiplier * 5); 
    }
}
