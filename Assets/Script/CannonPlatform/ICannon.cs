using UnityEngine;
using System.Collections;

public interface ICannon
{
    int Cost { get; }
    int Price { get; }
	float Speed { get; }
	int Damage { get; }
	int Level { get; }
	string towerName { get; }
	void destroy();
}
