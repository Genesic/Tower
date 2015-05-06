using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour, ICannon {
	public float searchRadius;
	public float speed;
	public int damage;
	public int cost;
	public int price;
	public int level;
	public string tower_name;

	
	public GameObject shot;
	public float fireRate;
	
	private float nextFire;
	private float startFire;
	private float startRate = 1.0F;

	public float Speed { get { return speed;}  }
	public int Damage { get { return damage;}  }
	public int Cost { get {return cost;} }
	public int Price { get {return price;} }
	public int Level { get { return level; } }
	public string towerName { get { return tower_name; } }

	private MonsterAI lockMonster;
	private Transform child_rotate;

	void Awake(){
		Transform[] allChildren = GetComponentsInChildren<Transform>();
		foreach (Transform child in allChildren) {
			if( child.tag == "tower_rotate" ){
				child_rotate = child;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, searchRadius, LayerMask.GetMask("Monster"));
		
		int minHp = 0;
		int LockIdx = -1;
		for (int i=0; i<hitColliders.Length; i++) {
			Collider tmpCollider = hitColliders[i];
			var monster = tmpCollider.gameObject.GetComponent<MonsterAI>();
			if( monster ){
				int checkHp = monster.getHp();
				if( (checkHp < minHp || minHp == 0) && checkHp > 0){
					minHp = monster.getHp();
					if( lockMonster == null || lockMonster.GetInstanceID() != monster.GetInstanceID() )
						startFire = Time.time + startRate;
					
					lockMonster = monster;
					LockIdx = i;
				}
			}
		}

		Vector3 ShotSpwan = child_rotate.position + new Vector3(0,0.5f,0);
		
		if (LockIdx >= 0) {
			Collider LockCollider = hitColliders [LockIdx];
			if (Time.time > nextFire && Time.time > startFire) {
				nextFire = Time.time + fireRate;
				GameObject Bullet = (GameObject)Instantiate (shot, ShotSpwan, transform.rotation);
				Vector3 direction = (LockCollider.gameObject.transform.position - ShotSpwan).normalized;
				Bullet.GetComponent<Rigidbody> ().velocity = direction * speed;
				LockCollider.gameObject.GetComponent<MonsterAI> ().Damage(damage);
			}
		}
	}

	void FixedUpdate() {
		if (lockMonster) {
			Vector3 targetDir = lockMonster.transform.position - child_rotate.transform.position;
			Vector3 newDir = Vector3.RotateTowards( child_rotate.transform.forward, targetDir, 0.05F, 0.0F );				
			child_rotate.transform.rotation = Quaternion.LookRotation(newDir);
		}
	}

	public void destroy(){
		Destroy (gameObject);
	}
}
