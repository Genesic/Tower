using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MonsterHUD : UIFollow
{
    [SerializeField]
    private Slider m_Health = null;

    [SerializeField]
    private Text m_Text = null;

    [SerializeField]
    private Text m_DebugInfo = null;
    
    protected override Vector3 Offset { get { return new Vector3(0f, 2f, 0f); } }

    public override void SetEnable()
    {
        base.SetEnable();
    }

    public override void SetDisable()
    {
        base.SetDisable();

        ResetData();
    }

    public void SetTarget(Transform target)
    {
        mTarget = target;
    }

    private void ResetData()
    {
        mTarget = null;
    }

    public void SetHealth(float percent)
    {
        m_Health.value = percent;
        m_Text.text = (percent * 100f).ToString();
    }

    public void DebugInfo(string text)
    {
        m_DebugInfo.text = text;
    }
}