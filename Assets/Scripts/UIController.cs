using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{

    public RawImage rw;
    public Text txt_predresult;
    // Start is called before the first frame update
    void Start()
    {
        //fromPixelsToTexture();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Predict()
    {
        // string result= Prediction.instance.Predict();
        string results = "";
        string []fullname= results.Split('-');
        fullname[0] = fullname[0].Substring(0, 1).ToUpper() + fullname[0].Substring(1, fullname[0].Length - 1);
        fullname[1] = fullname[1].Substring(0, 1).ToUpper() + fullname[1].Substring(1, fullname[1].Length - 1);
        txt_predresult.text = fullname[0]+" "+fullname[1];
    }

    public void FromPixelsToTexture()
    {
        Color32[] pixels = new Color32[560 * 499 ];
        int cont = 0;
        for (int i = 0; i < 560; i++)
        {
            for (int j = 0; j < 499; j++)
            {
               pixels[cont] =  new Color32((byte)Prediction.instance.preInput[i, j, 0],
                                           (byte)Prediction.instance.preInput[i, j, 1],
                                           (byte)Prediction.instance.preInput[i, j, 2],
                                            255);
                cont++;
            }
        }


        Texture2D tx = new Texture2D(560, 499);
        print(tx.width);
        print(tx.height);
        tx.SetPixels32(pixels);
        tx.Apply();
        rw.texture = tx;


    }
}
