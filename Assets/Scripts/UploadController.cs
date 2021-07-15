using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UploadController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameEvent.instance.OnUploadingImage += GetImage;
    }

    void GetImage(string path)
    {
        if (path != null)
        {
            GetTexture(path);
        }
        else
        {
            print("Fail loading image");
        }
    }

    void GetTexture(string path)
    {
        WWW www = new WWW("file:///" + path);
        //Set texture to raw image in next scene
        //image.texture = www.texture;
        GetPixelValues((Texture2D)www.texture);
    }

    void GetPixelValues(Texture2D tex)
    {
        //Reshape texture

        TextureScale.Bilinear(tex, 224, 224);

        // image.texture = tex;

        Color32[] pixels = tex.GetPixels32();

        Debug.Assert(
                     condition: pixels.Length == tex.width * tex.height,
                     message: $"length should be {tex.width * tex.height * 3} but was {pixels.Length}");

        // print(tex.width); 
        // print(tex.height); 

       // prediction.SetInput(pixels, tex.width, tex.height);

    }
}
