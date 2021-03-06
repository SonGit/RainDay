﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ATTACH TO ObjectPool gameObject

public class ObjectPool : MonoBehaviour {

	public static ObjectPool instance;

	// Effect
	GenericObject<WaterFX> waterFX;
	GenericObject<HitFX> hitFX;
	GenericObject<GiftFX> giftFX;
	GenericObject<HeartFX> heartFX;
	GenericObject<GpsFX> gpsFX;
	GenericObject<StunFX> stunFX;
	GenericObject<DizzyFX> dizzyFX;
	GenericObject<FallFX> fallFX;
	GenericObject<Score> redScoreFX;
	GenericObject<Score> blueScoreFX;
	GenericObject<Score> yellowScoreFX;
	GenericObject<RainGirl> playerGirl;
	GenericObject<RainGirl> playerBoy;
	GenericObject<AudioSource_RG> audiosource;
	void Awake()
	{
		if (instance == null) {
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;
		// Effect
		waterFX = new GenericObject<WaterFX>(ObjectFactory.PrefabType.WaterSplash,10);
		hitFX = new GenericObject<HitFX>(ObjectFactory.PrefabType.HitFX,10);
		giftFX = new GenericObject<GiftFX>(ObjectFactory.PrefabType.GiftFX,1);
		heartFX = new GenericObject<HeartFX>(ObjectFactory.PrefabType.HeartFX,1);
		gpsFX = new GenericObject<GpsFX>(ObjectFactory.PrefabType.GpsFX,10);
		stunFX = new GenericObject<StunFX>(ObjectFactory.PrefabType.StunFX,10);
		dizzyFX = new GenericObject<DizzyFX>(ObjectFactory.PrefabType.DizzyFX,10);
		fallFX = new GenericObject<FallFX>(ObjectFactory.PrefabType.FallFX,10);
		redScoreFX = new GenericObject<Score>(ObjectFactory.PrefabType.RedScore,5);
		blueScoreFX = new GenericObject<Score>(ObjectFactory.PrefabType.BlueScore,10);
		yellowScoreFX = new GenericObject<Score>(ObjectFactory.PrefabType.YellowScore,10);
		playerGirl = new GenericObject<RainGirl>(ObjectFactory.PrefabType.PlayerGirl,20);
		playerBoy = new GenericObject<RainGirl>(ObjectFactory.PrefabType.PlayerBoy,20);
		audiosource = new GenericObject<AudioSource_RG>(ObjectFactory.PrefabType.AudioSource,10);
	}

	#region Effect
	public WaterFX GetWaterFX()
	{
		return waterFX.GetObj ();
	}

	public HitFX GetHitFX()
	{
		return hitFX.GetObj ();
	}

	public GiftFX GetGiftFX()
	{
		return giftFX.GetObj ();
	}
	public HeartFX GetHeartFX()
	{
		return heartFX.GetObj ();
	}
	public GpsFX GetGpsFX()
	{
		return gpsFX.GetObj ();
	}
	public StunFX GetStunFX()
	{
		return stunFX.GetObj ();
	}
	public DizzyFX GetDizzyFX()
	{
		return dizzyFX.GetObj ();
	}
	public FallFX GetFallFX()
	{
		return fallFX.GetObj ();
	}

	public Score GetRedScoreFX()
	{
		return redScoreFX.GetObj ();
	}
	public Score GetBlueScoreFX()
	{
		return blueScoreFX.GetObj ();
	}
	public Score GetYellowScoreFX()
	{
		return yellowScoreFX.GetObj ();
	}

	public RainGirl GetPlayerGirl()
	{
		return playerGirl.GetObj ();
	}

	public RainGirl GetPlayerBoy()
	{
		return playerBoy.GetObj ();
	}
	public AudioSource_RG GetAudioSource()
	{
		return audiosource.GetObj ();
	}
	#endregion



}
