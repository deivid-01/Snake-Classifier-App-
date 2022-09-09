
// --------------------------------------------------------------------------
//------- Desarrolladores: -----------------------------
//-------- David Andrés Torres Betancour-------------------------------------------
//-------  Contacto : davida.torres@udea.edu.co --------------
//-------  Jenny Carolina Escobar Sozas    -----------------
//-------  Contacto:    carolina.escobar@udea.edu.co -------------------
//------- Proyecto Final del curso Procesamiento Digital de Imagenes----
//------- V1.5 Septiembre de 2021--------------------------------------------------
//--------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;
using TensorFlowLite;
using System.Linq;
public class Prediction : MonoBehaviour
{
    // Start is called before the first frame update
 
    public string MODEL_FILENAME = "snakes_classifier.tflite";

    int inputSize = 224; //Size in pixels of the input image
    int numBreeds = 10;
    public float[,,] input;
    public float[,,] preInput;
    float[] output;

    Interpreter interpreter;

    List<string> snake_breeds = new List<string>()
    {
        "agkistrodon-contortrix",
        "crotalus-atrox",
        "diadophis-punctatus",
        "nerodia-erythrogaster",
        "nerodia-sipedon",
        "pantherophis-alleghaniensis",
        "pantherophis-obsoletus",
        "pituophis-catenifer",
        "storeria-dekayi",
        "thamnophis-sirtalis"
    };


    #region Singlenton
    public static Prediction instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion



  
    void Start()
    {
        input = new float[inputSize, inputSize, 3];
        output = new float[numBreeds];

        var options = new InterpreterOptions()
        {
            threads = 2,
        };
        //Load machine learning model
        interpreter = new Interpreter(FileUtil.LoadFile(MODEL_FILENAME), options);
        //Build model
        interpreter.AllocateTensors();

      
    }
    private void OnDestroy()
    {
        interpreter?.Dispose();
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void SetInput(Color32[] pixels,int width,int height)
    {
        int rows = height;
        int cols = width;
        preInput = new float[rows, cols, 3];
        int cont = 0;
        for (int i = rows-1 ; i >= 0; i--)
        {
            for (int j = 0; j < cols; j++)
            {
                //BGR Format
                preInput[i, j, 0] = pixels[cont].b; // - 103.939F;
                preInput[i, j, 1] = pixels[cont].g; // - 116.779F;
                preInput[i, j, 2] = pixels[cont].r; // - 123.68F;
                cont++;
            }
        }
    }

  
    public Dictionary<string, float> Predict()
    {
        interpreter.SetInputTensorData(0, preInput);
        interpreter.Invoke();
        interpreter.GetOutputTensorData(0, output);

        
        return ShowBestOutput(output, 3);

    }
    Dictionary<string,float> ShowBestOutput(float[] output, int top)
    {

        List<float> sortoutput = new List<float>(output);

        var result = sortoutput
                     .Select((v, i) => new { v, i })
                     .OrderByDescending(x => x.v)
                     .ThenByDescending(x => x.i)
                     .Take(3)
                     .ToArray();

        Dictionary<string, float> results = new Dictionary<string, float>();

        foreach (var entry in result)
        {
            string snake_name = snake_breeds[entry.i];
            float accuracy = entry.v;
            results.Add(snake_name, accuracy);
    
        }

        return results;


    }




}
