using UnityEngine;
using System.Collections;

public class SeaHandler : MonoBehaviour {


	GameObject[] wave;
	GameObject[] seaSlice;

	int numberOfWaves = 70;

	float width = 40.0f;
	float avgDepth = 4.0f;

	public float waveHeightFactor = 1.0f;
	public float waveWidthFactor = 1.0f;
	public float waveSpeedFactor = 1.0f;



	void Start () 
	{

		wave = new GameObject[numberOfWaves];
		seaSlice = new GameObject[numberOfWaves];

		GameObject wavePrefab = Resources.Load<GameObject>("Wave");
		GameObject seaSlicePrefab = Resources.Load<GameObject>("SeaSlice");

		for (int i=0; i<numberOfWaves; i++)
		{
		    wave[i] = Instantiate (wavePrefab) as GameObject;
			wave[i].transform.parent = transform;
			SpriteRenderer sprite = wave[i].GetComponent<SpriteRenderer>();
			if (Util.IsOdd(i))
				sprite.sortingOrder = 4;
			else
				sprite.sortingOrder = 8;

			seaSlice[i] = Instantiate (seaSlicePrefab) as GameObject;
			seaSlice[i].transform.parent = transform;
			SpriteRenderer seaSprite = seaSlice[i].GetComponent<SpriteRenderer>();
			seaSprite.sortingOrder = 7;



		}

	}
	
	void Update () 
	{



		for (int i=0; i<numberOfWaves; i++)
		{
			float x = i* width/numberOfWaves;
			float y = GetSurfaceY( x );
			if (Util.IsOdd(i))
				y=y+0.15f;
			float z = 0.0f;
 
		    wave[i].transform.position = new Vector3(x,y,z);
			seaSlice[i].transform.position = new Vector3(x,y - 0.5f,z);

		}


	}
	
	
	public float GetSurfaceY( float x)
	{
		float time = Time.time * waveSpeedFactor;
		x *= waveWidthFactor;

		return avgDepth + (0.38f * waveHeightFactor)* Mathf.Sin(time*0.1f + x*0.7f) - (0.45f  * waveHeightFactor) * Mathf.Cos(time*0.8f + x*0.4f);

	}

}
