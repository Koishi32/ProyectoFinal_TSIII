using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
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
        if (TimePassed > timeLimit)
        {
            Destroy(this.gameObject);
        }
    }

    public void IncreaseScore() {
        LevelManager.CoinCounter++;
        Destroy(this.gameObject);
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
