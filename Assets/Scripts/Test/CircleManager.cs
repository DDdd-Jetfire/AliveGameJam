using UnityEngine;

public class CircleManager : MonoBehaviour
    {
    public GameObject circlePrefab;
    public Texture2D backgroundTexture;

    void Start()
        {
        Vector2 startPos = Vector2.zero;
        GameObject circle = Instantiate(circlePrefab, startPos, Quaternion.identity);
        circle.GetComponent<CircleBehavior>().Initialize(backgroundTexture);
        }
    }
