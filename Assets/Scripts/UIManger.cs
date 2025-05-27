using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManger : MonoBehaviour
{
    public GameObject text;

    public void AcivteUI()
    {
        text.gameObject.SetActive(true);
    }
}
