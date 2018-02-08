using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTouchTrigger : MonoBehaviour {

	public float delay; // The delay before being destroying
	public string[] tagsToDestroy; // The tags to destroy self (Any if null)
	public string[] tagsToIgnore; // The tags to ignore (If damage all tags)

    private bool setToDestroy; // Whether the object has been set to destroy itself

	void OnTriggerEnter (Collider other) {
		if (tagsToDestroy.Length != 0) { // If the tags to destroy is not null
			foreach (string tag in tagsToDestroy) { // Goes through each tag
				if (other.CompareTag (tag)) { // If the collided object is something that should cause this object to destroy itself
					Destroy (gameObject, delay); // Destroy this object after a delay
				}
			}
        } else if (tagsToIgnore.Length != 0) { // If the tags to ignore is not null
            setToDestroy = true; // Setting the bool
 			foreach (string tag in tagsToIgnore) { // Goes through each tag
				if (other.CompareTag (tag)) { // If the collided object is not something that should cause this object to destroy itself
                    setToDestroy = false; // Setting the bool
				}
			}
            if (setToDestroy) { // If the bool is still true
				Destroy (gameObject, delay); // Destroy this object after a delay
			}
		} else {
			Destroy (gameObject, delay); // Destroy this object after a delay
		}
	}
}
