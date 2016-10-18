using UnityEngine;
using System.Collections;

public class FloatingObject : MonoBehaviour {
	
	public bool isFloating = true;
	//protected bool IsFloating {get{return isFloating;} set{isFloating = value;}}

	public float yAdjustment = 0.0f;
	public float objectWidth = 0.1f;

	public bool isRolling = false;
	public float rollFactor = 1.0f;
	public float rollAdjustment = 0.0f;

	public bool isSinking;
	public float sinkingAcceleration = 0.25f;
	public float sinkingRollAcceleration = 5.0f;

	private float sinkingSpeed = 0.0f;
	private float sinkingRollSpeed = 0.0f;

	private SeaHandler _sea;  
	

	void Start(){
		_sea = GameObject.FindObjectOfType<SeaHandler>();
	}



	void Update () {

		float seaHeighMiddle = _sea.GetSurfaceY(transform.position.x);
		float seaHeightRight = _sea.GetSurfaceY(transform.position.x + objectWidth/2.0f);
		float seaHeightLeft  = _sea.GetSurfaceY(transform.position.x - objectWidth/2.0f);


		if(isFloating){
			float seaHeightAvg = (seaHeighMiddle + seaHeightRight + seaHeightLeft) / 3.0f;
			transform.position = new Vector3(transform.position.x, seaHeightAvg + yAdjustment, transform.position.z );
		}

		if (isRolling){
			float roll = Mathf.Atan( (seaHeightRight-seaHeightLeft)/objectWidth ) * Mathf.Rad2Deg;
			roll = roll * rollFactor + rollAdjustment; 
			transform.rotation =  Quaternion.AngleAxis(roll, Vector3.forward);
		}

		
		if(isSinking){
			sinkingSpeed += sinkingAcceleration * Time.deltaTime;
			sinkingRollSpeed += sinkingRollAcceleration * Time.deltaTime;

			transform.position += new Vector3(0.0f, -sinkingSpeed, 0.0f );

			float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, 180.0f, sinkingRollSpeed);
			transform.eulerAngles = new Vector3(0, 0, angle);
		}

		
		
	}
}
