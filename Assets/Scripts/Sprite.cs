using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[ExecuteInEditMode]
public class Sprite : MonoBehaviour
{
	public int costume = 0;

	new private Renderer renderer = null;
	private DecalProjector decalProjector = null;
	private int spriteWidth, spriteHeight;
	
	void Start() {
		renderer = GetComponent<Renderer>();
		Vector2 scale;

		if (renderer != null) {
			scale = renderer.material.mainTextureScale;
			scale.y = -scale.y;
		} else {
			decalProjector = GetComponent<DecalProjector>();
			scale = decalProjector.uvScale;
		}
		
		if (scale.x == 0f) return;
		
		spriteWidth = (int)(1f / scale.x);
		spriteHeight = (int)(1f / scale.y);
		Debug.Log("width: " + spriteWidth + "; height: " + spriteHeight);
	}

    // Update is called once per frame
    void Update() {
    	if (renderer == null && decalProjector == null) return;
    	
		var costumeX = costume % spriteWidth;
		var costumeY = costume / spriteWidth;
		var offset = new Vector2(
	    	(float)costumeX / spriteWidth,
	    	(float)costumeY / spriteHeight);
    	
    	if (renderer != null) {
		    renderer.material.mainTextureOffset = offset;
		 } else {
		 	decalProjector.uvBias = offset;
		 }
    }
}
