using UnityEngine;
using System.Collections;



public delegate void ButtonEvent(ButtonDir dir, Animator _animr);

public class Sailorman : MonoBehaviour {
	
	public int PlayerNumber = 1;
	public float sidewaysSpeed = 0.6f;
	public float climbSpeed = 0.3f;
	public float fallSpeed = 0.5f;

	public ButtonEvent OnButtonPressed;
	public ButtonEvent OnButtonHold;
	public ButtonEvent OnButtonRelease;
	
	private float handsHeight = 0.02f; // y-position of hands
	private float sailorWidth = 0.09f; 
	private bool lastMoveOnLadder = false;

	private UserInput _input;	
	private Animator _anim;

	void Awake(){
		_anim = gameObject.GetComponent<Animator>();
		_input = gameObject.AddComponent<UserInput>();

		_input.PlayerNumber = PlayerNumber;
		_anim.SetBool("Idle",true);
	}


	// Update is called once per frame
	void Update () 
	{
		if(_input.PlayerHold == ButtonDir.NONE){
			_anim.SetBool("Idle",true);
			_anim.SetBool("Move",false);
			_anim.SetBool("Climb",false);
			return;
		}

		if(OnButtonPressed != null && _input.PlayerPressed != ButtonDir.NONE)
			OnButtonPressed(_input.PlayerPressed,_anim);
		else if(OnButtonRelease != null && _input.PlayerRelease != ButtonDir.NONE)
			OnButtonRelease(_input.PlayerRelease, _anim);
		else if(OnButtonHold != null && _input.PlayerHold != ButtonDir.NONE)
			OnButtonHold(_input.PlayerHold, _anim);

		
		if ( _input.PlayerLeft && !LeftObstructed() && !FeetInsideFloorOnStairs() )
		{
			Move( new Vector3(-sidewaysSpeed * Time.deltaTime, 0.0f) );
			lastMoveOnLadder = false;
			_anim.SetBool("Move",true);
		}
		
		if ( _input.PlayerRight && !RightObstructed() && !FeetInsideFloorOnStairs())
		{
			Move( new Vector3(sidewaysSpeed * Time.deltaTime, 0.0f) );
			lastMoveOnLadder = false;
			_anim.SetBool("Move",true);
		}
		
		if ( _input.PlayerUp && ( HandsOnLadder() || FeetOnLadder()) )
		{
			Move( new Vector3(0.0f, climbSpeed * Time.deltaTime ) );
			lastMoveOnLadder = true;
			_anim.SetBool("Climb",true);
		}
		
		if ( _input.PlayerDown && FeetOnLadder() )
		{
			Move( new Vector3(0.0f, -climbSpeed * Time.deltaTime ) );
			lastMoveOnLadder = true;
			_anim.SetBool("Climb",true);
		}
		
		if (!StandingOnFloor () && !FeetOnLadder () && !HandsOnLadder ()) 
		{
			Fall();
			lastMoveOnLadder = false;
		}
	}
	
	void Move( Vector3 localPosChange )
	{
		transform.localPosition = transform.localPosition + localPosChange;
	}
	
	void Fall()
	{
		int iterations = 5;
		
		for (int i=0; i< iterations; i++ )
		{
			Vector3 posChange = new Vector3 (0.0f, -fallSpeed/iterations* Time.deltaTime);
			transform.localPosition = transform.localPosition + posChange;
			
			if (StandingOnFloor ())
				break;
		}
	}
	
	bool StandingOnFloor()
	{
		return (IsColliderAtLocal(transform.localPosition, "Floor"));
	}
	
	bool FeetInsideFloorOnStairs()
	{
		return FeetOnLadder() && StandingOnFloor() && lastMoveOnLadder;
	}
	
	
	bool FeetOnLadder()
	{
		return (IsColliderAtLocal(transform.localPosition, "Ladder"));
	}
	
	bool HandsOnLadder()
	{
		Vector3 checkPosition = transform.localPosition + new Vector3(0.0f, handsHeight, 0.0f);;
		return (IsColliderAtLocal(checkPosition, "Ladder"));
	}
	
	bool LeftObstructed()
	{
		Vector3 checkPosition = transform.localPosition+ new Vector3(-sailorWidth/2.0f, 0.0f, 0.0f);
		return (IsColliderAtLocal(checkPosition, "Wall"));
	}
	
	bool RightObstructed()
	{
		Vector3 checkPosition = transform.localPosition+ new Vector3(sailorWidth/2.0f, 0.0f, 0.0f);
		return (IsColliderAtLocal(checkPosition, "Wall"));
	}
	
	// Is there a collider with given name at given local position
	bool IsColliderAtLocal(Vector3 localPosition, string name)
	{
		Vector3 position = transform.parent.transform.TransformPoint(localPosition);
		return IsColliderAt(position, name);
	}
	
	
	// Is there a collider with given name at given global position
	bool IsColliderAt(Vector3 position, string name)
	{
		Collider2D[] hits = Physics2D.OverlapPointAll (position);
		
		for (int i=0; i < hits.Length; i++)
			if (hits[i].name == name )
				return true;
		
		return false;
	}
	
}
