using System;
using UnityEngine;

namespace NDRChopper
{
    public class Bullet : Weapon
    {
        /* public override void OnCollisionEnter(Collision collision)
         {
             base.OnCollisionEnter(collision);
         }*/
        [SerializeField]
        float desiredSpeed = 100f;
        public override void Start()
        {
            base.deathTime = 0.03f;
            base.Start();
            base.speed = desiredSpeed;
        }

        public override void Update()
        {
            base.Update();
        }
    }

}