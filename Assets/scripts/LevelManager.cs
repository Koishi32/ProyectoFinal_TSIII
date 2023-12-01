using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public static int CoinCounter;
    public static int Species_Number;
    [SerializeField] int SpeciesMaxWin;
    [SerializeField] TextMeshProUGUI textScore;
    [SerializeField] TextMeshProUGUI speciesCount;

    public List<GameObject> AnimalsToSpawn = new List<GameObject>();
    [SerializeField] public static List<GameObject> SpawnedAnimals = new List<GameObject>();
    static LevelManager MyReference;

    [SerializeField] int SpeciesPrice;
    [SerializeField] GameObject VictoryPanel;
    [SerializeField] GameObject FailPanel;
    // Start is called before the first frame update
    void Start()
    {
        VictoryPanel.SetActive(false);
        MyReference = this.GetComponent<LevelManager>();
        FailPanel.SetActive(false);
        CoinCounter = 0;
        Species_Number = 0;
        //SpeciesMaxWin = 3;
#if UNITY_EDITOR
        CoinCounter = 100;
        Fill_lists();
#endif
        updateSpecies();
        updateScoreScreen();

    }

    public static void Fill_lists() {
        foreach (GameObject a in GameObject.FindGameObjectsWithTag("Species"))
        {
            MyReference.AnimalsToSpawn.Add(a);
            a.SetActive(false);
        }
        MyReference.AddAnimal();
    }

    public static void ReduceSpecies(GameObject Eliminated)
    {
        Species_Number -= 1;
        if (Species_Number <= 0)
        {
            Species_Number = 0;
            Debug.Log("StopGame");
            MyReference.updateSpecies();
            MyReference.FailPanel.SetActive(true);
            MyReference.StopGameTime();
        }
        else
        {
            SpawnedAnimals.Remove(Eliminated);
            MyReference.AnimalsToSpawn.Add(Eliminated);
            MyReference.updateSpecies();
        }
    }
    public void IncreasesSpecies(GameObject added)
    {
        Species_Number += 1;
        if (Species_Number >= SpeciesMaxWin)
        {
            Species_Number = SpeciesMaxWin;
            updateSpecies();
            VictoryPanel.SetActive(true);
            StopGameTime();
        }
        else
        {
            LevelManager.SpawnedAnimals.Add(added);
            added.GetComponentInChildren<SpeciesScript>().Reset_Life();
            updateSpecies();
        }
    }

    public void AddNextAnimal() {
        if (!(LevelManager.CoinCounter > SpeciesPrice))
        {
            return;
        }
        else {
            LevelManager.CoinCounter -= 1;
            AddAnimal();
        }
        
    }

    void AddAnimal(){
        if (AnimalsToSpawn.Count<=0) {
            Debug.Log("No Animals To add");
            return;
        }
        var animal = AnimalsToSpawn[0];
        IncreasesSpecies(animal);
        MyReference.AnimalsToSpawn.Remove(animal);
    }
    // Update is called once per frame

    public void updateScoreScreen()
    {
        textScore.text = "Monedas: " + CoinCounter;
    }
    public void updateSpecies()
    {
        speciesCount.text = "Especies: " + Species_Number+"/"+SpeciesMaxWin;
    }
    void StopGameTime()
    {
        Invoke("FinalStop", 1.5f);
    }
    void FinalStop()
    {
        Time.timeScale = 0;
    }

    public void SetSaveFileForLevelUnlock(int level) {
        if (level == 2) {
            PlayerPrefs.SetInt("LevelTwoUnlocked", 1);
            Debug.Log(PlayerPrefs.GetInt("LevelTwoUnlocked", -1));
            PlayerPrefs.Save();
        } else if (level ==3) {
            PlayerPrefs.SetInt("LevelThirdUnlocked", 1);
            PlayerPrefs.Save();
        }
    
    }
    public void change_Scene(int index_Sce)
    {
        SceneManager.LoadScene(index_Sce);
    }
}
