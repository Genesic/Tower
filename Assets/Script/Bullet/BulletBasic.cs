using UnityEngine;
using System.Collections;

public class BulletBasic : IPool
{
    public GameObject explosion;

	public int damage;

    private Transform mTs = null;

    void Awake()
    {
        mTs = transform;
    }

    public void SetParam(string id)
    {
        mID = id;
    }

    public void SetPosition(Vector3 position)
    {
        mTs.position = position;
    }

    public void SetRotation(Quaternion rotation)
    {
        mTs.rotation = rotation;
    }

    public void SetVelocity(Vector3 direction, float speed)
    {
        GetComponent<Rigidbody>().velocity = direction * speed;
    }

    public void SetExplosion(GameObject explosion_prefab)
    {
        explosion = explosion_prefab;
    }

	public void onHit(Collider other)
	{
		Instantiate(explosion, transform.position, transform.rotation);
		SetDisable();
		BulletManager.Retrieve(this);
		
		if (other.gameObject.layer == LayerMask.NameToLayer ("Monster")) {
			other.gameObject.GetComponent<MonsterAI> ().Damage (damage);
		}
	}

    void OnTriggerEnter(Collider other)
    {    
		onHit (other);
    }
}
