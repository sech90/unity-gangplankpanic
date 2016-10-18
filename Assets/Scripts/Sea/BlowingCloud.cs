using UnityEngine;
using System.Collections;

public class BlowingCloud : MonoBehaviour {

	public float steerAmountPerSec = 1.0f;
	private Transform _body;
	private Transform _tail;
	private Transform _wind;

	public AudioClip blowSound;

	// Use this for initialization
	void Start () {
		_body = transform.FindChild("Body");
		_tail = transform.FindChild("Body/Tail");
		_wind = transform.FindChild("Body/Wind");
	}
	
	// Update is called once per frame
	void Update () {

		float yBody = Mathf.Sin( Time.time ) * 0.25f; 
		_body.transform.localPosition = new Vector3(0.0f, yBody, 0.0f);

		float rotationBody = Mathf.Sin( Time.time * 0.8f ) * 5.0f;
		_body.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationBody);

		float rotationTail = Mathf.Sin( Time.time * 1.8f ) * 3.0f;
		_tail.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationTail);
	}

	void OnTriggerStay2D(Collider2D coll){
		Ship ship = coll.GetComponent<Ship>();
		if(ship != null){
			ship.Steer( steerAmountPerSec * Time.deltaTime );
		}

	}

	void OnTriggerEnter2D(Collider2D coll){
		Ship ship = coll.GetComponent<Ship>();
		if(ship != null){
			_wind.gameObject.renderer.enabled = true;
			AudioSource.PlayClipAtPoint(blowSound, transform.position);
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		Ship ship = coll.GetComponent<Ship>();
		if(ship != null){
			_wind.gameObject.renderer.enabled = false;
		}
	}

}
