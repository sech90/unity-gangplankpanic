using UnityEngine;
using System.Collections;

public class SeaBottom : MonoBehaviour {

	EditableMesh mesh1;
	EditableMesh mesh2;
	EditableMesh mesh3;
	EditableMesh mesh4;
	EditableMesh mesh5;
	EditableMesh mesh6;
	EditableMesh mesh7;
	EditableMesh mesh8;

	Color bottomColor = new Color(0.9f, 0.5f, 0.0F, 1.0F);
	Color waterColor = Color.blue;

	void Start () 
	{
		mesh1 = new EditableMesh();
		mesh1.Create(15.0f, 3.0f, 80, 1);
		mesh1.SetColor( GetBottomColor(1.0f) );
		mesh1.SetPosition( new Vector3(0.0f, 0.0f, -1.0f));

		mesh2 = new EditableMesh();
		mesh2.Create(15.0f, 3.0f, 80, 1);
		mesh2.SetColor(GetBottomColor(0.7f) );
		mesh2.SetPosition( new Vector3(0.0f, 0.0f, -0.9f));

		mesh3 = new EditableMesh();
		mesh3.Create(15.0f, 3.0f, 80, 1);
		mesh3.SetColor(GetBottomColor(0.6f) );
		mesh3.SetPosition( new Vector3(0.0f, 0.0f, -0.8f));

		mesh4 = new EditableMesh();
		mesh4.Create(15.0f, 3.0f, 80, 1);
		mesh4.SetColor(GetBottomColor(0.5f) );
		mesh4.SetPosition( new Vector3(0.0f, 0.0f, -0.7f));

		mesh5 = new EditableMesh();
		mesh5.Create(15.0f, 3.0f, 80, 1);
		mesh5.SetColor( GetBottomColor(0.4f) );
		mesh5.SetPosition( new Vector3(0.0f, 0.0f, -0.6f));
		
		mesh6 = new EditableMesh();
		mesh6.Create(15.0f, 3.0f, 80, 1);
		mesh6.SetColor(GetBottomColor(0.3f) );
		mesh6.SetPosition( new Vector3(0.0f, 0.0f, -0.5f));
		
		mesh7 = new EditableMesh();
		mesh7.Create(15.0f, 3.0f, 80, 1);
		mesh7.SetColor(GetBottomColor(0.2f) );
		mesh7.SetPosition( new Vector3(0.0f, 0.0f, -0.4f));
		
		mesh8 = new EditableMesh();
		mesh8.Create(15.0f, 3.0f, 80, 1);
		mesh8.SetColor(GetBottomColor(0.1f) );
		mesh8.SetPosition( new Vector3(0.0f, 0.0f, -0.3f));

	}


	Color GetBottomColor( float factor )
	{
		float red = waterColor.r + (bottomColor.r - waterColor.r)*factor;
		float green = waterColor.g + (bottomColor.g - waterColor.g)*factor;
		float blue = waterColor.b + (bottomColor.b - waterColor.b)*factor;

		return new Color(red, green, blue, 1.0F);
	}

	void Update () 
	{
		float time = Time.time;

		UpdateMesh( mesh1, time );		
		UpdateMesh( mesh2, time + 2.0f );		
		UpdateMesh( mesh3, time + 4.0f);		
		UpdateMesh( mesh4, time + 6.0f );		
		UpdateMesh( mesh5, time + 8.0f );		
		UpdateMesh( mesh6, time + 10.0f );		
		UpdateMesh( mesh7, time + 12.0f );		
		UpdateMesh( mesh8, time + 14.0f );		

	}

	void UpdateMesh( EditableMesh mesh, float time)
	{
		int i = 0;
		

		while (i < 81) 
		{
			Vector3 position = mesh.GetVertex(i,1); 
			Vector3 newPosition = new Vector3( position.x, 
			                                  GetSurfaceY( position.x, time ), 
			                                  position.z );
			
			mesh.SetVertex(i,1, newPosition);
			i++;
		}
		
		mesh.UpdateMesh();

	}
	
	
	public float GetSurfaceY( float x, float time)
	{
		//		return 4.0f + 0.8f * Mathf.Sin(time*0.4f + x*0.7f) 
		//			- 0.1f * Mathf.Cos(time*2 + x*2.0f);
		//return 2.0f + 0.25f * Mathf.Sin(x*0.7f) + 0.4f * Mathf.Sin(time/3.0f) + 0.3f * Mathf.Sin(time/5.0f);

		return 2.0f + 0.25f * Mathf.Sin(x*0.7f) + 
			          0.4f * Mathf.Sin(time/3.0f) + 0.3f * Mathf.Sin(x+time/5.0f);



		/*return 4.0f + 0.3f * Mathf.Sin(time*0.4f + x*0.7f) 
			- 0.1f * Mathf.Cos(time*0.13f + x*2.0f); */
	}

}
