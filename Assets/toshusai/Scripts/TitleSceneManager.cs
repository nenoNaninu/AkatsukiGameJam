using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour {

    [SerializeField]
    private string gameSceneName = "";
   

    public void ChangeGameScene(){
        SceneManager.LoadSceneAsync(gameSceneName);
    }
}
