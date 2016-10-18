using UnityEngine;
using System.Collections;

public class Lantern : MonoBehaviour {


	void Update () 
	{
		float shipRotation = transform.parent.localEulerAngles.z;
		transform.localEulerAngles = new Vector3(0.0f, 0.0f, -shipRotation);
	}
}
