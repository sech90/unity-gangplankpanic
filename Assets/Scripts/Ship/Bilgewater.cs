using UnityEngine;
using System;
using System.Collections.Generic;

public class Bilgewater : MonoBehaviour {


	public GameObject ship;


	EditableMesh mesh;
	


	Vector2 hullLeftBottom;
	Vector2 hullRightBottom;
	Vector2 hullLeftMiddle;
	Vector2 hullLeftTop;
	Vector2 hullRightMiddle;
	Vector2 hullRightTop;

	float _waterLevel = 0.5f;

	float _waterMaxHeight = 0.3f;
	float _waterMinHeight = -1.1f;

	private GameObject bilgeWaterLine;

	//should be from 0 (no water) to 1 (completely full)
	public void SetWaterLevel(float level)
	{
		level = Mathf.Clamp(level, 0.0f, 1.0f);

		_waterLevel = level;
	}

	//should return values from 0..1
	public float GetWaterLevel()
	{
		return _waterLevel;
	}

	void Start () 
	{


		bilgeWaterLine = transform.FindChild("BilgeWaterLine").gameObject;

		/** /
		PolygonCollider2D coll = ship.collider2D as PolygonCollider2D;
		Vector2[] sortedX = coll.points;
		Vector2[] sortedY = coll.points;

		Array.Sort(sortedX,delegate(Vector2 a, Vector2 b) {
			return a.x.CompareTo(b.x);
		});

		Array.Sort(sortedY,delegate(Vector2 a, Vector2 b) {
			return a.y.CompareTo(b.y);
		});

		for(int i=0;i<coll.GetTotalPointCount(); i++){
			Debug.Log("X "+i+": "+sortedX[i]);
		}

		for(int i=0;i<coll.GetTotalPointCount(); i++){
			Debug.Log("Y "+i+": "+sortedY[i]);
		}

		hullBottom = 		sortedY[0];
		hullLeftTop =  		sortedX[0];
		hullRightTop = 		sortedX[6];
		hullLeftMiddle = 	sortedX[1].x < sortedX[2].x ? sortedX[1] : sortedX[2];
		hullRightMiddle = 	sortedX[4].x < sortedX[5].x ? sortedX[4] : sortedX[5];

		Debug.Log(hullBottom);
		Debug.Log(hullLeftTop);
		Debug.Log(hullRightTop);
		Debug.Log(hullLeftMiddle);
		Debug.Log(hullRightMiddle);
		/**/

		hullLeftBottom = 		new Vector2( -0.55f,  -1.25f);
		hullRightBottom = 		new Vector2( 0.55f,  -1.25f);
		hullLeftTop =  		new Vector2(-1.7f, 1.18f);
		hullRightTop = 		new Vector2( 1.85f, 1.18f);
		hullLeftMiddle = 	new Vector2(-1.2f,  -0.85f);
		hullRightMiddle = 	new Vector2( 1.2f,  -0.85f);
		/**/
		mesh = new EditableMesh();
		mesh.Create(10.0f, 10.0f, 1, 2);

		mesh.SetParent(this.gameObject);
		mesh.SetLocalPosition( new Vector3(0.0f, 0.0f, 0.0f) );

		mesh.SetSortingOrder(23);

		Color waterColor = Util.CreateColor(118, 112, 95, 170);
		mesh.SetColor(waterColor);

		mesh.SetVertex(0,0, hullLeftBottom);
		mesh.SetVertex(1,0, hullRightBottom);
		mesh.SetVertex(0,1, hullLeftMiddle);
		mesh.SetVertex(1,1, hullRightMiddle);
		mesh.SetVertex(0,2, hullLeftTop);
		mesh.SetVertex(1,2, hullRightTop);

		
		mesh.UpdateMesh();
	}
	
	void Update () 
	{

		LineSegment2d upperLeftSide  = new LineSegment2d( hullLeftTop,  hullLeftMiddle );
		LineSegment2d upperRightSide = new LineSegment2d( hullRightTop, hullRightMiddle );
		LineSegment2d lowerLeftSide  = new LineSegment2d( hullLeftBottom,   hullLeftMiddle );
		LineSegment2d lowerRightSide = new LineSegment2d( hullRightBottom,   hullRightMiddle );


		Vector2 isect = new Vector2(0.0f,0.0f);
	 	LineSegment2d surface = GetWaterSurfaceLine();

		if( surface.Intersection(lowerLeftSide, ref isect))
		{	
			mesh.SetVertex(0, 1, new Vector3(isect.x, isect.y, 0.0f) );
			mesh.SetVertex(0, 2, new Vector3(isect.x, isect.y, 0.0f) );
		}
		else if( surface.Intersection(upperLeftSide, ref isect))
		{	
			mesh.SetVertex(0, 2, new Vector3(isect.x, isect.y, 0.0f) );
			mesh.SetVertex(0,1, hullLeftMiddle);
		}
		else
		{
			mesh.SetVertex(0,1, hullLeftBottom);
			mesh.SetVertex(0,2, hullLeftBottom);
		}

		if( surface.Intersection(lowerRightSide, ref isect))
		{	
			mesh.SetVertex(1, 1, new Vector3(isect.x, isect.y, 0.0f) );
			mesh.SetVertex(1, 2, new Vector3(isect.x, isect.y, 0.0f) );
		}
		else if( surface.Intersection(upperRightSide, ref isect))
		{	
			mesh.SetVertex(1, 2, new Vector3(isect.x, isect.y, 0.0f) );
			mesh.SetVertex(1,1, hullRightMiddle);
		}
		else
		{
			mesh.SetVertex(1,1, hullRightBottom);
			mesh.SetVertex(1,2, hullRightBottom);
		}


		mesh.UpdateMesh();


		bilgeWaterLine.transform.localPosition = mesh.GetVertex(0,2) * 0.33333333f;
		bilgeWaterLine.transform.localEulerAngles = new Vector3( 0.0f, 0.0f, -transform.rotation.eulerAngles.z);
		Vector3 vec = mesh.GetVertex(0,2) - mesh.GetVertex(1,2);
		float xScale = vec.magnitude;
		bilgeWaterLine.transform.localScale = new Vector3( xScale, 1.5f, 1.0f);

	//	bilgeWaterLine.transform.localScale = new Vector3(0.3333333f, 0.33333333f, 1.0f);
//			new Vector3(0.0f,0.0f,0.0f); //mesh.GetVertex(0,1);
		//int i = 0;
		
/*
		while (i < numberOfSegments+1) 
		{
			Vector3 position = mesh.GetVertex(i,1); 
			float surfaceY = GetSurfaceY( position.x, time );
			
			Vector3 newPosition = new Vector3( position.x, surfaceY, position.z );
			
			mesh.SetVertex(i,1, newPosition);
			
			Vector2 uvSurface = new Vector2( i*0.1f, 1.0f ); 
			Vector2 uvBottom = new Vector2( i*0.1f, 1.0f-surfaceY*0.5f ); 
			mesh.SetUv(i, 1, uvSurface);
			mesh.SetUv(i, 0, uvBottom);
			
			i++;
		}
		
		mesh.UpdateMesh();
		*/
		
	}

	LineSegment2d GetWaterSurfaceLine()
	{
		float angle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
		float lineLenght = 100.0f; // arbitrary

		float x = Mathf.Cos(-angle) * lineLenght;
		float y = Mathf.Sin(-angle) * lineLenght;

		//Vector2 a =  new Vector2(10.0f, GetWaterHeight() );
		//Vector2 eb =     new Vector2( 4.0f, GetWaterHeight() );

		float waterHeight = _waterMinHeight + (_waterMaxHeight-_waterMinHeight)*GetWaterLevel();
		Vector2 a =  new Vector2(x, y +  waterHeight );
		Vector2 b =  new Vector2( -x, -y + waterHeight );

	

		LineSegment2d surface = new LineSegment2d( a, b );
		return surface;
	}

}
