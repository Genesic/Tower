using UnityEngine;
using System.Collections;

public class BulletFrozen : BulletBasic {

	void OnTriggerEnter(Collider other)
	{
		// 處理基本的擊中扣血和爆炸
		onHit (other);

		// 緩速怪的速度
	}
}
