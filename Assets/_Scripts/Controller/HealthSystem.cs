using UnityEngine;
using System.Collections;

namespace NDRChopper
{
    public class HealthSystem : MonoBehaviour
    {
        protected float currentHealth;
        public GameObject explosionResource;
        public float health = 100f;
        public GameObject powerUpResource;

        public virtual void heal()
        {
            this.currentHealth = this.health;
        }

        public virtual bool hit(float damage)
        {
            bool flag = this.currentHealth == 0f;
            this.currentHealth -= damage;
            if (this.currentHealth < 0f)
            {
                this.currentHealth = 0f;
            }
            if (base.gameObject.GetComponent<HelicopterController>() != null)
            {
                Camera.main.GetComponent<CameraFollow>().Shake(damage);
            }
            return ((this.currentHealth == 0f) && !flag);
        }

        public virtual void Start()
        {
            this.currentHealth = this.health;
        }

        public virtual void Update()
        {
        }

        public GameObject ExplosionResource
        {
            get
            {
                return this.explosionResource;
            }
            set
            {
                this.explosionResource = value;
            }
        }

        public float Life
        {
            get
            {
                return (this.currentHealth / this.health);
            }
            set
            {
                this.currentHealth = value * this.health;
                if (this.currentHealth < 0f)
                {
                    this.currentHealth = 0f;
                }
                if (this.currentHealth > this.health)
                {
                    this.currentHealth = this.health;
                }
            }
        }

        public GameObject PowerUpResource
        {
            get
            {
                return this.powerUpResource;
            }
            set
            {
                this.powerUpResource = value;
            }
        }
    }
}