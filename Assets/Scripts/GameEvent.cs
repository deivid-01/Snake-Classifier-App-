using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameEvent : MonoBehaviour
{
    #region Singlenton
    public static GameEvent instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    
    }

    #endregion

    public event Action<string> OnUploadingImage;

    public void UploadImage(string path) => OnUploadingImage?.Invoke(path);
}
