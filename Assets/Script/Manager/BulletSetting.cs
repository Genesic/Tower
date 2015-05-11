using UnityEngine;
using System.Collections;

public class BulletSetting : IPool
{
    public GameObject explosion;

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

    void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.layer == LayerMask.NameToLayer ("Monster")) {
        Instantiate(explosion, transform.position, transform.rotation);
        SetDisable();
        BulletManager.Retrieve(this);
        //} else {
        //	Debug.Log ("trigger:"+other.gameObject);
        //}
    }
}
