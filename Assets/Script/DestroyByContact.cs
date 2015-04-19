using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
	public GameObject explosion;
	
	void OnTriggerEnter(Collider other){
		if (other.gameObject.layer == LayerMask.NameToLayer("Monster")) {
			Instantiate (explosion, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}
