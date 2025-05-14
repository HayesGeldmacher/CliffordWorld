using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class ShaderTest : MonoBehaviour
{

    public Shader postShader;
    Material postEffectMaterial;


    //src camera source, dest destination buffer we are writing to
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (postEffectMaterial == null)
        {
            postEffectMaterial = new Material(postShader);
        }

        RenderTexture renderTexture = RenderTexture.GetTemporary(src.width, src.height, 0, src.format); // depth buffer = 0, rendertexture format based on effect we want

        Graphics.Blit(src, renderTexture, postEffectMaterial, 0); //pass number
        Graphics.Blit(renderTexture, dest);

        RenderTexture.ReleaseTemporary(renderTexture);

    }
   
}
