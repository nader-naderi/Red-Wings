using System;
using UnityEngine;

namespace NDRChopper
{
    [RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(BoxCollider))]
    public class Missile : Weapon
    {
        private float aoe;
        public GameObject explosionResource;

        protected virtual void checkAOE(GameObject gameObject)
        {
            if (Vector3.Distance(gameObject.transform.position, base.transform.position) < this.aoe)
            {
                HealthSystem component = gameObject.gameObject.GetComponent<HealthSystem>();
                if (component.hit(base.damage))
                {
                    UnityEngine.Object.Destroy(gameObject.gameObject);
                    GameObject obj2 = UnityEngine.Object.Instantiate(component.ExplosionResource) as GameObject;
                    obj2.transform.position = component.transform.position;
                    if (component.PowerUpResource != null)
                    {
                        GameObject obj3 = UnityEngine.Object.Instantiate(component.PowerUpResource) as GameObject;
                        obj3.transform.position = new Vector3(component.transform.position.x, Globals.HELICOPTER_Y, component.transform.position.z);
                    }
                }
            }
        }

        public override void OnCollisionEnter(Collision collision)
        {
            HealthSystem component = collision.gameObject.GetComponent<HealthSystem>();
            if ((component != null) && component.hit(base.damage))
            {
                UnityEngine.Object.Destroy(collision.gameObject);
                GameObject obj2 = UnityEngine.Object.Instantiate(component.ExplosionResource) as GameObject;
                obj2.transform.position = component.transform.position;
                if (component.PowerUpResource != null)
                {
                    GameObject obj3 = UnityEngine.Object.Instantiate(component.PowerUpResource) as GameObject;
                    obj3.transform.position = new Vector3(component.transform.position.x, Globals.HELICOPTER_Y, component.transform.position.z);
                }
            }
            foreach (GameObject obj4 in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (obj4 != collision.gameObject)
                {
                    this.checkAOE(obj4);
                }
            }
            base.GetComponent<BoxCollider>().enabled = false;
            UnityEngine.Object.Destroy(base.gameObject);
            GameObject obj5 = UnityEngine.Object.Instantiate(this.explosionResource) as GameObject;
            obj5.transform.position = base.gameObject.transform.position;
        }

        public override void Start()
        {
            base.deathTime = 0.01f;
            base.Start();
            //base.speed = 100f;
            this.aoe = 15f;
        }

        public override void Update()
        {
            base.Update();
        }
    }

}