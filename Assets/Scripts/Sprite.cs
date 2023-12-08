using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Splines;

[ExecuteInEditMode]
public class Sprite : MonoBehaviour
{
	public int costume = 0;

	new private Renderer renderer = null;
	private DecalProjector decalProjector = null;
	private int spriteWidth, spriteHeight;

	private SplineAnimate[] splineAnimations;
	private Animator animator;
	public string animationOnSplineMove;
	public string animationAfterSplineMove;
	
	public float[] splineMoveDelays;
	private int splineMoveIndex = 0;
	private bool readyForNextSplineMove = true;

	void Start() {
		renderer = GetComponent<Renderer>();
		splineAnimations = GetComponents<SplineAnimate>();
		animator = GetComponent<Animator>();

		Vector2 scale;
		if (renderer != null) {
			scale = renderer.sharedMaterial.mainTextureScale;
			scale.y = -scale.y;
		} else {
			decalProjector = GetComponent<DecalProjector>();
			scale = decalProjector.uvScale;
		}
		
		if (scale.x == 0f) return;
		
		spriteWidth = (int)Mathf.Round(1f / scale.x);
		spriteHeight = (int)Mathf.Round(1f / scale.y);
		Debug.Log("width: " + spriteWidth + "; height: " + spriteHeight + "; scale: " + scale);
	}

	IEnumerator WaitThenPlay() {
		yield return new WaitForSeconds(splineMoveDelays[splineMoveIndex]);
		splineAnimations[splineMoveIndex++].Play();
		readyForNextSplineMove = true;
	}

    // Update is called once per frame
    void Update() {
		if (animator != null) {
			var splinePlaying = splineAnimations.Any(anim => anim.IsPlaying);
			var animPlaying = animator.GetCurrentAnimatorStateInfo(0).IsName(animationOnSplineMove);
			if (splinePlaying && !animPlaying)
				animator.Play(animationOnSplineMove);
			else if (!splinePlaying && animPlaying)
				animator.Play(animationAfterSplineMove);

			if (splineMoveIndex < splineMoveDelays.Length &&
				splineMoveIndex < splineAnimations.Length &&
				readyForNextSplineMove
			) {
				readyForNextSplineMove = false;
				StartCoroutine(WaitThenPlay());
			}
		}

    	if (renderer == null && decalProjector == null) return;

		var costumeX = costume % spriteWidth;
		var costumeY = costume / spriteWidth;
		var offset = new Vector2(
	    	(float)costumeX / spriteWidth,
	    	(float)costumeY / spriteHeight);
    	
    	if (renderer != null) {
    		offset.y = -offset.y;
		    renderer.sharedMaterial.mainTextureOffset = offset;
		 } else {
		 	decalProjector.uvBias = offset;
		 }
    }
}
