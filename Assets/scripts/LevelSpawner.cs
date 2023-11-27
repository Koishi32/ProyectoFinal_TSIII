using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
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
    Transform Food_heightAncor;
    LevelManager levManager;

    private bool levelSet;
    int food_Limit;

    public static int current_Food;

    // Start is called before the first frame update
    void Start()
    {
        //TouchedObject = null;
        levelSet = false;
        food_Limit = 20;
        levManager = this.GetComponent<LevelManager>();
        //arCam = GameObject.Find("AR Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
            return;

        RaycastHit hit;
        Ray ray = arCam.ScreenPointToRay(Input.GetTouch(0).position);

        if (m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits)) {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (!levelSet) { // Put level prefab if the level is not set yet
                        levelSet = true;
                        SpawnPrefab(m_Hits[0].pose.position, spawnablePrefab);
                        Food_heightAncor = GameObject.FindGameObjectWithTag("HeightAnchor").transform;
                        var planes = aRPlaneManager.trackables;
                        foreach (var plane in planes)
                        {
                            plane.gameObject.SetActive(false);
                        }
                        aRPlaneManager.enabled = false;
                        LevelManager.Fill_lists();
                    }
                    else if (levelSet) // Put food instead of prefab of level
                    {
                        if (current_Food < food_Limit) {
                            current_Food++;
                            Instantiate(Food, m_Hits[0].pose.position + Food_heightAncor.position.y * Vector3.up, Random.rotation, this.transform.parent);
                        }
                    }
                }
            }

        }

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
            }
        }
        }

        private void SpawnPrefab(Vector3 spawnPosition, GameObject spawnablePrefab) {
            Instantiate(spawnablePrefab, spawnPosition, Quaternion.identity);
        }


    } 
