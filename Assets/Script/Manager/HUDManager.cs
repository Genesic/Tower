using UnityEngine;
using System.Collections;

public class HUDManager : ObjectPools<HUDManager, MonsterHUD>
{
    private RectTransform mContainerRTS = null;

    protected override Transform ContainerTs { get { return mContainerRTS; } }
    
    void Awake()
    {
        base.Awake();

        var containerGo = new GameObject("MonsterHUDContainer");
        mContainerRTS = containerGo.AddComponent<RectTransform>();
        mContainerRTS.SetParent(mGameMgr.UICanvas.transform);
        mContainerRTS.localScale = Vector3.one;
        mContainerRTS.anchoredPosition = Vector3.zero;
    }

    public static void Retrieve(MonsterHUD obj)
    {
        Instance.InsRetrieve(obj);
    }

    public override MonsterHUD CreateNew(string id)
    {
        var path = string.Format("Prefab/HUD/{0}", id);
        var prefab = Resources.Load<GameObject>(path);
        var go = Instantiate(prefab) as GameObject;
        var ts = go.transform;

        ts.SetParent(ContainerTs);
        ts.localScale = Vector3.one;

        var hud = go.GetComponent<MonsterHUD>();
        hud.SetParam(id, mGameMgr.UICanvas);

        return hud;
    }
}
