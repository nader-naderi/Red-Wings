using System;
using UnityEngine;

namespace NDRChopper
{
    public class Weapon : MonoBehaviour
    {
        public float damage;
        protected float deathTime;
        [SerializeField]
        protected float speed;
        protected float timer;

        public virtual void OnCollisionEnter(Collision collision)
        {
            Destroy(base.gameObject);
            base.GetComponent<BoxCollider>().enabled = false;
            HealthSystem component = collision.gameObject.GetComponent<HealthSystem>();
            if ((component != null) && component.hit(this.damage))
            {
                Destroy(collision.gameObject);
                GameObject obj2 = Instantiate(component.ExplosionResource) as GameObject;
                obj2.transform.position = component.transform.position;
                if (component.PowerUpResource != null)
                {
                    GameObject obj3 = Instantiate(component.PowerUpResource) as GameObject;
                    obj3.transform.position = new Vector3(component.transform.position.x, Globals.HELICOPTER_Y, component.transform.position.z);
                }
            }
        }

        public virtual void OnTriggerEnter(Collider collider)
        {
            base.GetComponent<BoxCollider>().enabled = false;
            HealthSystem component = collider.gameObject.GetComponent<HealthSystem>();
            if ((component != null) && component.hit(this.damage))
            {
                UnityEngine.Object.Destroy(collider.gameObject);
                GameObject obj2 = UnityEngine.Object.Instantiate(component.ExplosionResource) as GameObject;
                obj2.transform.position = component.transform.position;
                if (component.PowerUpResource != null)
                {
                    GameObject obj3 = UnityEngine.Object.Instantiate(component.PowerUpResource) as GameObject;
                    obj3.transform.position = new Vector3(component.transform.position.x, Globals.HELICOPTER_Y, component.transform.position.z);
                }
            }
        }

        public virtual void Start()
        {
            this.timer = this.deathTime;
            if (base.GetComponents<AudioSource>().Length > 0)
            {
                int index = Mathf.FloorToInt(UnityEngine.Random.value * base.GetComponents<AudioSource>().Length);
                AudioSource source = base.GetComponents<AudioSource>()[index];
                if (Globals.getMute())
                {
                    source.volume = 0f;
                }
                source.Play();
            }
        }

        public virtual void Update()
        {
            base.transform.Translate((Vector3)((base.transform.forward * this.speed) * Time.deltaTime), Space.World);
            if ((base.transform.position.y < -10f) || (base.transform.position.y > 150f))
            {
                UnityEngine.Object.Destroy(base.gameObject);
            }
        }

        public float Damage
        {
            get
            {
                return this.damage;
            }
            set
            {
                this.damage = value;
            }
        }
    }

}