using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeciesScript : MonoBehaviour
{
    [SerializeField]
    float Time_LimitFood;
    float Time_LimitCoin;
    float TimePassed_Food;
    float TimePassed_Coin;
    [SerializeField]
    GameObject Coin_Prefab;
    [SerializeField]
    Material[] thisMaterial;
    bool starving;
    [SerializeField]
    Transform Pos_spawn;
    private void Start()
    {
        TimePassed_Food = 0.0f;
        TimePassed_Coin = 0.0f;
        Time_LimitFood = 15.0f;
        Time_LimitCoin = 5.0f;
        foreach (Material mate in thisMaterial)
        {
            mate.color = Color.white;
        }
        starving = false;
    }
    public void EatSomething() {
        starving = false;
        foreach (Material mate in thisMaterial) {
            mate.color = Color.white;
        }
        TimePassed_Food = 0;
    }

    private void FixedUpdate()
    {
        TimePassed_Food += Time.deltaTime;
        TimePassed_Coin += Time.deltaTime;

        if (TimePassed_Coin > Time_LimitCoin) {
            GenerateCoin();
            TimePassed_Coin = 0;
        }

        if (TimePassed_Food > (Time_LimitFood / 2) && !starving) {
            starving = true;
            foreach (Material mate in thisMaterial)
            {
                mate.color = Color.green;
            }
        }

        if (TimePassed_Food > Time_LimitFood) {
            //Play deaths sound?
            Destroy(this.gameObject,0.25f);
        }
    }

    public void GenerateCoin() {
       var coin = Instantiate(Coin_Prefab,Pos_spawn.position,Quaternion.identity, this.transform.parent);
    }
}
