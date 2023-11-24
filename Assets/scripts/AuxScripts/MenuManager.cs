using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public void change_Scene(int index_Sce){
        SceneManager.LoadScene(index_Sce);
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
