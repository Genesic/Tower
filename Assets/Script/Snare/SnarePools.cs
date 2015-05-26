using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnarePools : ObjectPools<SnarePools, SnareObject>
{
    protected override Transform ContainerTs
    {
        get
        {
            if (mGameMgr != null)
                return mGameMgr.MapMgr.SnareContainerTs;

            return GameObject.FindObjectOfType<MapManager>().SnareContainerTs;
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    public static SnareObject ObtainObject(string id)
    {
        if (Instance == null)
            GameObject.FindObjectOfType<MapManager>().gameObject.AddComponent<SnarePools>();

        return Instance.Obtain(id);
    }

    public static void Retrieve(SnareObject obj)
    {
        Instance.InsRetrieve(obj);
    }

    public override SnareObject CreateNew(string id)
    {
        var path = string.Format("Prefab/Snares/{0}", id);
        var prefab = Resources.Load<GameObject>(path);
        var go = Instantiate<GameObject>(prefab);
        var ts = go.transform;
        ts.SetParent(ContainerTs);

        var component = go.GetComponent<SnareObject>();
        component.SetParam(id);

        return component;
    }
}
