using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CRTPostEffect : MonoBehaviour
{
    public Material crtMaterial;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (crtMaterial != null)
            Graphics.Blit(source, destination, crtMaterial);
        else
            Graphics.Blit(source, destination);
    }
}
