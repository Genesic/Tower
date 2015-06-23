using UnityEngine;
using System.Collections;

public class BulletTesla : BulletBasic {

	private float wait = 1.0F;
		
	public override void setDamage(Collider other){
		StartCoroutine (toDamage (other));
	}
	
	IEnumerator toDamage(Collider other){
		yield return new WaitForSeconds(wait);
		other.gameObject.GetComponent<MonsterAI>().Damage(damage);
	}

}
