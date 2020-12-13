using System;
using UnityEngine;

namespace NDRChopper
{
    public static class Globals
    {
        public static float ALERT_RANGE = 300f;
        public static string FAILED_MISSION = "MainMenu";
        public static float HELICOPTER_Y = 60f;
        public static int levelUnlocked = 1;
        private static bool muted;
        public static float UNALERT_RANGE = 900f;

        public static bool getMute()
        {
            return muted;
        }

        public static float getVolume()
        {
            return (!muted ? ((float)1) : ((float)0));
        }

        public static void setMute(bool mute)
        {
            float num = !muted ? ((float)0) : ((float)1);
            if (muted != mute)
            {
                foreach (AudioSource source in UnityEngine.Object.FindObjectsOfType(typeof(AudioSource)))
                {
                    source.volume = num;
                }
            }
            muted = mute;
        }
    }

}