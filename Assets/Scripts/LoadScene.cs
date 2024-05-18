using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    public int sceneNum;
    public void LoadNextScene()
    {
        // Load the MainGame scene
        SceneManager.LoadScene(sceneNum);
    }
}
