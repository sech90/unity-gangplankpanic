using UnityEngine;
using System.Collections;

public class LizardMonster : Monster {

	public float xSpeed = 1.0f;
	static public int _headCount = 0;

	Facing _facing;
	Damageable _damageable;
	DiesByFalling _deathBehaviour;

	static public int GetNumberOf(){
		return _headCount;
	}

	// Use this for initialization
	void Start () {
		_damageable = GetComponent<Damageable>();
		_facing = GetComponent<Facing>();
		_deathBehaviour = GetComponent<DiesByFalling>();

		_mode = MonsterMode.Approach;
		_headCount++;
	}
	
	// Update is called once per frame
	void Update () {

		if (transform.position.x > Ship.instance.transform.position.x)
			_facing.Set( Facing.Direction.Left );
		else
			_facing.Set( Facing.Direction.Right );


		if (_mode == MonsterMode.Approach)
			Approach();

		if (_damageable.GetHitPoints() == 0 && !_deathBehaviour.enabled) {
			_deathBehaviour.enabled = true;
			GetComponent<FloatingObject>().isFloating = false;
			GameHandler.AddScore(PointsWhenKilled);
		}

	}
	

	void Approach() {
		Vector3 shipPos = Ship.instance.transform.position;
		float x = Mathf.MoveTowards( transform.position.x, shipPos.x, xSpeed * Time.deltaTime );
		transform.position = new Vector3( x, transform.position.y, transform.position.z); 
	}


}
