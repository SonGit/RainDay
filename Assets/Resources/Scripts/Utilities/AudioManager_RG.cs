using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager_RG : MonoBehaviour {

	public static AudioManager_RG instance;

	private Dictionary<SoundFX,AudioClip> clips;

	[HideInInspector]
	public string isOnSound = "t";

	void Awake()
	{
		instance = this;
	}

	IEnumerator Start()
	{
		

		clips = new Dictionary<SoundFX, AudioClip> {

			{ SoundFX.None, null },
			{ SoundFX.Stun, Resources.Load<AudioClip>("Sounds/Stun") },
			{ SoundFX.PressBtn, Resources.Load<AudioClip>("Sounds/PressBtn") },
			{ SoundFX.bite_hit, Resources.Load<AudioClip>("Sounds/bite_hit") },
			{ SoundFX.WaterSplash, Resources.Load<AudioClip>("Sounds/water_splash") },
			{ SoundFX.RightHome, Resources.Load<AudioClip>("Sounds/Righthome") },
			{ SoundFX.Gps, Resources.Load<AudioClip>("Sounds/GPS") },
			{ SoundFX.fence, Resources.Load<AudioClip>("Sounds/fence") },
			{ SoundFX.life, Resources.Load<AudioClip>("Sounds/life") },
			{ SoundFX.dizzy, Resources.Load<AudioClip>("Sounds/dizzy") },
			{ SoundFX.wronghome, Resources.Load<AudioClip>("Sounds/wronghome") },
		};

		yield return new WaitForSeconds (1);
	}

	public enum SoundFX
	{
		None,
		PressBtn,
		bite_hit,
		WaterSplash,
		Stun,
		RightHome,
		Gps,
		fence,
		life,
		dizzy,
		wronghome
	}

	public void PlayClip(SoundFX soundFX,Vector3 worldPos)
	{
		AudioClip clip;
		if (clips.TryGetValue (soundFX, out clip)) {
			
			AudioSource_RG audio = ObjectPool.instance.GetAudioSource ();
			audio.audioSource.clip = clip;

			if (isOnSound == "t") {
				audio.audioSource.volume = 0.5f;
			} else if (isOnSound == "f"){
				audio.audioSource.volume = 0;
			}
		
			audio.transform.position = worldPos;
			audio.Live ();

		}
	}
}
