using UnityEngine;
using System.Collections;

public class DiesByFalling : MonoBehaviour {

	public float droppingDeadAccelleration = 0.5f;
	public bool divesHeadFirst = false;
	public AudioClip _dyingSound = null;


	private float _droppingDeadSpeed = 0.0f;
	private Damageable _damageable = null;
//	public bool _isActive = false;

	/*
	public void Activate() {
		_isActive = true;
	}

	public void Deactivate() {
		_isActive = false;
	}

	public bool IsActive(){
		return _isActive;
	}


	void Start () {
		_damageable = GetComponent<Damageable>();
	}

*/
	public void OnEnable() { 
		AudioSource.PlayClipAtPoint( _dyingSound,transform.position );
	}

	public void Update() {
//			if ( !_isActive )
//				return;

		//if (_damageable.GetHitPoints() == 0) {
			_droppingDeadSpeed += droppingDeadAccelleration * Time.deltaTime;
			transform.position += new Vector3(0.0f, -_droppingDeadSpeed, 0.0f );

			if (divesHeadFirst) {
				float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, 90.0f, 5.0f);
				transform.eulerAngles = new Vector3(0, 0, angle);
			}
		
			// ajastimella
			if (transform.position.y < -5.0f){
				Destroy(this.gameObject);
			}
		//}

	}
}
