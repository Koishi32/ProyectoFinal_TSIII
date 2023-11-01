using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class raycastScript : MonoBehaviour
{
    public GameObject spawn_prefab;
    GameObject spawned_object;
    bool object_spawned;
    ARRaycastManager arrayman;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    
    // Start is called before the first frame update
    void Start()
    {
        object_spawned = false;
        arrayman = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0) {
            if (arrayman.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon)) {
                var hitpose = hits[0].pose;
                if (!object_spawned)
                {
                    spawned_object = Instantiate(spawn_prefab, hitpose.position, hitpose.rotation);
                    //spawned_object.BroadcastMessage("rebuild_AssetFlex");
                    object_spawned = true;
                }
                else {
                    Destroy(spawned_object.gameObject);
                    spawned_object = null;
                    spawned_object = Instantiate(spawn_prefab, hitpose.position, hitpose.rotation);
                    //spawned_object.BroadcastMessage("rebuild_AssetFlex");
                    //spawned_object.transform.position = hitpose.position;
                }
            }
        }

    #if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            {
                if (!object_spawned)
                {
                    spawned_object = Instantiate(spawn_prefab, Vector3.zero, Quaternion.identity);
                    object_spawned = true;
                }
                else
                {
                    Destroy(spawned_object.gameObject);
                    spawned_object = null;
                    spawned_object = Instantiate(spawn_prefab,Vector3.zero, Quaternion.identity);
                    //spawned_object.BroadcastMessage("rebuild_AssetFlex");
                    //spawned_object.transform.position = hitpose.position;
                }
            }
        }
    #endif
    }
}
