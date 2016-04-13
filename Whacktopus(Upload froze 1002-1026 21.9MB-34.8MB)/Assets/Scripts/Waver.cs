using UnityEngine;
using System.Collections;

public class Waver : MonoBehaviour
{
	float scale = 0.1f;
	float speed = 4.0f;
	float noiseStrength = 0.9f;
	float noiseWalk = 5f;
	float rando = 0.2f;

	private Vector3[] baseHeight;
	
	void Update () {
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		
		if (baseHeight == null)
			baseHeight = mesh.vertices;
		
		Vector3[] vertices = new Vector3[baseHeight.Length];
		for (int i=0;i<vertices.Length;i++)
		{
			Vector3 vertex = baseHeight[i];
			vertex.y += Mathf.Sin(Time.time * speed+ baseHeight[i].x + baseHeight[i].y + baseHeight[i].z) * scale;
			vertex.y += Mathf.PerlinNoise(baseHeight[i].x + noiseWalk, baseHeight[i].y + Mathf.Sin(Time.time * 0.1f)    ) * noiseStrength;
			vertices[i] = vertex;
		}

		//rando = Random.Range (-1, 1);
		GetComponent<MeshRenderer> ().material.mainTextureOffset = new Vector2 ((Time.time * rando), (Time.time * rando));

		mesh.vertices = vertices;
		mesh.RecalculateNormals();
	}
}