using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabe;

namespace Gabe
{
    public class PlayerAudio : MonoBehaviour
    {
        void Start(){
            
        }

        // void Update(){
            
        // }

        public void ChargeJump(float jumpCharge){
            // intensity = jumpCharge (will allow for dynamic audio)
            // i.e., if the player can shorten their jump charge time with a powerup, 
            // or a hazard like honey can lengthen it.)

            // if !AudioManager.IsPlaying(ChargeJump.wav)
                // Play("ChargeJump.wav");
        }

        public void ReleaseJump(float jumpCharge){
            //filter/volume/whatever = jumpCharge
            //Play("ReleaseJump.wav");
        }

    }
}
