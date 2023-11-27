using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public void change_Scene(int index_Sce){
        SceneManager.LoadScene(index_Sce);
    }

    [SerializeField]Animator AnimationWarning;
    public void CheckLevelUnlocked(int index_LevelScene) {
        if (index_LevelScene == 1) {
            if (PlayerPrefs.GetInt("LevelOneUnlocked", 1)==1) {
                SceneManager.LoadScene(1);
            }
            else{
                AnimationWarning.gameObject.SetActive(true);
                AnimationWarning.Play("Show",-1);
            }
        } else if (index_LevelScene == 2) {
            if(PlayerPrefs.GetInt("LevelTwoUnlocked", 0) == 1) {
                SceneManager.LoadScene(2);
            }
            else
            {
                AnimationWarning.gameObject.SetActive(true);
                AnimationWarning.Play("Show",-1);
            }
        }
    
    }
    public void ExitApp()
    {
        Application.Quit();
    }

}
