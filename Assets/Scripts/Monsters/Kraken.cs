using UnityEngine;
using System.Collections;

public class Kraken : Monster {

	public AudioClip AttackSound = null;
	public float xSpeed = 0.5f;
	public float attackYSpeed  = 2.0f;
	public float attackHeight = -2.0f;
	public float swimHeight = -3.0f;
	public float upWaitTime = 2.0f;
	public float attackRotation = 10.0f;
	public float attackWaitRotation = -10.0f;
	public float retreatRotation = -30.0f;
	public float restRotation = 0.0f;
	public float rotationSpeed = 50.0f;
	public float damagePerSec = 200;

	private Transform _tentacle1 = null;
	private Transform _tentacle2 = null;
	private Transform _tentacle3 = null;

	private float _height;
	private float _tentacRotation = 0.0f;

	// Use this for initialization
	void Start () {
		_tentacle1 = transform.FindChild("TentacleRight");
		_tentacle2 = transform.FindChild("TentacleMiddle");
		_tentacle3 = transform.FindChild("TentacleLeft");

		_mode = MonsterMode.Approach;
		_height = swimHeight;
		SetTentacleHeight(_height);
	}
	
	// Update is called once per frame
	void Update () {
		if (_mode == MonsterMode.Approach)
			Approach();
		if (_mode == MonsterMode.Attack)
			Attack();
		if (_mode == MonsterMode.Retreat)
			Retreat();

		if ( IsUnderShip() && _mode == MonsterMode.Approach) {
			_mode = MonsterMode.Attack;
			AudioSource.PlayClipAtPoint(AttackSound, Ship.instance.transform.position);
		}

		if ( _height == attackHeight ) {
			if ( _mode == MonsterMode.Attack ){
				WaitUntil( Time.time + upWaitTime );
			}
			if (_mode == MonsterMode.Wait && FinishedWaiting()) {
				_mode = MonsterMode.Retreat;
			}
			MoveTentacRotationTowards( attackWaitRotation );
		}

		if ( _height == swimHeight && _mode == MonsterMode.Retreat ) {
			_mode = MonsterMode.Approach;
		}



	}

	void Attack() {
		_height = Mathf.MoveTowards( _height, attackHeight, attackYSpeed * Time.deltaTime );
		SetTentacleHeight( _height );
		MoveTentacRotationTowards( attackRotation );
	}

	void Retreat() {
		_height = Mathf.MoveTowards( _height, swimHeight, attackYSpeed * Time.deltaTime );
		SetTentacleHeight( _height );
		MoveTentacRotationTowards( retreatRotation );
	}

	void Approach() {
		Vector3 shipPos = Ship.instance.transform.position;
		float xKraken = Mathf.MoveTowards( transform.position.x, shipPos.x, xSpeed * Time.deltaTime );
		transform.position = new Vector3( xKraken, transform.position.y, transform.position.z); 
		MoveTentacRotationTowards( restRotation );
	}

	bool IsUnderShip(){
		Vector3 shipPos = Ship.instance.transform.position;
		return ( Mathf.Abs( shipPos.x - transform.position.x ) < 0.1f );
	}


	void SetTentacleHeight( float height ){
		_tentacle1.GetComponent<FloatingObject>().yAdjustment = height;
		_tentacle2.GetComponent<FloatingObject>().yAdjustment = height;
		_tentacle3.GetComponent<FloatingObject>().yAdjustment = height + 0.2f;
	}

	void MoveTentacRotationTowards( float rotation ) {
		_tentacRotation = Mathf.MoveTowardsAngle(_tentacRotation, rotation, rotationSpeed * Time.deltaTime);
		SetTentacleRotation( _tentacRotation );
	}


	void SetTentacleRotation( float rotation ){
		_tentacle1.localEulerAngles = new Vector3( 0.0f, 0.0f, -rotation);
		_tentacle2.localEulerAngles = new Vector3( 0.0f, 0.0f, -rotation);
		_tentacle3.localEulerAngles = new Vector3( 0.0f, 0.0f, rotation);
	}

	void OnTriggerStay2D(Collider2D coll){
		Ship ship = coll.GetComponent<Ship>();
		if(ship != null && _mode == MonsterMode.Retreat){
				ship.CurrentHp -= damagePerSec * Time.deltaTime;

		}
		
	}
}
