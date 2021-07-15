using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class UIMenu : MonoBehaviour
{
   
    public void OpenExplorer()
    {
        string path = EditorUtility.OpenFilePanel("Select image", "", "");

        GameEvent.instance.UploadImage(path);
    }


}
