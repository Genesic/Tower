using UnityEngine;
using System.Collections;

/// <summary>投石陷阱接收者</summary>
public class FallStoneReceiver : SnareReceiver
{
    [SerializeField]
    private Vector3 m_Force = Vector3.forward;

    [SerializeField]
    private Vector3 m_Torque = Vector3.zero;

    [SerializeField]
    private float m_FallStoneLifeTime = 10f;

    protected override string EmitObjectID { get { return "FallStone"; } }

    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnSnareTrigger()
    {
        SpawnFallStone();
    }

    private void SpawnFallStone()
    {
        FallStoneObject snareObject = SnarePools.ObtainObject(EmitObjectID) as FallStoneObject;
        snareObject.SetLifeTime(m_FallStoneLifeTime);
        snareObject.SetEnable();

        var ts = snareObject.transform;
        ts.position = transform.position;
        ts.rotation = transform.rotation;

        var rigid = snareObject.GetComponent<Rigidbody>();
        rigid.AddRelativeForce(m_Force, ForceMode.VelocityChange);
        rigid.AddRelativeTorque(m_Torque, ForceMode.VelocityChange);
    }
}
