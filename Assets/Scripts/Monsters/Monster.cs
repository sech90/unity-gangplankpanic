using UnityEngine;
using System.Collections;



public enum MonsterMode{
	Approach,
	Wait,
	Attack,
	Retreat,
	Dying
};

public class Monster : MonoBehaviour {

	public int PointsWhenKilled;
	public MonsterMode Mode{get{return _mode;}}

	protected MonsterMode _mode;
	
	protected float waitingUntil;



	protected bool FinishedWaiting(){
		return (Time.time >= waitingUntil);
	}



	protected void WaitUntil( float time ){
		waitingUntil = time;
		_mode = MonsterMode.Wait;
	}

	public void StopAttacking(){
		if (GetComponent<Kraken>() == null)
			WaitUntil(Time.time + 9999);
	}

	
	
}
