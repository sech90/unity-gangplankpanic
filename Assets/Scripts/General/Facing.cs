using UnityEngine;
using System.Collections;

// This class controls the facing of a 2D object 
// by mirroring it left and right
public class Facing : MonoBehaviour {

	public enum Direction{
		Left,
		Right
	};

	public Direction startsFacing = Direction.Left;

	private Direction _facing;

	void Start () {
		_facing = startsFacing;
	}
	

	public void Set( Direction facing ) {
		_facing = facing;
		Vector3 scale = transform.localScale;
		
		if (_facing == Direction.Left) {
			scale.x = Mathf.Abs( scale.x );
		}else {
			scale.x = -Mathf.Abs( scale.x );
		}
		transform.localScale = scale;
	}

	public void SetRandom() {
		if (Random.value > 0.5f)
			Set(Direction.Left);
		else
			Set(Direction.Right);

	}

	public Direction Get(){
		return _facing;
	}

	public bool IsRight() {
		return (_facing == Direction.Right);
	}

	public bool IsLeft() {
		return (_facing == Direction.Left);
	}

	public void Flip() {
		if (_facing == Direction.Right)
			_facing = Direction.Left;
		else
			_facing = Direction.Right;
	}


}
