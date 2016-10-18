using UnityEngine;
using System.Collections;

public class Cannon : InteractiveObject {

	public float CannonPower = 10.0f;
	public float RotateSpeed = 5.0f;
	public float Cooldown = 1.5f;
	public int ShootAngle = 90;
	public GameObject Explosion;
	public AudioClip ExplosionSound1;
	public AudioClip ExplosionSound2;


	private Transform 	_hinge;
	private Transform 	_spawner;
	private GameObject 	_cannonBall;

	private float 		_angleLimit;
	private float 		_curAngle = 0;
	private bool		_isRightSide;
	private float		_remainCooldown = 0;
	private bool 		_explosionSoundToggle = true;

	void Start () {
		_hinge 		 = transform.Find("CannonHinge");
		_spawner 	 = _hinge.Find("CannonChamber/BulletSpawner");
		_cannonBall  = Resources.Load<GameObject>("CannonBall");
		_angleLimit  = ShootAngle/2;
		//_keyList 	 = new string[]{ "up", "down", "left", "right" };
		_isRightSide = _spawner.position.x > _hinge.position.x ? true : false; 
	}

	void FixedUpdate(){
		if(_remainCooldown != 0)
			_remainCooldown = Mathf.Clamp(_remainCooldown - Time.deltaTime, 0, _remainCooldown);
	}
	
	override protected void OnButtonPressed(ButtonDir key, Animator _anim){
		switch(key){
		case ButtonDir.LEFT:
			if(!_isRightSide){
				Shoot();
				_anim.SetTrigger("ActivityLeft");
			}
			break;
		case ButtonDir.RIGHT:
			if(_isRightSide){
				Shoot();
				_anim.SetTrigger("ActivityRight");
			}
			break;
		default:
			break;
		}
	}

	override protected void OnButtonHold(ButtonDir key, Animator _anim){
		switch(key){
		case ButtonDir.UP:
			_curAngle -= RotateSpeed * Time.deltaTime;
			break;
		case ButtonDir.DOWN:
			_curAngle += RotateSpeed * Time.deltaTime;
			break;
		default:
			return;
		}
		_curAngle = Mathf.Clamp(_curAngle, -_angleLimit, +_angleLimit); // update the object rotation: 
		_hinge.localRotation = Quaternion.Euler(0,0,_curAngle); 
	}

	private void Shoot(){
		if(_remainCooldown == 0){
			GameObject bullet = Instantiate(_cannonBall,_spawner.position,_hinge.localRotation) as GameObject;
			Vector2 distance = _spawner.position - _hinge.position;
			Vector2 forceDirection = distance / distance.magnitude;
			bullet.rigidbody2D.AddForce(CannonPower * forceDirection, ForceMode2D.Impulse);
			Instantiate(Explosion,_spawner.position,_hinge.localRotation);
			if (_explosionSoundToggle)
	 			AudioSource.PlayClipAtPoint(ExplosionSound1,_spawner.position);
			else
				AudioSource.PlayClipAtPoint(ExplosionSound2,_spawner.position);
			_explosionSoundToggle = ! _explosionSoundToggle;
			Destroy(bullet,1.0f);
			_remainCooldown = Cooldown;
		}
	}

	override protected void OnButtonRelease(ButtonDir key, Animator _anim){}

}
