using UnityEngine;
using System.Collections;

public class EffectSetting : MonoBehaviour, IPool
{
    private string mID = string.Empty;
    public string ID { get { return mID; } }

    private Transform mTs = null;

    void Awake()
    {
        mTs = transform;
    }

    public void SetDisable()
    {
        gameObject.SetActive(false);
    }

    public void SetParam(string id)
    {
        mID = id;
    }

    public void SetPosition(Vector3 position)
    {
        mTs.position = position;
    }

    public void SetRotation(Quaternion rotation)
    {
        mTs.rotation = rotation;
    }

    public void SetEnable()
    {
        gameObject.SetActive(true);

        StartCoroutine(CalculateLifePeriod());
    }

    private IEnumerator CalculateLifePeriod()
    {
        yield return new WaitForSeconds(5f);

        EffectManager.Retrieve(this);
    }

}
