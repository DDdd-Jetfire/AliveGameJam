using UnityEngine;

public class CircleManager : MonoBehaviour
    {
    public GameObject circlePrefab;
    public GameObject Pivot;
    public Texture2D backgroundTexture;

    void Start()
        {
        Vector2 startPos = Vector2.zero;
        GameObject circle = Instantiate(circlePrefab, startPos, Quaternion.identity);
        circle.transform.position = Pivot.transform.position;
        circle.transform.localScale = Pivot.transform.localScale;
        circle.GetComponent<CircleBehavior>().Initialize(backgroundTexture);
        }
    }
