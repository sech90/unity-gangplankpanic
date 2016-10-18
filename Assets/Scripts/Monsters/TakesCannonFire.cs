using UnityEngine;
using System.Collections;



public class TakesCannonFire : MonoBehaviour {

	Damageable _damageable;

	void Awake() {
		_damageable = GetComponent<Damageable>();
	}

	void OnCollisionEnter2D(Collision2D coll) {

		// we should test that the collider is actually a cannonball
		Destroy( coll.gameObject );
		if (_damageable != null)
			_damageable.TakeDamage(1);
	}

}      
