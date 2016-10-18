using UnityEngine;
using System.Collections;


public abstract class InteractiveObject : MonoBehaviour {

	abstract protected void OnButtonPressed(ButtonDir key, Animator anim);
	abstract protected void OnButtonHold(ButtonDir key, Animator anim);
	abstract protected void OnButtonRelease(ButtonDir key, Animator anim);

	void OnTriggerEnter2D(Collider2D coll){
		Sailorman sailor = coll.GetComponent<Sailorman>();
		if(sailor != null){
			sailor.OnButtonPressed += OnButtonPressed;
			sailor.OnButtonHold += OnButtonHold;
			sailor.OnButtonRelease += OnButtonRelease;
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		Sailorman sailor = coll.GetComponent<Sailorman>();
		if(sailor != null){
			sailor.OnButtonPressed -= OnButtonPressed;
			sailor.OnButtonHold -= OnButtonHold;
			sailor.OnButtonRelease -= OnButtonRelease;
		}
	}
}
