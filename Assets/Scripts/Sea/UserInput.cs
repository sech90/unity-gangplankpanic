using UnityEngine;
using System.Collections;

public enum ButtonDir {UP, DOWN, LEFT, RIGHT, NONE};

public class UserInput : MonoBehaviour {

	private int _pNumber;
	public int PlayerNumber{set{_pNumber = value;}}

	private string[] _ButtonNameList;
	private static ButtonDir[] _keyList;

	private ButtonDir _pressed;
	private ButtonDir _release;
	private ButtonDir _hold;
	private bool l, r, u, d;

	public ButtonDir PlayerPressed{get{return _pressed;}}
	public ButtonDir PlayerHold{get{return _hold;}}
	public ButtonDir PlayerRelease{get{return _release;}}

	public bool PlayerLeft{get{return _hold == ButtonDir.LEFT;}}
	public bool PlayerRight{get{return _hold == ButtonDir.RIGHT;}}
	public bool PlayerUp{get{return _hold == ButtonDir.UP;}}
	public bool PlayerDown{get{return _hold == ButtonDir.DOWN;}}


	static UserInput(){
		_keyList = new ButtonDir[]{ButtonDir.UP, ButtonDir.DOWN, ButtonDir.LEFT, ButtonDir.RIGHT};
	}

	void Start(){
		string s = "Player"+_pNumber;
		_ButtonNameList = new string[]{s+"Up", s+"Down", s+"Left", s+"Right"};
	}

	void Update(){
		_hold = _pressed = _release = ButtonDir.NONE;
		if(Input.anyKey){
			for(int i=0;i<_ButtonNameList.Length; i++){
				
				if(Input.GetButton    (_ButtonNameList[i])) _hold 	 = _keyList[i];
				if(Input.GetButtonDown(_ButtonNameList[i])) _pressed = _keyList[i];
				if(Input.GetButtonUp  (_ButtonNameList[i])) _release = _keyList[i];

			}
		}
	}

	static public bool Exit()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			return true;

		return false;
	}


}
