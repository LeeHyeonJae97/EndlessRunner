using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initializer : MonoBehaviour
{
    private void Start()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Main");                
    }
}
