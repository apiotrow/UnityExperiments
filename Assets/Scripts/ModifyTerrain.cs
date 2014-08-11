using UnityEngine;
using System.Collections;

public class ModifyTerrain : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ReplaceBlockCenter(float range, byte block){
		//Replaces the block directly in front of the player
	}
	
	public void AddBlockCenter(float range, byte block){
		//Adds the block specified directly in front of the player
	}
	
	public void ReplaceBlockCursor(byte block){
		//Replaces the block specified where the mouse cursor is pointing
	}
	
	public void AddBlockCursor( byte block){
		//Adds the block specified where the mouse cursor is pointing
	}
	
	public void ReplaceBlockAt(RaycastHit hit, byte block) {
		//removes a block at these impact coordinates, you can raycast against the terrain and call this with the hit.point
	}
	
	public void AddBlockAt(RaycastHit hit, byte block) {
		//adds the specified block at these impact coordinates, you can raycast against the terrain and call this with the hit.point
	}
	
	public void SetBlockAt(Vector3 position, byte block) {
		//sets the specified block at these coordinates
	}
	
	public void SetBlockAt(int x, int y, int z, byte block) {
		//adds the specified block at these coordinates
	}
	
	public void UpdateChunkAt(int x, int y, int z){
		//Updates the chunk containing this block
	}
}
