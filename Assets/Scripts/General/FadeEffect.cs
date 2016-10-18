using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class FadeEffect : MonoBehaviour {

	public float StartGradient = 0.0f;
	public float EndGradient = 1.0f;
	public float FadeTime = 1.5f;
	public float Delay = 0.0f;
	public bool Loop = false;
	public bool FadeOnAwake = false;

	private float _waitedDelay;
	private float _progress = 0;
	private bool _fade = false;
	private CanvasGroup _group;

	void Start () {
		_fade = FadeOnAwake;
		_group = GetComponent<CanvasGroup>();
		_group.alpha = StartGradient;
	}

	public void Fade(){
		_fade = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(_fade){
			if(Delay != 0 && _waitedDelay < Delay){
				_waitedDelay += Time.deltaTime;
				return;
			}
			if(_group.alpha == EndGradient){
				if(Loop){
					float t = StartGradient;
					StartGradient = EndGradient;
					EndGradient = t;
					_progress = 0;
					_waitedDelay = 0;
				}
			}

			else{
				_progress += Time.deltaTime;
				_group.alpha = Mathf.Lerp(StartGradient,EndGradient,_progress/FadeTime);
			}
		}
	}





}
