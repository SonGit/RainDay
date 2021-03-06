﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ATTACH TO ObjectFactory gameObject
public class ObjectFactory: MonoBehaviour {

	public static ObjectFactory instance;

	void Awake()
	{
		if (instance == null) {
			instance = this;
		}
	}

	public enum PrefabType
	{
		None,

		// Effect
		WaterSplash,
		HitFX,
		GiftFX,
		LifeFX,
		HeartFX,
		GpsFX,
		StunFX,
		DizzyFX,
		FallFX,
		RedScore,
		BlueScore,
		YellowScore,

		// Audio
		AudioSource,

		// GameObject
		Rock,

		// AIs
		PlayerBoy,
		PlayerGirl,
	}

	public Dictionary<PrefabType,string> PrefabPaths = new Dictionary<PrefabType, string> {
		
		{ PrefabType.None, "" },

		// Effect
		{ PrefabType.WaterSplash, "Prefabs/FX/Water_Splash" },
		{ PrefabType.HitFX, "Prefabs/FX/Hit" },
		{ PrefabType.GiftFX, "Prefabs/FX/GiftExplosive" },
		{ PrefabType.HeartFX, "Prefabs/FX/Heart" },
		{ PrefabType.GpsFX, "Prefabs/FX/gpsFX" },
		{ PrefabType.StunFX, "Prefabs/FX/StunFX" },
		{ PrefabType.DizzyFX, "Prefabs/FX/DizzyFX" },
		{ PrefabType.FallFX, "Prefabs/FX/FallFX" },
		{ PrefabType.RedScore, "Prefabs/Score/ScoreRed" },
		{ PrefabType.BlueScore, "Prefabs/Score/ScoreBlue" },
		{ PrefabType.YellowScore, "Prefabs/Score/ScoreYellow" },
		// Audio
		{ PrefabType.AudioSource, "Prefabs/AudioSource" },

		// GameObject
		{ PrefabType.Rock, "Prefabs/Rock" },

		// Enemy
		{ PrefabType.PlayerBoy, "Prefabs/Player/Player_Boy" },
		{ PrefabType.PlayerGirl, "Prefabs/Player/Player_Girl" },

	};

	// Make GameObject from Resources
	public GameObject MakeObject(PrefabType type)
	{
		string path;
		if (PrefabPaths.TryGetValue (type, out path)) {
			return (Instantiate (Resources.Load (path, typeof(GameObject))) as GameObject);
		}
		print ("NULL");
		return null;
	}

	// Load gameObject from resources
	public GameObject LoadObject(PrefabType type)
	{
		string path;
		if (PrefabPaths.TryGetValue (type, out path)) {
			return (Resources.Load (path, typeof(GameObject))) as GameObject;
		}
		print ("NULL");
		return null;
	}

}
