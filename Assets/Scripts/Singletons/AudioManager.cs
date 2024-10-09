using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    [SerializeField] private AudioSource _mainMenu;
    [SerializeField] private AudioSource _lvl1Audio;
    [SerializeField] private AudioSource _lvl2Audio;
    [SerializeField] private AudioSource _lvl3Audio;
    [SerializeField] private AudioSource _lvl4Audio;
    [SerializeField] private AudioSource _endingAudio;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void mainMenu() { _mainMenu.Play(0); }
    public void mainMenuKill() { _mainMenu.Stop(); }
    public void lvl1Audio() { _lvl1Audio.Play(0); }
    public void lvl1AudioKill() { _lvl1Audio.Stop(); }
    public void lvl2Audio() { _lvl2Audio.Play(0); }
    public void lvl2AudioKill() { _lvl2Audio.Stop(); }
    public void lvl3Audio() { _lvl3Audio.Play(0); }
    public void lvl3AudioKill() { _lvl3Audio.Stop(); }
    public void lvl4Audio() { _lvl4Audio.Play(0); }
    public void lvl4AudioKill() { _lvl4Audio.Stop(); }
    public void endingAudio() { _endingAudio.Play(0); }
    public void endingAudioKill() { _endingAudio.Stop(); }


}
