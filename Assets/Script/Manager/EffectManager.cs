using UnityEngine;
using System.Collections;

public class EffectManager : ObjectPools<EffectManager, EffectSetting>
{
    protected override Transform ContainerTs { get { return mGameMgr.MapMgr.EffectContainerTs; } }

    public static void Retrieve(EffectSetting obj)
    {
        Instance.InsRetrieve(obj);
    }

    public override EffectSetting CreateNew(string id)
    {
        var path = string.Format("Effects/MagicProjectiles/{0}", id);
        var prefab = Resources.Load<GameObject>(path);
        var enemyGo = Instantiate(prefab) as GameObject;
        var enemyTs = enemyGo.transform;
        enemyTs.SetParent(ContainerTs);

        var setting = enemyGo.AddComponent<EffectSetting>();
        setting.SetParam(id);

        return setting;
    }
}
