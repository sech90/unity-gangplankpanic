using UnityEngine;
using System.Collections;

public class EditableMesh {
	
	private int widthVertices = 0;
	private int heightVertices = 0;
	private int widthSegments = 0;
	private int heightSegments = 0;
	
	
	Vector3[] verticesCopy;
	Vector2[] uvCopy;


	MeshFilter meshFilter;
	MeshRenderer renderer;
	

	GameObject plane;
	
	public void Create(float totalWidth, float totalHeight, int numWidthSegments, int numHeightSegments)
	{
		plane = new GameObject("Plane");
		meshFilter = (MeshFilter)plane.AddComponent(typeof(MeshFilter));
		meshFilter.mesh = CreateMeshPlane(totalWidth, totalHeight, numWidthSegments, numHeightSegments);
		verticesCopy = meshFilter.mesh.vertices;
		uvCopy = meshFilter.mesh.uv;
		
		renderer = plane.AddComponent(typeof(MeshRenderer)) as MeshRenderer;

		SetColor (Color.red);
	}

	public void SetParent(GameObject parent)
	{
		plane.transform.SetParent(parent.transform);
	}


	public void SetPosition(Vector3 position)
	{
		plane.transform.position = position;
	}

	public void SetLocalPosition(Vector3 position)
	{
		plane.transform.localPosition = position;
	}

	public void SetSortingOrder(int so)
	{
		renderer.sortingOrder = so;
	}

	public void SetColor(Color color)
	{
		renderer.material.shader = Shader.Find ("Unlit/Transparent");
		Texture2D tex = new Texture2D(1, 1);
		tex.SetPixel(0, 0, color);
		tex.Apply();
		renderer.material.mainTexture = tex;
		renderer.material.color = color;
		
	}

	public void SetTexture(Texture2D tex)
	{
		renderer.material.shader = Shader.Find ("Unlit/Transparent");
		renderer.material.mainTexture = tex;
//		renderer.material.color = color;
		
	}

	public void SetMaterial(Material material)
	{
		renderer.material = material;
	}
	
	private Mesh CreateMeshPlane(float totalWidth, float totalHeight, int numWidthSegments, int numHeightSegments)
	{
		widthSegments = numWidthSegments;
		heightSegments = numHeightSegments;
		widthVertices = widthSegments + 1;
		heightVertices = heightSegments + 1;
		
		
		Mesh m = new Mesh();
		m.name = "ScriptedMesh";
		
		
		m.vertices = CreateVertices(totalWidth, totalHeight);
		m.uv = CreateUV();
		m.triangles = CreateTriangles();
		m.RecalculateNormals();
		
		return m;
	}
	
	private Vector3[] CreateVertices(float totalWidth, float totalHeight)
	{
		float width = totalWidth / widthSegments;
		float height = totalHeight / heightSegments;
		
		
		Vector3[] vertices = new Vector3[widthVertices * heightVertices];
		
		int index=0;
		
		for (int j = 0; j < heightVertices; j++)
		{
			for (float i = 0; i < widthVertices; i++)
			{
				vertices[index] = new Vector3(i*width , j*height, 0.01f);
				index++;
			}
		}
		
		
		return vertices;
	}
	
	private Vector2[] CreateUV()
	{
		
		Vector2[] uvs = new Vector2[widthVertices * heightVertices];
		
		float uvFactorX = 1.0f/widthSegments;
		float uvFactorY = 1.0f/heightSegments;
		
		int index = 0;
		
		for (int j = 0; j < heightSegments; j++)
		{
			for (int i = 0; i < widthSegments; i++)
			{
				uvs[index] = new Vector2(i*uvFactorX, j*uvFactorY);
				index++;
			}
		}
		
		return uvs;
	}
	
	private int[] CreateTriangles()
	{
		int numTriangles = widthSegments * heightSegments * 6;
		int[] triangles = new int[numTriangles];
		
		int index=0;
		
		for (int j = 0; j < heightSegments; j++)
		{
			for (int i = 0; i < widthSegments; i++)
			{
				triangles[index]   = (j     * widthVertices) + i;
				triangles[index+1] = ((j+1) * widthVertices) + i;
				triangles[index+2] = (j     * widthVertices) + i + 1;
				
				triangles[index+3] = ((j+1) * widthVertices) + i;
				triangles[index+4] = ((j+1) * widthVertices) + i + 1;
				triangles[index+5] = (j     * widthVertices) + i + 1;
				index += 6;
			}
		}
		
		return triangles;
	}
	
	
	public Vector3 GetVertex(int i, int j)
	{
		int index = i + j * widthVertices;
		return meshFilter.mesh.vertices[ index ];
	}
	
	public void SetVertex(int i, int j, Vector3 position)
	{
		int index = i + j * widthVertices;
		verticesCopy[ index ] = position;
	}

	public void SetVertex(int i, int j, Vector2 position)
	{
		SetVertex( i, j, new Vector3(position.x, position.y, 0.0f) );
	}

	
	public void SetUv(int i, int j, Vector2 uv)
	{
		int index = i + j * widthVertices;
		uvCopy[ index ] = uv;
	}

	
	public void UpdateMesh()
	{
		meshFilter.mesh.vertices = verticesCopy; 
		meshFilter.mesh.uv = uvCopy;
		meshFilter.mesh.RecalculateBounds();
		
		verticesCopy = meshFilter.mesh.vertices;
		
	}
	
	
	
}
