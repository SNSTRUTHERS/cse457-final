using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	public float startingVelocity = 2.25f;
	public float freezeAt = 9.7f;
	private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(startingVelocity, 0.5f, 0f);
        StartCoroutine(FreezeAfterSecs());
    }
    
    IEnumerator FreezeAfterSecs() {
    	yield return new WaitForSeconds(freezeAt);
    	rb.constraints = RigidbodyConstraints.FreezeAll;
    	transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
