using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadScript : MonoBehaviour
{
    [SerializeField]
    float timeLimit;
    float TimePassed;
    private void Start()
    {
        TimePassed = 0.0f;
    }
    private void Update()
    {
        TimePassed += Time.deltaTime;
        if (TimePassed> timeLimit) {
            LevelSpawner.current_Food--;
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.gameObject.name);
        if (collision.collider.tag == "Species")
        {
            LevelSpawner.current_Food--;
            collision.collider.gameObject.GetComponentInChildren<SpeciesScript>().EatSomething();
            Destroy(this.gameObject, 0.1f);

        } else if (collision.collider.tag == "DownLimit")
        {
            LevelSpawner.current_Food--;
            Destroy(this.gameObject, 2);
        }
    }

}
