using UnityEngine;

[CreateAssetMenu(fileName = "FruitData", menuName = "Fruit/Fruit Data", order = 1)]
public class FruitData : ScriptableObject
{
    [System.Serializable]
    public struct FruitProperties
    {
        public string name;             //Name of the fruit
        public float scaleMultiplier;   //Scale multiplier
        public float sizeMultiplier;
        public Color color;             //Fruit color
    }

    public FruitProperties[] fruitProperties;
}
