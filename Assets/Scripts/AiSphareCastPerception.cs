using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSphareCastPerception : AiPerception
{
	[SerializeField] [Range(2, 50)] int numRayCast = 2;
	[SerializeField] [Range(0.5f, 5)] float radius = 0.5f;


	public override GameObject[] GetGameObjects()
	{
		List<GameObject> result = new List<GameObject>();

		// calculate angle from transform forward vector to direction of game object
		Vector3[] directions = Utilities.GetDirectionsInCircle(numRayCast, maxAngle);

		foreach (Vector3 direction in directions)
		{
			Ray ray = new Ray(transform.position, transform.rotation * direction);
			Debug.DrawRay(ray.origin, ray.direction, Color.red);
			if (Physics.SphereCast(ray, radius, out RaycastHit raycastHit, distance))
			{
				if (raycastHit.collider.gameObject == gameObject) continue;
				if (tagName == "" || raycastHit.collider.CompareTag(tagName))
				{
					result.Add(raycastHit.collider.gameObject);
				}
				else
				{
					Debug.DrawRay(ray.origin, ray.direction, Color.red);
				}
			}
		}
		// result = result.Distinct().ToList(); 

		return result.ToArray();
	}

    public void OnDrawGizmos()
    {
		Vector3[] directions = Utilities.GetDirectionsInCircle(numRayCast, maxAngle);
		foreach (var direction in directions)
		{
			// cast ray from transform position towards direction (use game object orientation)
			Ray ray = new Ray(transform.position, transform.rotation * direction);
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(ray.origin + ray.direction * distance, radius);
		}
	
	}

    public bool GetOpenDirection(ref Vector3 openDirection)
	{
		Vector3[] directions = Utilities.GetDirectionsInCircle(numRayCast, maxAngle);
		foreach (var direction in directions)
		{
			// cast ray from transform position towards direction (use game object orientation)
			Ray ray = new Ray(transform.position, transform.rotation * direction);
			// if there is NO raycast hit then that is an open direction
			if (!Physics.SphereCast(ray, radius, out RaycastHit raycastHit, distance, layerMask))
			{
				Debug.DrawRay(ray.origin, ray.direction * distance, Color.green);
				// set open direction
				openDirection = ray.direction;
				return true;
			}
		}

		// no open direction
		return false;
	}

	public bool CheckDirection(Vector3 direction)
	{
		// create ray in direction (use game object orientation)
		Ray ray = new Ray(transform.position, transform.rotation * direction);
		// check ray cast
		return Physics.SphereCast(ray, radius, distance, layerMask);

	}


}