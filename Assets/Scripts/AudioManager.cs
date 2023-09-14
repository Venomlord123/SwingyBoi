using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabe;

namespace Gabe
{
    public class AudioManager : MonoBehaviour
    {
        public static GameObject audioManagerObject;

        [Header("Volume Settings")]
        [Range(0f, 1f)]
        public static float masterVolume;

        public static string currentEnvAudio;


        // public static AudioManager instance;


        void Awake()
        {
            if (audioManagerObject != null) 
                Destroy(this.gameObject);
            else
                audioManagerObject = this.gameObject;
        }

        public void Start(){
            //Load volume settings from file
            //        DontDestroyOnLoad(this.gameObject);
        }

        // Update is called once per frame
        public void Update(){
            
        }

        public void SetVolume(string volumeBus, float newVolume){
            // oldVolume = newVolume
            // save this in a settings file also
        }

        public void Play(string audioFileName, bool forceOneInstance = false, GameObject gameObject = null){
            if (gameObject = null)
                gameObject = audioManagerObject;

            
            
            // if (forceOneInstance & IsPlaying(audioFileName){
            //     Debug.Log($"{audioFileName}" is already playing on {gameObject}, will not trigger while forceOneInstance is true);
            //     return;
            // } else {
            //     'system.playsound'(audioFileName);
            // }

        }

        public void Stop(string audioFileName, GameObject gameObject){
            
        }

        public bool IsPlaying(string audioFileName, GameObject gameObject){
            // check gameobject for <thing playing audio>
            // if (gameObject.audioPlayer.isPlaying && gameObject.audioPlayer.fileName == audioFileName){
                // Debug.Log($"{audioFileName} is curently playing on {gameObject}")
                // return true; 
            // }
            return false;
        }


        public void SetAudioEnvironment(string newEnvAudio){
            if (newEnvAudio == currentEnvAudio){
                Debug.Log($"Environment audio state is already {currentEnvAudio}, ignoring");
                return;
            } else {
                Play(newEnvAudio);
            } 
        }
    }
}