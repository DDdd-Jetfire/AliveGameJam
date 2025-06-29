using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class GaussianBlurEffect : MonoBehaviour
{
    public Material blurMaterial;
    [Range(0.0f, 3.0f)] public float blurSize = 1.0f;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (blurMaterial != null)
        {
            blurMaterial.SetFloat("_BlurSize", blurSize);

            // 临时缓存
            RenderTexture temp = RenderTexture.GetTemporary(source.width, source.height);
            
            // Pass 0: 横向模糊
            Graphics.Blit(source, temp, blurMaterial, 0);

            // Pass 1: 纵向模糊
            Graphics.Blit(temp, destination, blurMaterial, 1);

            RenderTexture.ReleaseTemporary(temp);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
