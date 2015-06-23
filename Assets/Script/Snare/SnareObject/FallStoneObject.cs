using UnityEngine;
using System.Collections;

public class FallStoneObject : SnareObject
{
    private float mLifrTime = 10f;
    private float mRadius = 1f;

    private Rigidbody mRigid = null;
    private SphereCollider mSphereCD = null;

    //SpinningEarth
    /*[SerializeField]
    private GameObject m_DustPrefab = null;

    private GameObject mDustEffectGo = null;
    private GameObject DustEffectGo
    {
        get
        {
            if (mDustEffectGo == null)
            {
                mDustEffectGo = Instantiate<GameObject>(m_DustPrefab);
                var ts = mDustEffectGo.transform;
                ts.SetParent(transform);
                ts.localPosition = Vector3.zero;
                ts.localRotation = Quaternion.identity;
            }

            return mDustEffectGo;
        }
    }*/

    protected override void Awake()
    {
        base.Awake();

        mRigid = GetComponent<Rigidbody>();
        mSphereCD = GetComponent<SphereCollider>();
        mRadius = mSphereCD.radius * Ts.localScale.x;
    }

    public void SetLifeTime(float lifeTime)
    {
        mLifrTime = lifeTime;
    }

    private IEnumerator CalculateLifePeriod()
    {
        yield return new WaitForSeconds(mLifrTime);

        SnarePools.Retrieve(this);
    }

    public override void SetEnable()
    {
        base.SetEnable();

        StartCoroutine(CalculateLifePeriod());
    }

    public override void SetDisable()
    {
        base.SetDisable();

        ResetParams();
    }

    protected override void ResetParams()
    {
        mRigid.velocity = Vector3.zero;
        mRigid.angularVelocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (mRigid.velocity.sqrMagnitude == 0f)
            return;

        HitDetect(collision.collider);
    }

    private void HitDetect(Collider co)
    {
        var monster = co.GetComponent<MonsterAI>();
        if (monster != null)
            monster.CollisionHit(99999, 40f, Ts.position, 5f, 3.5f);
    }

}
