using UnityEngine;
using System.Collections;

public class LineSegment2d {

	private Vector2 pointA;
	private Vector2 pointB;


	public LineSegment2d( Vector2 a, Vector2 b)
	{
		pointA = a;
		pointB = b;
	}

	public bool Intersection(LineSegment2d ls, ref Vector2 intersection )
	{
		float isectX = 0.0f;
		float isectY = 0.0f;

		bool result = Get_line_intersection(  pointA.x, pointA.y, pointB.x, pointB.y,
		                                      ls.pointA.x, ls.pointA.y, ls.pointB.x, ls.pointB.y,
		                                      ref isectX, ref isectY);
		                                    	
		if (result)
			intersection = new Vector2(isectX, isectY);

		return result;

	}

	bool Get_line_intersection(float p0_x, float p0_y, float p1_x, float p1_y, 
	                           float p2_x, float p2_y, float p3_x, float p3_y, ref float i_x, ref float i_y)
	{
		float s1_x, s1_y, s2_x, s2_y;
		s1_x = p1_x - p0_x;     s1_y = p1_y - p0_y;
		s2_x = p3_x - p2_x;     s2_y = p3_y - p2_y;
		
		float s, t;
		s = (-s1_y * (p0_x - p2_x) + s1_x * (p0_y - p2_y)) / (-s2_x * s1_y + s1_x * s2_y);
		t = ( s2_x * (p0_y - p2_y) - s2_y * (p0_x - p2_x)) / (-s2_x * s1_y + s1_x * s2_y);
		
		if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
		{
			// Collision detected
			i_x = p0_x + (t * s1_x);
			i_y = p0_y + (t * s1_y);
			return true;
		}
		
		return false; // No collision
	}
}
