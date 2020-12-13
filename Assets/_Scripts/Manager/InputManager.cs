using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NDRChopper
{
    public class InputManager : MonoBehaviour
    {
        public static float MovementX, MovementY;
        public static bool Fire, Flare, MissileChange, ElevateUp, ElevateDown;

        private void Update()
        {
            MovementX = Input.GetAxis("Horizontal");
            MovementY = Input.GetAxis("Vertical");
            Fire = Input.GetButton("Fire1");
            ElevateDown = Input.GetButton("ElevateDown");
            ElevateUp = Input.GetButton("ElevateUp");
        }
    }
}