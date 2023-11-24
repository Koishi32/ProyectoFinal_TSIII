using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashScript : MonoBehaviour
{
    int life;
    // Start is called before the first frame update
    void Start()
    {
        life = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (life < 0) {
            Destroy(this.gameObject, 0.5f);
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
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }

}
