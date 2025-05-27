using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
   public AiSpawner aiSpawner;
   public UIManger ui;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            aiSpawner.KillAllAgents();
            ui.AcivteUI();
        }
    }


}
