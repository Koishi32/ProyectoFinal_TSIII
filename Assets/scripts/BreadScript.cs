using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadScript : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.gameObject.name);
        if (collision.collider.tag == "Species")
        {
            LevelSpawner.current_Food--;
            collision.collider.gameObject.GetComponent<SpeciesScript>().EatSomething();
            Destroy(this.gameObject, 0.1f);

        } else if (collision.collider.tag == "DownLimit")
        {
            LevelSpawner.current_Food--;
            Destroy(this.gameObject, 2);
        }
    }

}
