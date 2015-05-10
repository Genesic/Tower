using UnityEngine;
using System.Collections;

public class BulletManager : ObjectPools<BulletManager, BulletSetting> {

	protected override Transform ContainerTs { get { return mGameMgr.MapMgr.BulletContainerTs; } }

	protected override void Awake()
	{
		base.Awake();
	}
	
	protected override void OnDestroy()
	{
		base.OnDestroy();
	}
	
	public static void Retrieve(BulletSetting obj)
	{
		Instance.InsRetrieve(obj);
	}
	
	public override BulletSetting CreateNew(string id)
	{
		var path = string.Format("prefabs/Bullet/{0}", id);
		var prefab = Resources.Load<GameObject>(path);
		var bulletGo = Instantiate(prefab) as GameObject;
		var bulletTs = bulletGo.transform;
		bulletTs.SetParent(ContainerTs);

		BulletSetting setting = bulletGo.GetComponent<BulletSetting> ();
		setting.SetParam (id);

		return setting;
	}
}
