using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Linq;
public class UIController2 : MonoBehaviour
{
    public GameObject box_menu;
    public GameObject box_results;
    public RawImage img_snake;
    public List<Text> txt_snakenames = new List<Text>(3);
    public List<Text> txt_accuracys = new List<Text>(3);
    public List<Image> img_bars = new List<Image>(3);
    void Start()
    {
        box_menu.SetActive(true);
        box_results.SetActive(false);
    }

    public void OpenExplorer()
    {
        #if UNITY_EDITOR
            string path = EditorUtility.OpenFilePanel("Select image", "", "");
        
        GetImage(path);
        #endif
    }

    void GetImage(string path)
    {
        if (path != null)
        {
            Texture2D texture= GetTexture(path);
            img_snake.texture = texture;
            Color32[]pixels = GetPixelValues(texture);

            Prediction.instance.SetInput(pixels, texture.width, texture.height);

            Dictionary<string,float> results =  Prediction.instance.Predict();

            int cont = 0;
            foreach (KeyValuePair<string, float> result in results)
            {
                // do something with entry.Value or entry.Key
                txt_snakenames[cont].text = result.Key;

                float accuracy = result.Value;

                txt_accuracys[cont].text = accuracy.ToString();
                img_bars[cont].fillAmount = accuracy;

                cont++;

            }






            box_menu.SetActive(false);
            box_results.SetActive(true);
        }
        else
        {
            print("Fail loading image");
        }
    }

    Texture2D GetTexture(string path)
    {
        WWW www = new WWW("file:///" + path);
        //Set texture to raw image in next scene
        //image.texture = www.texture;

       return (Texture2D)www.texture;
    }

    Color32[] GetPixelValues(Texture2D tex)
    {
        //Reshape texture

        TextureScale.Bilinear(tex, 224, 224);

        // image.texture = tex;

        Color32[] pixels = tex.GetPixels32();

        Debug.Assert(
                     condition: pixels.Length == tex.width * tex.height,
                     message: $"length should be {tex.width * tex.height * 3} but was {pixels.Length}");

        return pixels;
        // print(tex.width); 
        // print(tex.height); 

        // prediction.SetInput(pixels, tex.width, tex.height);

    }
}
