using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    AudioSource bgmSource;
    // Map BGM
    public AudioClip audioDwarfCityUnder;
    
    
    
    private void Awake() {
        bgmSource = GetComponent<AudioSource>();
    }
    // BGM
    public void PlayBGM(string map){
        switch  (map){
            case "DwarfCityUnder":
                bgmSource.clip = audioDwarfCityUnder;
                break;
        }
        bgmSource.Play();
    }
    public void StopBGM(){
        bgmSource.Stop();
    }
}
