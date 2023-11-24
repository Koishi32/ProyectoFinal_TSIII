using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    //Rigidbody myRgb;

    private void Start()
    {
        //myRgb = this.GetComponent<Rigidbody>();
        //myRgb.velocity = Vector3.up * -MyVelocity;
    }

    public void IncreaseScore() {
        LevelManager.CoinCounter++;
        Destroy(this.gameObject, 0.1f);
        //Play Soun of Coin
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "DownLimit")
        {
            Destroy(this.gameObject, 2);
        }
    }
}
