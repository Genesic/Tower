using UnityEngine;
using System.Collections;

public class CoreBase : MonoBehaviour
{
    public const int HP_MAX = 1000;
    public int Hp = HP_MAX;

    public void Damage(int damage)
    {
        Hp = Mathf.Max(0, Hp - damage);

        if (Hp == 0)
        {
            CoreBomb();
        }
    }

    private void CoreBomb()
    {
        Debug.Log("CoreBomb");
    }
}
