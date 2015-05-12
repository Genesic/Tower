using UnityEngine;
using System.Collections;

public class BaseCore : MonoBehaviour
{
    public const int HP_MAX = 1000;
    public int Hp = HP_MAX;

    public void Damage(int damage)
    {
        Hp = Mathf.Max(0, Hp - damage);

        UpdateBaseCoreHP((float)Hp / HP_MAX);
        
        if (Hp == 0)
        {
            CoreBomb();
        }
    }

    private void UpdateBaseCoreHP(float percent)
    {
        Debug.Log("UpdateBaseCoreHP:" + percent);

        GameManager.Instance.BaseHUD.SetUIHP(percent);
    }

    private void CoreBomb()
    {
        Debug.Log("CoreBomb");

    }
}
