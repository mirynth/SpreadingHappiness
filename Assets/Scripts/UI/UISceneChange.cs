using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneChange : MonoBehaviour
{
    [SerializeField] private string target_scene;

    public void ChangeScene()
    {
        SceneManager.LoadScene(target_scene);
    }
}
