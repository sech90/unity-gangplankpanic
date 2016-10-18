using UnityEngine;
using System.Collections;


// Keeps track of hit points of an object. 
public class Damageable : MonoBehaviour {

	public int hitPoints = 1;
	public int maxHitPoints = 1;



	public void TakeDamage (int amount) {
		hitPoints = Mathf.Max( hitPoints - amount, 0 ); // hitpoints can't be < 0
	}

	public void RepairDamage (int amount) {
		hitPoints = Mathf.Min( hitPoints + amount, maxHitPoints );
	}

	public void SetHitpoints (int amount) {
		hitPoints = amount;
	}


	public int GetHitPoints(){
		return hitPoints;
	}


}
