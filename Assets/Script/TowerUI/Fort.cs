 using UnityEngine;
using System.Collections;

public class Fort : MonoBehaviour {
	[SerializeField]
	private float fireRate;

	[SerializeField]
	private float speed;

	[SerializeField]
	private int damage;

	[SerializeField]
	private int cost;

	[SerializeField]
	private int price;

	[SerializeField]
	private string tower_name;

	public float Speed { get { return speed;}  }
	public int Damage { get { return damage;}  }
	public int Cost { get {return cost;} }
	public int Price { get {return price;} }
	public float FireRate { get { return fireRate; } }
	public string towerName { get { return tower_name; } }
}
