using UnityEngine;
using System.Collections;

/// <summary>圖層管理</summary>
public static class Layers
{
    #region Layer String

    private const string DEFAULT = "Default";
	private const string TRANSPARENT_FX = "TransparentFX";
	private const string IGNORE_RAYCAST = "Ignore Raycast";
	private const string WATER = "Water";
    private const string UI = "UI";
    private const string MONSTER = "Monster";
	private const string CANNON_PLATFORM = "CannonPlatform";
	private const string SNARE = "Snare";
    private const string BULLET = "bullet";
    private const string SNARE_OBJECT = "Snare";
    
    #endregion

    #region Layer Int

    public static readonly int Default = LayerMask.NameToLayer(DEFAULT);
	public static readonly int TransparentFX = LayerMask.NameToLayer(TRANSPARENT_FX);
	public static readonly int IgnoreRaycast = LayerMask.NameToLayer(IGNORE_RAYCAST);
	public static readonly int Water = LayerMask.NameToLayer(WATER);
    public static readonly int Monster = LayerMask.NameToLayer(MONSTER);
    public static readonly int CannonPlatform = LayerMask.NameToLayer(CANNON_PLATFORM);
    public static readonly int Snare = LayerMask.NameToLayer(SNARE);
    public static readonly int Bullet = LayerMask.NameToLayer(BULLET);
    public static readonly int SnareObject = LayerMask.NameToLayer(SNARE_OBJECT);

    #endregion

    #region Layer Mask

    public static readonly LayerMask DefaultMask = (1 << Default);
	public static readonly LayerMask TransparentFXMask = (1 << TransparentFX);
	public static readonly LayerMask IgnoreRaycastMask = (1 << IgnoreRaycast);
	public static readonly LayerMask WaterMask = (1 << Water);
    public static readonly LayerMask MonsterMask = (1 << Monster);

    //public static readonly LayerMask MonsterMask = LayerMask.GetMask(MONSTER);

    #endregion

    #region Combine Layer Mask

    //public static readonly LayerMask BulletHitMask = DefaultMask | TerrainMask | TankMask;
    //public static readonly LayerMask ObstacleMask = DefaultMask | TerrainMask;
    //public static readonly LayerMask DamageMask = TankMask;
    //public static readonly LayerMask CamHitMask = DefaultMask | TerrainMask;

    #endregion

	#region layer helper

	/// <summary>比對是否符合</summary>
	public static bool IsContain(LayerMask _total, LayerMask _target)
	{
		return (_total & _target) == _target;
	}

	/// <summary>排除指定 Layer</summary>
	public static LayerMask ExcludeLayer(LayerMask total, LayerMask exclude)
	{
		return total & ~exclude;
	}

	#endregion
}