using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Networking;
public class ImageUpload : MonoBehaviour
{
    string path;
    public RawImage image;

    Prediction prediction;
    private void Start()
    {
        prediction = Prediction.instance;
    }

    public void OpenExplorer()
    {
        path = EditorUtility.OpenFilePanel("Select image", "","");
        GetImage();
    }

    void GetImage()
    {
        if (path != null)
        {
            UpdateImage();
        }
    }

    void UpdateImage()
    {
        WWW www = new WWW("file:///" + path);
        image.texture = www.texture;
        GetPixelValues((Texture2D)www.texture);
    }

    void GetPixelValues(Texture2D tex)
    {
        //Reshape texture

        TextureScale.Bilinear(tex, 224 ,224);

       // image.texture = tex;

        Color32[] pixels = tex.GetPixels32();

        Debug.Assert(
                     condition: pixels.Length == tex.width * tex.height,
                     message: $"length should be {tex.width * tex.height * 3} but was {pixels.Length}");

       // print(tex.width); 
       // print(tex.height); 

        prediction.SetInput(pixels,tex.width,tex.height);

    }
}
