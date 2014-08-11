using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PolygonGenerator : MonoBehaviour {
	// This first list contains every vertex of the mesh that we are going to render
	public List<Vector3> newVertices = new List<Vector3>();
	
	// The triangles tell Unity how to build each section of the mesh joining
	// the vertices
	public List<int> newTriangles = new List<int>();
	
	// The UV list is unimportant right now but it tells Unity how the texture is
	// aligned on each polygon
	public List<Vector2> newUV = new List<Vector2>();
	
	
	// A mesh is made up of the vertices, triangles and UVs we are going to define,
	// after we make them up we'll save them as this mesh
	private Mesh mesh;

	private float tUnit = 0.25f;
	private Vector2 tStone = new Vector2 (0, 0);
	private Vector2 tGrass = new Vector2 (0, 1);

	private int squareCount;

	public byte[,] blocks;

	public List<Vector3> colVertices = new List<Vector3>();
	public List<int> colTriangles = new List<int>();
	private int colCount;
	
	private MeshCollider col;
	public bool update=false;


	void Start () {
		mesh = GetComponent<MeshFilter> ().mesh;
		col = GetComponent<MeshCollider> ();
		
		GenTerrain();
		BuildMesh();
		UpdateMesh();
	}
	
	void Update(){
		if(update){
			BuildMesh();
			UpdateMesh();
			update=false;
		}
	}

	void UpdateMesh () {
		mesh.Clear ();
		mesh.vertices = newVertices.ToArray();
		mesh.triangles = newTriangles.ToArray();
		mesh.uv = newUV.ToArray();
		mesh.Optimize ();
		mesh.RecalculateNormals ();

		squareCount=0;
		newVertices.Clear();
		newTriangles.Clear();
		newUV.Clear();

		Mesh newMesh = new Mesh();
		newMesh.vertices = colVertices.ToArray();
		newMesh.triangles = colTriangles.ToArray();
		col.sharedMesh= newMesh;
		
		colVertices.Clear();
		colTriangles.Clear();
		colCount=0;
	}

	void GenSquare(int x, int y, Vector2 texture){
		newVertices.Add( new Vector3 (x  , y  , 0 ));
		newVertices.Add( new Vector3 (x + 1 , y  , 0 ));
		newVertices.Add( new Vector3 (x + 1 , y-1 , 0 ));
		newVertices.Add( new Vector3 (x  , y-1 , 0 ));
		
		newTriangles.Add(squareCount*4);
		newTriangles.Add((squareCount*4)+1);
		newTriangles.Add((squareCount*4)+3);
		newTriangles.Add((squareCount*4)+1);
		newTriangles.Add((squareCount*4)+2);
		newTriangles.Add((squareCount*4)+3);
		
		newUV.Add(new Vector2 (tUnit * texture.x, tUnit * texture.y + tUnit));
		newUV.Add(new Vector2 (tUnit*texture.x+tUnit, tUnit*texture.y+tUnit));
		newUV.Add(new Vector2 (tUnit * texture.x + tUnit, tUnit * texture.y));
		newUV.Add(new Vector2 (tUnit * texture.x, tUnit * texture.y));
		
		squareCount++;
	}

	void GenTerrain(){
		blocks=new byte[96,128];
		
		for(int px=0;px<blocks.GetLength(0);px++){
			int stone= Noise(px,0, 80,15,1);
			stone+= Noise(px,0, 50,30,1);
			stone+= Noise(px,0, 10,10,1);
			stone+=75;
			
			print(stone);
			
			int dirt = Noise(px,0, 100f,35,1);
			dirt+= Noise(px,100, 50,30,1);
			dirt+=75;
			
			
			for(int py=0;py<blocks.GetLength(1);py++){
				if(py<stone){
					blocks[px, py]=1;
					
					//The next three lines make dirt spots in random places
					if(Noise(px,py,12,16,1)>10){
						blocks[px,py]=2;
						
					}
					
					//The next three lines remove dirt and rock to make caves in certain places
					if(Noise(px,py*2,16,14,1)>10){ //Caves
						blocks[px,py]=0;
						
					}
					
				} else if(py<dirt) {
					blocks[px,py]=2;
				}
				
				
			}
		}
	}

	int Noise (int x, int y, float scale, float mag, float exp){
		return (int) (Mathf.Pow ((Mathf.PerlinNoise(x/scale,y/scale)*mag),(exp) )); 	
	}

	void BuildMesh(){
		for (int px=0; px<blocks.GetLength(0); px++) {
			for (int py=0; py<blocks.GetLength(1); py++) {
				
				//If the block is not air
				if (blocks [px, py] != 0) {
					
					// GenCollider here, this will apply it
					// to every block other than air
					GenCollider (px, py);
					
					if (blocks [px, py] == 1) {
						GenSquare (px, py, tStone);
					} else if (blocks [px, py] == 2) {
						GenSquare (px, py, tGrass);
					}
				}//End air block check
			}
		}
	}

	void GenCollider(int x, int y){
		
		//Top
		if(Block(x,y+1)==0){
			colVertices.Add( new Vector3 (x  , y  , 1));
			colVertices.Add( new Vector3 (x + 1 , y  , 1));
			colVertices.Add( new Vector3 (x + 1 , y  , 0 ));
			colVertices.Add( new Vector3 (x  , y  , 0 ));
			
			ColliderTriangles();
			
			colCount++;
		}
		
		//bot
		if(Block(x,y-1)==0){
			colVertices.Add( new Vector3 (x  , y -1 , 0));
			colVertices.Add( new Vector3 (x + 1 , y -1 , 0));
			colVertices.Add( new Vector3 (x + 1 , y -1 , 1 ));
			colVertices.Add( new Vector3 (x  , y -1 , 1 ));
			
			ColliderTriangles();
			colCount++;
		}
		
		//left
		if(Block(x-1,y)==0){
			colVertices.Add( new Vector3 (x  , y -1 , 1));
			colVertices.Add( new Vector3 (x  , y  , 1));
			colVertices.Add( new Vector3 (x  , y  , 0 ));
			colVertices.Add( new Vector3 (x  , y -1 , 0 ));
			
			ColliderTriangles();
			
			colCount++;
		}
		
		//right
		if(Block(x+1,y)==0){
			colVertices.Add( new Vector3 (x +1 , y  , 1));
			colVertices.Add( new Vector3 (x +1 , y -1 , 1));
			colVertices.Add( new Vector3 (x +1 , y -1 , 0 ));
			colVertices.Add( new Vector3 (x +1 , y  , 0 ));
			
			ColliderTriangles();
			
			colCount++;
		}
		
	}

	void ColliderTriangles(){
		colTriangles.Add(colCount*4);
		colTriangles.Add((colCount*4)+1);
		colTriangles.Add((colCount*4)+3);
		colTriangles.Add((colCount*4)+1);
		colTriangles.Add((colCount*4)+2);
		colTriangles.Add((colCount*4)+3);
	}

	//This is a simple function that checks if the block you're checking 
	//is within the array's boundaries, if not it returns 1 (Solid rock) 
	//otherwise it returns the block's value. This means that we 
	//can use this places where we're not sure that the block we're checking 
	//is within the level size. We'll use this in the collider function.
	byte Block (int x, int y){
		
		if(x==-1 || x==blocks.GetLength(0) ||   y==-1 || y==blocks.GetLength(1)){
			return (byte)1;
		}
		
		return blocks[x,y];
	}
}
