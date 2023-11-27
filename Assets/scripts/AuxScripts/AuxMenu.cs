using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuxMenu : MonoBehaviour
{
    [SerializeField] GameObject warning;

    public void setWarningActive() {
        warning.SetActive(false);
    }
}
