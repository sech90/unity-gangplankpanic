using UnityEngine;
using System.Collections;

public class Util 
{
	public static Color CreateColor(int red, int green, int blue)
	{
		Color color = new Color (red/256.0f, green/256.0f, blue/256.0f);
		return color;
	}

	public static Color CreateColor(int red, int green, int blue, int alpha)
	{
		Color color = new Color (red/256.0f, green/256.0f, blue/256.0f, alpha/256.0f);
		return color;
	}

	
	public static bool IsOdd(int value)
	{
		return value % 2 != 0;
	}

	public static bool RandomBool() {
		return (Random.value > 0.5f);
	}

}
