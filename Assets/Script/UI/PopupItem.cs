using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class PopupItem : MonoBehaviour
{
    public event UnityAction<PopupItem> ClickCallback;

    [SerializeField]
    private Button m_ItemBtn;

    [SerializeField]
    private Text m_ItemText;

    [SerializeField]
    private LayoutElement m_LayoutElement;
    public LayoutElement LayoutElement { get { return m_LayoutElement; } }

    private RectTransform mRT;
    public RectTransform RT { get { return mRT; } }

    public int Index { get; private set; }
    public string Name { get; private set; }

    void Awake()
    {
        mRT = GetComponent<RectTransform>();

        m_ItemBtn.onClick.AddListener(OnClickHandle);
    }

    void OnDestroy()
    {
        m_ItemBtn.onClick.RemoveListener(OnClickHandle);

        ClickCallback = null;
    }

    private void OnClickHandle()
    {
        if (ClickCallback != null)
            ClickCallback(this);
    }

    public void SetParam(int itemIndex, string itemName)
    {
        Index = itemIndex;
        Name = itemName;

        Refresh();
    }

    private void Refresh()
    {
        m_ItemText.text = string.Format("{0}", Name);
    }
}
