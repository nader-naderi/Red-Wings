using System;
using UnityEngine;

namespace NDRChopper
{
    public class HomingMissile : Missile
    {
        protected float rotationSpeed = 2f;
        protected GameObject target;

        public override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);
        }

        public override void Start()
        {
            base.deathTime = 0.01f;
            base.Start();
            //base.speed = 100f;
        }

        public override void Update()
        {
            if (this.target != null)
            {
                base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.LookRotation(this.target.transform.position - base.transform.position), this.rotationSpeed * Time.deltaTime);
            }
            base.Update();
        }

        public GameObject Target
        {
            get
            {
                return this.target;
            }
            set
            {
                this.target = value;
            }
        }
    }

}