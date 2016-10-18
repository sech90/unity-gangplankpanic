using UnityEngine;
using System.Collections;

public class FlyingLion : Monster 
{
	public AudioClip AttackSound = null;

	static public int _headCount = 0;

	public float _attackSpeed = 4.0f;
	public float _approachSpeed = 0.4f;
	public float _retreatSpeed = 2.0f;
	public int   _attackPower = 2;
	float _waitTime = 6.0f;


	GameObject _shipAttackSpotRight;
	GameObject _shipAttackSpotLeft;
	Vector3 _target;			// Position towards which the monster is moving
	Damageable _damageable;
	Facing _facing;
	DiesByFalling _deathBehaviour;



	static public int GetNumberOf(){
		return _headCount;
	}


	// Use this for initialization
	void Start () {
		_damageable = GetComponent<Damageable>();
		_facing = GetComponent<Facing>();
		_deathBehaviour = GetComponent<DiesByFalling>();
		_facing.SetRandom();
		_mode = MonsterMode.Approach;
		_shipAttackSpotLeft = GameObject.Find("FlyingLionAttackSpotLeft");
		_shipAttackSpotRight = GameObject.Find("FlyingLionAttackSpotRight");

		if (_shipAttackSpotRight == null || _shipAttackSpotLeft == null)
			Debug.LogError( "ERROR: Attack spot for Flying Lion not found");

		_headCount++;
	}

	void Update () {

		if (_mode == MonsterMode.Approach)
			Approach();
		else if (_mode == MonsterMode.Attack)
			Attack();
		else if (_mode == MonsterMode.Retreat)
			Retreat();

		if (_damageable.GetHitPoints() == 0 && !_deathBehaviour.enabled) {
			_deathBehaviour.enabled = true;
			GameHandler.AddScore(PointsWhenKilled);
		}


	}

	void OnDestroy(){
		_headCount--;
	}



	void Approach(){
		float x = Mathf.MoveTowards( transform.position.x, Ship.instance.transform.position.x, 
		                            _approachSpeed * Time.deltaTime );
		transform.position = new Vector3(x, transform.position.y, transform.position.z );

		// If monster is on the attack range...
		if ( Mathf.Abs( Ship.instance.transform.position.x - x) < 4.0f )
		{
			_mode = MonsterMode.Attack;
			AudioSource.PlayClipAtPoint(AttackSound,transform.position);
		}

		if (transform.position.x > Ship.instance.transform.position.x)
			_facing.Set( Facing.Direction.Left );
		else
			_facing.Set( Facing.Direction.Right );

	}

	void Retreat(){
		MoveStraightTowards(_target, _retreatSpeed);
		if (transform.position == _target){
			Destroy(this.gameObject);
		}
	}

	void Attack()
	{
		if ( _facing.IsLeft() ) 
			_target = _shipAttackSpotRight.transform.position;
		else
			_target = _shipAttackSpotLeft.transform.position;


		MoveStraightTowards(_target, _attackSpeed);
		
		if (transform.position == _target){
			_mode = MonsterMode.Retreat;
			_target = RetreatPosition();
			if (_facing.IsRight() ) 
				Ship.instance.TakeDamage( _attackPower, 0);
			else
				Ship.instance.TakeDamage( _attackPower, 1);
			_facing.Flip();
		}
	}
	

	void MoveStraightTowards(Vector3 position, float speed){
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, position, step);
	}


	Vector3 RetreatPosition(){
		float x = Ship.instance.transform.position.x;

		if ( _facing.IsRight() )
			return new Vector3(x-15.0f, 15.0f, 0.0f);
		else
			return new Vector3(x+15.0f, 15.0f, 0.0f);
	}




}
