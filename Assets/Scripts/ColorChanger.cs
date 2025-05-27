using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Color SetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }
}