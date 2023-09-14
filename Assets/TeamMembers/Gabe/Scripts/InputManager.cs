using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabe;

namespace Gabe
{
    public class InputManager : MonoBehaviour
    {
        public static int jumpButton = 1;
        public static int tongueButton = 0;
        public static Dictionary<string, int> userSettings = new Dictionary<string, int>
        {
            {"jumpButton", 1},
            {"tongueButton", 0},
        };
        
        private void Awake(){
            // jumpButton = ReadSetting("jumpButton");
            // tongueButton = ReadSetting("tongueButton");
        }

        void Start(){
            
        }

        // Update is called once per frame
        void Update(){
            
        }

        public int ReadSetting(string settingToRead){
            // parse the settings file for a setting with the name {settingToRead}
            // return the value for that key
            // for now I might just use a dict
            return userSettings[settingToRead];
        }
    }
}