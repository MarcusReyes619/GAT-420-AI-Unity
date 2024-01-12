using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSpawner : MonoBehaviour
{
	public AiAgent[] agents;
	public LayerMask layerMask;

	int index = 0;

	void Update()
	{
		//press tab to switch agent spawn
		if (Input.GetKeyDown(KeyCode.Tab)) index = (++index % agents.Length);

		//click spawn or hold let ctrl and mouse butn to spwan mutiple 
		if (Input.GetMouseButtonDown(0) || (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftControl)))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layerMask))
			{
			  Instantiate(agents[index], hitInfo.point, Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up));

			}
		}
	}
}
