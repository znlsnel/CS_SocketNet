using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utills 
{
	public static Vector3 MakeVector3(vector3 vector)
	{
		return new Vector3(vector.x, vector.y, vector.z); 
	}

	public static vector3 MakeVector3(Vector3 vector)
	{
		return new vector3() { x = vector.x, y = vector.y, z = vector.z}; 
	}
}
