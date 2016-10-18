using UnityEngine;
using System.Collections;

public delegate void PumpEvent(Pump p);

public class Pump : InteractiveObject {
	
	public float PumpPower = 1.0f;
	public float LevelOfActivation = 0.0f; //this should stay from 0 to 1
	public float PumpIntereval = 0.5f; // minimum time between pumps
	public AudioClip PullSound;
	public AudioClip PushSound;

	public PumpEvent OnPump;
	private bool _pumpUp = false;
	private float _timeLastPump = 0.0f;
	
	override protected void OnButtonPressed(ButtonDir key, Animator _anim){
		switch(key){
		case ButtonDir.UP:
			if(!_pumpUp && Time.time >_timeLastPump + PumpIntereval ){
				_pumpUp = true;
				AudioSource.PlayClipAtPoint(PullSound,transform.position);
				transform.localEulerAngles = new Vector3(0.0f,0.0f,0.0f);
				_timeLastPump = Time.time;

			}
			break;
		case ButtonDir.DOWN:
			if(_pumpUp && Time.time >_timeLastPump + PumpIntereval){
				AudioSource.PlayClipAtPoint(PushSound,transform.position);
				OnPump(this);
				_pumpUp = false;
				transform.localEulerAngles = new Vector3(0.0f,0.0f,-30.0f);
				_timeLastPump = Time.time;
			}
			break;
		default:
			break;
		}
	}
	
	//empty bodies
	override protected void OnButtonHold(ButtonDir key, Animator _anim){}
	override protected void OnButtonRelease(ButtonDir key, Animator _anim){}
}
