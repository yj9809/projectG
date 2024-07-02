using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("#BGM")]
    public AudioClip[] bgmClip;
    public float bgmVolume;
    public int bgmChannels;
    private AudioSource[] bgmPlayer;

    [Header("#SFX")]
    public AudioClip[] sfxClip;
    public float sfxVolume;
    public int sfxChannels;
    private AudioSource[] sfxPlayer;
    private int channelIndex;

    public enum Bgm
    {
        Main,
        Game
    }
    public enum Sfx
    {
        Click,
        Day,
        TimeScale
    }
    protected override void Awake()
    {
        base.Awake();

        // �����
        GameObject bgmObj = new GameObject("BgmPlayer");
        bgmObj.transform.parent = transform;
        bgmPlayer = new AudioSource[bgmChannels];

        for (int i = 0; i < bgmPlayer.Length; i++)
        {
            bgmPlayer[i] = bgmObj.AddComponent<AudioSource>();

            if (bgmClip[i].name == "Main Bgm")
                bgmPlayer[i].playOnAwake = true;
            else
                bgmPlayer[i].playOnAwake = false;

            bgmPlayer[i].loop = true;
            bgmPlayer[i].volume = bgmVolume;
            bgmPlayer[i].clip = bgmClip[i];
        }

        //ȿ����
        GameObject sfxObj = new GameObject("SfxPlayer");
        sfxObj.transform.parent = transform;
        sfxPlayer = new AudioSource[sfxChannels];

        for (int i = 0; i < sfxPlayer.Length; i++)
        {
            sfxPlayer[i] = sfxObj.AddComponent<AudioSource>();
            sfxPlayer[i].playOnAwake = false;
            sfxPlayer[i].volume = sfxVolume;
        }
    }
    public void PlayBgm(Bgm bgm)
    {
        // ��� BGM �÷��̾ �����
        foreach (AudioSource player in bgmPlayer)
        {
            player.Stop();
        }

        // ������ BGM�� ����Ѵ�
        bgmPlayer[(int)bgm].Play();
    }
    public void PlaySfx(Sfx sfx)
    {
        for (int i = 0; i < sfxPlayer.Length; i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayer.Length;

            if (sfxPlayer[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            sfxPlayer[loopIndex].clip = sfxClip[(int)sfx];
            sfxPlayer[loopIndex].Play();
            break;
        }
    }
}
