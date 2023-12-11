using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToAfterSecs : MonoBehaviour {
	public float seconds;
	public Vector3 newPosition;
	public Vector3 newRotation;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(MoveAfterTime(seconds));
    }
    
    IEnumerator MoveAfterTime(float seconds) {
    	yield return new WaitForSeconds(seconds);
    	transform.rotation = Quaternion.Euler(newRotation);
    	transform.position = newPosition;
    }
}
