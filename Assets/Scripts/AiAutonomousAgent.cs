using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAutonomousAgent : AiAgent
{
	public AiPerception seekperception = null;
	public AiPerception fleeperception = null;
	public AiPerception flockperception = null;

	private void Update()
	{

		if (seekperception != null)
		{
			var gameObj = seekperception.GetGameObjects();
			if (gameObj.Length > 0)
			{
				movement.ApplyForce(Seek(gameObj[0]));
			}

		}
		//flee
		if (fleeperception != null)
		{
			var gameObj = fleeperception.GetGameObjects();
			if (gameObj.Length > 0)
			{
				movement.ApplyForce(Flee(gameObj[0]));
			}


		}
		//flock
		if (flockperception != null)
		{
			var gameObj = flockperception.GetGameObjects();
			if (gameObj.Length > 0)
			{
				print("x");
				movement.ApplyForce(Cohesion(gameObj));
				movement.ApplyForce(Aligmenment(gameObj));
				movement.ApplyForce(Separation(gameObj, 3));
			}
		}

		print(movement.Velocity);

		//warp positon in world
		transform.position = Utilities.Wrap(transform.position, new Vector3(-10, -10, -10), new Vector3(10, 10, 10));
	}

	private Vector3 Separation(GameObject[] neighbors, float radius)
    {
		Vector3 separation =  Vector3.zero;
		foreach(var neighnor in neighbors)
        {
			Vector3 dir = (transform.position - neighnor.transform.position);
            if (dir.sqrMagnitude < radius)
            {
				separation += dir / dir.sqrMagnitude;
            }
        }
		return GetSteeringForce(separation);
		
    }

	private Vector3 Aligmenment(GameObject[] neighbors)
    {
		Vector3 vel = Vector3.zero;
		foreach(var neighbor in neighbors)
        {
			vel += neighbor.GetComponent<AiAgent>().movement.Velocity;
        }

		Vector3 avgVel = vel / neighbors.Length;

		Vector3 force = GetSteeringForce(avgVel);

		return force;
    }

	private Vector3 Cohesion(GameObject[] neighbors)
    {
		Vector3 pos = Vector3.zero;
		foreach(var neighbor in neighbors) {

			pos += neighbor.transform.position;
        }

		Vector3 center = pos / neighbors.Length;
		Vector3 dir = center - transform.position;
		Vector3 force = GetSteeringForce(dir);

		return force;
    }

	private Vector3 Seek(GameObject target)  {

		Vector3 dir = target.transform.position - transform.position;
		return GetSteeringForce(dir);

    }
	private Vector3 Flee(GameObject target)
	{

		Vector3 dir = transform.position - target.transform.position;
		return GetSteeringForce(dir);

	}
	public Vector3 GetSteeringForce(Vector3 direction)
	{
		Vector3 desired = direction.normalized * movement.maxSpeed;
		Vector3 steer = desired - movement.Velocity;
		Vector3 force = Vector3.ClampMagnitude(steer, movement.maxForce);

		return force;
	}
}
