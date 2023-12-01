using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashScript : MonoBehaviour
{
    int life;
    [SerializeField]
    GameObject Efect;
    // Start is called before the first frame update
    void Start()
    {
        life = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (life < 0) {
            Debug.Log("BagDestroyed");
            var party = Instantiate(Efect, transform.position, Quaternion.identity);
            Destroy(party.gameObject,1);
            Destroy(this.gameObject);
        }
    }

    public void life_Reduction() {
        life--;
        //punhc sound effect
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Species")
        {
            // hurt sound
            Debug.Log(collision.collider.gameObject.name);
            var party = Instantiate(Efect, transform.position, Quaternion.identity);
            Destroy(party.gameObject, 1);
            collision.collider.GetComponentInChildren<SpeciesScript>().deactivateSelf();
            Destroy(this.gameObject);
        }
    }

}
