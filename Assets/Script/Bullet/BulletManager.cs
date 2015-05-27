using UnityEngine;
using System.Collections;

public class BulletManager : ObjectPools<BulletManager, BulletBasic> {

	protected override Transform ContainerTs { get { return mGameMgr.MapMgr.BulletContainerTs; } }

	protected override void Awake()
	{
		base.Awake();
	}
	
	protected override void OnDestroy()
	{
		base.OnDestroy();
	}
	
	public static void Retrieve(BulletBasic obj)
	{
		Instance.InsRetrieve(obj);
	}
	
	public override BulletBasic CreateNew(string id)
	{
		var path = string.Format("prefabs/Bullet/{0}", id);
		var prefab = Resources.Load<GameObject>(path);
		var bulletGo = Instantiate(prefab) as GameObject;
		var bulletTs = bulletGo.transform;
		bulletTs.SetParent(ContainerTs);

		BulletBasic setting = bulletGo.GetComponent<BulletBasic> ();
		setting.SetParam (id);

		return setting;
	}
}
