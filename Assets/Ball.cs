using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	public float startingVelocity = 2.25f;
	public float freezeAt = 9.7f;
	private Rigidbody rb;
	private AudioSource audioSource;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(startingVelocity, 0.5f, 0f);
        StartCoroutine(FreezeAfterSecs());
        
        audioSource = GetComponent<AudioSource>();
    }
    
    IEnumerator FreezeAfterSecs() {
    	yield return new WaitForSeconds(freezeAt);
    	rb.constraints = RigidbodyConstraints.FreezeAll;
    	transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
    
    void OnCollisionEnter(Collision collision) {
    	Debug.Log(collision.relativeVelocity + " " + collision.contacts[0].point + " " + audioSource);
    	audioSource.Play();
    }
}
