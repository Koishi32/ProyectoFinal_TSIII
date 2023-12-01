using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager m_RaycastManager;
    [SerializeField]
    ARPlaneManager aRPlaneManager;
    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();

    [SerializeField]
    GameObject spawnablePrefab, Food;
    [SerializeField]
    Camera arCam;
    //Transform Food_heightAncor;
    LevelManager levManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public static bool levelSet;
    int food_Limit;

    public static int current_Food;

   
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        //TouchedObject = null;
        levelSet = false;
        food_Limit = 20;
        levManager = this.GetComponent<LevelManager>();
        //arCam = GameObject.Find("AR Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0) {
            RaycastHit hit;
            Ray ray = arCam.ScreenPointToRay(Input.GetTouch(0).position);


            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "Coin")
                    {
                        hit.transform.GetComponent<CoinScript>().IncreaseScore();
                        levManager.updateScoreScreen();
                    }
                    else if (hit.collider.gameObject.tag == "Trash")
                    {
                        hit.transform.GetComponent<TrashScript>().life_Reduction();
                    }
                    else if (current_Food < food_Limit && hit.collider.transform.tag=="Species") {
                        current_Food++;
                        //Vector3 apos= hit.transform.gameObject
                        Transform pos = hit.transform.GetComponentInChildren<FoodPos>().gameObject.transform;
                        Instantiate(Food, pos.position + 0.25f* Vector3.up,Random.rotation,hit.transform);
                    }
                }
            }
        }

        
    }


} 
