using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;


	/*---------------------------------------------------------
		AudioClip - �ϳ��� ���� �ҽ�
		AudioSource - AudioClip�� �����ϴ� ������Ʈ(����, ����)
		
	 --------------------------------------------------------*/
	// �����
	[Header("# BGM")]
	public AudioClip bgmClip;
	public float bgmVolum;
	AudioSource bgmPlayer;
	AudioHighPassFilter bgmHighPassFilter;

	// ȿ����
	[Header("# SFX")]
	public AudioClip[] sfxClips;
	public float sfxVolum;
	public int channels;
	AudioSource[] sfxPlayers;
	int channelIndex;

	public enum Sfx
	{
		Dead,
		Hit,
		LevelUp = 3,
		Lose,
		Melee,
		Range = 7,
		Select,
		Win
	}


	private void Awake()
	{
		instance = this;
		Init();
	}

	void Init()
	{
		// ����� �ÿ��̾� �ʱ�ȭ
		GameObject bgmObject = new GameObject("BgmPlayer");
		bgmObject.transform.parent = transform;
		bgmHighPassFilter = Camera.main.GetComponent<AudioHighPassFilter>();
		// AddComponent - ������Ʈ �����Լ� AudioSource ��ȯ
		bgmPlayer = bgmObject.AddComponent<AudioSource>();
		bgmPlayer.playOnAwake = false;
		bgmPlayer.loop = true;
		bgmPlayer.volume = bgmVolum;
		bgmPlayer.clip = bgmClip;

		// ȿ���� �ÿ��̾� �ʱ�ȭ
		GameObject sfxObject = new GameObject("sfxPlayer");
		sfxObject.transform.parent = transform;
		sfxPlayers = new AudioSource[channels];

		for(int i =0;i< channels; i++)
		{
			sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
			sfxPlayers[i].playOnAwake = false;
			sfxPlayers[i].volume = sfxVolum;
			sfxPlayers[i].bypassListenerEffects = true;

		}

	}

	// ȿ���� ���
	public void SfxPlay(Sfx sfx)
	{
		for(int i =0;i< channels;i++)
		{
			int loopIndex = (i + channelIndex) % sfxPlayers.Length;

			if (sfxPlayers[loopIndex].isPlaying) { continue; }

			int rand = 0;
			if(sfx == Sfx.Melee || sfx == Sfx.Hit)
				rand = Random.Range(0, 2);

			channelIndex = loopIndex;
			sfxPlayers[loopIndex].clip = sfxClips[(int)sfx + rand];
			sfxPlayers[loopIndex].Play();
			break;
		}
	}

	// ����� ���
	public void BgmPlay(bool isPlay)
	{
		if(isPlay)
		{
			bgmPlayer.Play();
		}
		else
		{
			bgmPlayer.Stop();
		}
	}

	public void EffectBgm(bool isPlay)
	{
		bgmHighPassFilter.enabled = isPlay;
	}
}
