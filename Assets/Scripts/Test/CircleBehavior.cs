using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CircleBehavior : MonoBehaviour
    {
    private float radius = 1.0f;
    private Texture2D sourceTexture;
    private Camera mainCamera;

    public void Initialize(Texture2D texture)
        {
        this.sourceTexture = texture;
        this.mainCamera = Camera.main;

        // 获取精确的半径（基于 SpriteRenderer.bounds 大小）
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float diameter = sr.bounds.size.x;  // 假设是正圆，用 X 尺寸
        this.radius = diameter / 2f;

        SetColorFromTexture();
        }
    //private void SetColorFromTexture()
    //    {
    //    Vector2 worldPos = transform.position;
    //    Vector2 texCoord = WorldToTextureCoord(worldPos);

    //    // 圆的直径（世界单位）转换为像素单位
    //    float worldDiameter = radius * 2f;

    //    // 先估算贴图尺寸与世界单位的比例（假设背景是 10 单位宽）
    //    float worldWidth = 10f;
    //    float pixelsPerUnitX = sourceTexture.width / worldWidth;
    //    float pixelDiameter = worldDiameter * pixelsPerUnitX;

    //    int halfSize = Mathf.RoundToInt(pixelDiameter / 2f);
    //    int centerX = Mathf.RoundToInt(texCoord.x);
    //    int centerY = Mathf.RoundToInt(texCoord.y);

    //    // 避免越界
    //    int minX = Mathf.Max(0, centerX - halfSize);
    //    int maxX = Mathf.Min(sourceTexture.width - 1, centerX + halfSize);
    //    int minY = Mathf.Max(0, centerY - halfSize);
    //    int maxY = Mathf.Min(sourceTexture.height - 1, centerY + halfSize);

    //    Color total = Color.black;
    //    int count = 0;

    //    for (int y = minY; y <= maxY; y++)
    //        {
    //        for (int x = minX; x <= maxX; x++)
    //            {
    //            total += sourceTexture.GetPixel(x, y);
    //            count++;
    //            }
    //        }

    //    Color avgColor = (count > 0) ? total / count : Color.white;
    //    GetComponent<SpriteRenderer>().color = avgColor;
    //    }
    private void SetColorFromTexture()
        {
        Debug.Log("颜色设置成功!");
        Vector2 worldPos = transform.position;
        Vector2 texCoord = WorldToTextureCoord(worldPos) * 1.3f;
        Color sampledColor = sourceTexture.GetPixel((int)texCoord.x, (int)texCoord.y);
        GetComponent<SpriteRenderer>().color = sampledColor;
        }

    private Vector2 WorldToTextureCoord(Vector2 worldPos)
        {
        Debug.Log("世界坐标对应成功");
        Vector3 localPos = worldPos - (Vector2)mainCamera.transform.position;
        float texWidth = sourceTexture.width;
        float texHeight = sourceTexture.height;

        float worldWidth = 10f;  // 你自己设置的背景图像的宽度（单位是 Unity 单位）
        float worldHeight = 10f; // 同上

        float x = (localPos.x + worldWidth / 2f) / worldWidth * texWidth;
        float y = (localPos.y + worldHeight / 2f) / worldHeight * texHeight;

        return new Vector2(x, y);
        }

    //void OnMouseEnter()
    //    {
    //    Debug.Log("指针入!");
    //    if (radius <= 0.001f)//太小了则不响应
    //        {

    //        return;
    //        }

    //    float childRadius = radius / 2f;
    //    Vector2 pos = transform.position;

    //    Vector2[] offsets = new Vector2[]
    //    {
    //    new Vector2(-childRadius,  childRadius),
    //    new Vector2(-childRadius, -childRadius),
    //    new Vector2( childRadius,  childRadius),
    //    new Vector2( childRadius, -childRadius)
    //    };

    //    foreach (var offset in offsets)
    //        {
    //        GameObject child = Instantiate(gameObject, pos + offset, Quaternion.identity);
    //        child.transform.localScale = transform.localScale * 0.5f;
    //        child.GetComponent<CircleBehavior>().Initialize(sourceTexture);
    //        }

    //    Destroy(gameObject);
    //    }

    void OnMouseEnter()
        {
        if (radius <= 0.005f)
            {

            return;
            }

        float childRadius = radius / 2f;
        Vector2 pos = transform.position;

        Vector2[] offsets = new Vector2[]
        {
        new Vector2(-childRadius,  childRadius),
        new Vector2(-childRadius, -childRadius),
        new Vector2( childRadius,  childRadius),
        new Vector2( childRadius, -childRadius)
        };

        foreach (var offset in offsets)
            {
            GameObject child = Instantiate(gameObject, pos + offset, Quaternion.identity);
            child.transform.localScale = transform.localScale * 0.5f;
            child.GetComponent<CircleBehavior>().Initialize(sourceTexture);
            }

        Destroy(gameObject);
        }
    }