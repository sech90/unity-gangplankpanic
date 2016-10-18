using UnityEngine;
using System.Collections;

public class RamsTheShip : MonoBehaviour {

	public int damageToShip = 1;
	public int damageToSelf = 10;

	void OnTriggerEnter2D(Collider2D coll){
		Ship ship = coll.GetComponent<Ship>();
		if (ship!=null) {

			// Check if collides from left or right
			if ( transform.position.x < ship.transform.position.x )
				Ship.instance.TakeDamage( damageToShip, 2);
			else
				Ship.instance.TakeDamage( damageToShip, 3);

			Damageable dam = GetComponent<Damageable>();

			if (dam != null)
				dam.TakeDamage( damageToSelf );

		}
	}
}
