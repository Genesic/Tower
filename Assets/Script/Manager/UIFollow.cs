using UnityEngine;
using System.Collections;

public abstract class UIFollow : IPool
{
    #region Object Pool

   
    //public string ID { get { return mID; } }

    /*public virtual void SetEnable()
    {
        gameObject.SetActive(true);
    }

    public virtual void SetDisable()
    {
        gameObject.SetActive(false);
    }*/

    #endregion

    protected Camera mWorldCam = null;
    protected Camera mUICamera = null;

    protected RectTransform mCanvasRTs = null;

    protected RectTransform mRTS = null;

    protected Transform mTarget = null;

    protected virtual Vector3 Offset { get { return Vector3.zero; } }

    protected virtual void Awake()
    {
        mRTS = transform as RectTransform;
    }

    protected virtual void LateUpdate()
    {
        if (mCanvasRTs == null || mTarget == null || mWorldCam == null || mRTS == null)
            return;

        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(mWorldCam, mTarget.position + Offset);

        Vector3 worldPoint;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(mCanvasRTs, screenPoint, mUICamera, out worldPoint))
        {
            mRTS.position = worldPoint;
        }
    }

    public void SetParam(string id, Canvas canvas)
    {
        mID = id;

        mCanvasRTs = canvas.transform as RectTransform;
        mUICamera = canvas.worldCamera;
        mWorldCam = Camera.main;
    }

}
