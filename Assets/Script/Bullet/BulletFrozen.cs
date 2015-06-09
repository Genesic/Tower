using UnityEngine;
using System.Collections;

public class BulletFrozen : BulletBasic {

	void OnTriggerEnter(Collider other)
	{
		// 處理擊中判定
		onHit (other);
	}

	public override void setDamage(Collider other){
		Instantiate(explosion, transform.position, transform.rotation);
		//帶 緩速(維持3秒，速度為原來的30%) 攻擊
		var damageAppendBuff = new BuffData(BuffType.MoveSpeed, 3f, 0.3f);
		other.gameObject.GetComponent<MonsterAI>().Damage(damage, damageAppendBuff);
	}
}
