using UnityEngine;
using System.Collections;


// If attached to camera, this script makes it follow the given target GameObject.
// It only starts to move when target is further away from camera center than the
// given clearance.

public class CameraFollow2d : MonoBehaviour 
{

	public GameObject targetGameObject;

	public bool followX = true;
	public float clearanceX = 1.0f;
    public float maxX = 1000.0f;
	public float minX = -1000.0f;

	public bool followY = true;
	public float clearanceY = 1.0f;
	public float maxY = 1000.0f;
	public float minY = -1000.0f;



	void LateUpdate () 
	{
		if (targetGameObject == null) 
		{
			return;
		}

		Vector3 targetPos = targetGameObject.transform.position;

		if (followX)
		{
			float x = CalculatePosition( transform.position.x, targetPos.x, clearanceX, minX, maxX);
			transform.position = new Vector3( x, transform.position.y, transform.position.z );
		}

		if (followY)
		{
			float y = CalculatePosition( transform.position.y, targetPos.y, clearanceY, minY, maxY);
			transform.position = new Vector3( transform.position.x, y, transform.position.z );
		}


	}

	float CalculatePosition( float cameraPos, float targetPos, float clearance, float min, float max)
	{
		float deviation = cameraPos - targetPos;

		if (deviation > clearance)
			cameraPos = targetPos + clearance;
		else if (deviation < -clearance)
			cameraPos = targetPos - clearance;


		cameraPos = Mathf.Clamp( cameraPos, min, max );

		return cameraPos;
	}




}
