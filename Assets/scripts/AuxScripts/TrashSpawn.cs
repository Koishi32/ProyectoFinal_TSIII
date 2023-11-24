using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawn : MonoBehaviour
{
    // Only times are randomnized, the  Trash Prefab is assigined from editor
    
    [SerializeField]
    GameObject Trash_Prefab;
    [SerializeField]
    float TrashTimeLimit,BaseTime,TrashLife_Time;
    float Actual_Time;
    float Time_passed;
    private void Start()
    {
        Time_passed = 0;
        Actual_Time = Random.Range(1.0f, TrashTimeLimit);
    }
    private void Update()
    {
        Time_passed += Time.deltaTime;
        if (Time_passed> Actual_Time+ BaseTime) {
            Time_passed = 0;
            Actual_Time = Random.Range(1.0f, TrashTimeLimit);
            var bag = Instantiate(Trash_Prefab,this.transform.position,Random.rotation,this.transform.parent.parent);
            //bag.GetComponent<Rigidbody>().AddForce(Vector3.up*15,ForceMode.Impulse);
            Destroy(bag, TrashLife_Time);
        }
    }
}
