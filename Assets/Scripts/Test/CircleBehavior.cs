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

        // ��ȡ��ȷ�İ뾶������ SpriteRenderer.bounds ��С��
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float diameter = sr.bounds.size.x;  // ��������Բ���� X �ߴ�
        this.radius = diameter / 2f;

        SetColorFromTexture();
        }

    private void SetColorFromTexture()
        {
        Debug.Log("��ɫ���óɹ�!");
        Vector2 worldPos = transform.position;
        Vector2 texCoord = WorldToTextureCoord(worldPos);
        Color sampledColor = sourceTexture.GetPixel((int)texCoord.x, (int)texCoord.y);
        GetComponent<SpriteRenderer>().color = sampledColor;
        }

    private Vector2 WorldToTextureCoord(Vector2 worldPos)
        {
        Debug.Log("���������Ӧ�ɹ�");
        Vector3 localPos = worldPos - (Vector2)mainCamera.transform.position;
        float texWidth = sourceTexture.width;
        float texHeight = sourceTexture.height;

        float worldWidth = 10f;  // ���Լ����õı���ͼ��Ŀ�ȣ���λ�� Unity ��λ��
        float worldHeight = 10f; // ͬ��

        float x = (localPos.x + worldWidth / 2f) / worldWidth * texWidth;
        float y = (localPos.y + worldHeight / 2f) / worldHeight * texHeight;

        return new Vector2(x, y);
        }

    //void OnMouseEnter()
    //    {
    //    Debug.Log("ָ����!");
    //    if (radius <= 0.001f)//̫С������Ӧ
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