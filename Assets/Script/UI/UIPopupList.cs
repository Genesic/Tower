using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DaikonForge.Tween;

public class UIPopupList : MonoBehaviour
{
    #region Field

    [SerializeField]
    private GameObject m_ItemPrefab;

    [SerializeField]
    private Button m_ExpandBtn;

    [SerializeField]
    private Text m_ExpandText;

    [SerializeField]
    private RectTransform m_ContentRT;

    [SerializeField]
    private VerticalLayoutGroup m_ContentVerticalLayoutGroup;

    [SerializeField]
    private Image m_WholeImg;
    
    [SerializeField]
    private Scrollbar m_Scrollbar;

    [SerializeField]
    private CanvasGroup m_BottomCanvasGroup;

    [SerializeField]
    private List<string> m_ItemList;

    [SerializeField]
    private Queue<PopupItem> m_PopupItems = new Queue<PopupItem>();

    [SerializeField]
    private float m_DropSpeed = 0.1f;

    private bool mIsExpand = false;
    private bool IsExpand {
        get { return mIsExpand; }
        set
        {
            mIsExpand = value;
            
        }
    }

    private TweenGroup mTweenGroup = new TweenGroup();

    #endregion

    void Awake()
    {
        //隱藏範圍示意圖
        m_WholeImg.enabled = false;

        m_ExpandBtn.onClick.AddListener(OnSwitch);

        TweenInit();

        CollapseList();

        SetExpendBtnText(m_ItemList.Count > 0 ? m_ItemList[0] : "-----");
    }

    void Start()
    {
        
    }

    void OnDestroy()
    {
        m_ExpandBtn.onClick.RemoveListener(OnSwitch);
    }

    private void OnSwitch()
    {
        if (IsExpand)
            CollapseList();
        else
            ExpandList();
    }

    /// <summary>展開列表</summary>
    private void ExpandList()
    {
        IsExpand = true;

        SetBottomContentShow(true);

        UpdateList();
    }

    /// <summary>折疊列表</summary>
    private void CollapseList()
    {
        IsExpand = false;

        SetBottomContentShow(false);

        mTweenGroup.Stop();

        RemoveItems();
    }

    private void UpdateList()
    {
        RemoveItems();

        mTweenGroup.ClearTweens();

        int itenNum = m_ItemList.Count;

        for (int i = 0; i < itenNum; i++)
        {
            var itemGo = Instantiate(m_ItemPrefab) as GameObject;
            itemGo.transform.SetParent(m_ContentRT, false);

            var popupItem = itemGo.GetComponent<PopupItem>();
            popupItem.SetParam(i, m_ItemList[i]);
            popupItem.ClickCallback += PopupItemClickHandle;

            m_PopupItems.Enqueue(popupItem);
        }

        SetItemFadeInToPlay();
    }

    private void PopupItemClickHandle(PopupItem popupItem)
    {
        SetExpendBtnText(m_ItemList[popupItem.Index]);

        CollapseList();
    }

    private void SetExpendBtnText(string text)
    {
        m_ExpandText.text = text;
    }

    private void RemoveItems()
    {
        while (m_PopupItems.Count > 0)
        {
            var popupItem = m_PopupItems.Dequeue();
            Destroy(popupItem.gameObject);
        }
    }

    private void SetBottomContentShow(bool show)
    {
        m_BottomCanvasGroup.alpha = show ? 1f : 0f;
    }

    private void SetBottomContentEnable(bool enable)
    {
        m_BottomCanvasGroup.interactable = enable;
        m_BottomCanvasGroup.blocksRaycasts = enable;
    }

    #region Tween

    private void TweenInit()
    {
        mTweenGroup.SetMode(TweenGroupMode.Concurrent);
        mTweenGroup.OnStarted((TweenBase sender) =>
        {
            SetBottomContentEnable(false);
        });

        mTweenGroup.OnCompleted((TweenBase sender) =>
        {
            SetBottomContentEnable(true);
        });
    }

    private void SetItemFadeInToPlay()
    {
        StartCoroutine(DelaySetItemTweenIn());

        TweenFadeIn();
    }

    private IEnumerator DelaySetItemTweenIn()
    {
        yield return new WaitForEndOfFrame();

        var popupItems = m_PopupItems.ToArray();
        int num = Mathf.Min(popupItems.Length, 5);

        for (int i = 0; i < num; i++)
        {
            var popupItem = popupItems[i];
            var itemTween = GetItemTween(popupItem, i);
            mTweenGroup.AppendTween(itemTween);
        }
    }

    private Tween<float> GetItemTween(PopupItem popupItem, int index)
    {
        //Debug.Log(index + ":" + popupItem.RT.anchoredPosition.y);

        var tween = Tween<float>.Obtain();
        tween.SetEasing(TweenEasingFunctions.Linear);
        tween.SetStartValue(25f);
        tween.SetEndValue(popupItem.RT.anchoredPosition.y);
        //tween.OnSyncStartValue(() => { return 25f; });
        //tween.OnSyncEndValue(() => { return popupItem.RT.anchoredPosition.y; });

        tween.SetDuration(index * m_DropSpeed);
        tween.SetLoopType(TweenLoopType.None);
        tween.OnExecute((float y) =>
        {
            var pos = popupItem.RT.anchoredPosition;
            pos.y = y;
            popupItem.RT.anchoredPosition = pos;
        });

        return tween;
    }
    private void TweenFadeIn()
    {
        mTweenGroup.Play();
    }

    #endregion

}
