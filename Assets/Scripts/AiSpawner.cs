using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSpawner : MonoBehaviour 
{
	public AiAgent[] agents;
	public LayerMask layerMask;
	private List<AiAgent> list = new List<AiAgent>();
	public AiMediator aiMediator = new AiMediator();

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
				list.Add(Instantiate(agents[index], hitInfo.point, Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up)));
				
			}
		}
        if (Input.GetKeyDown(KeyCode.Space))
        {
			KillAll();
        }
	}

    public void Spawn()
    {
        throw new System.NotImplementedException();
    }

    public void KillAll()
    {
		foreach (var ai in list)
		{
			AIStateAgent stateAI = ai.GetComponent<AIStateAgent>();
			stateAI.health -= 10000;
		}
	}
}
