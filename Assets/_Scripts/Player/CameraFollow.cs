using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NDRChopper
{
    public class CameraFollow : MonoBehaviour
    {
        public float distance = 65f;
        public float height = 50f;
        public float lag = 20f;
        public float lookAtHeight = 15f;
        public float maxDamage = 50f;
        public float maxMagnitude = 10f;
        public float maxShake = 10f;
        public float minMagnitude = -10f;
        public float shake;
        public GameObject target;

        public void Shake(float damage)
        {
            this.shake = (Mathf.Min(damage, this.maxDamage) / this.maxDamage) * this.maxShake;
        }

        private void Start()
        {
            base.transform.position = new Vector3(this.target.transform.position.x - (this.distance * Mathf.Sin(0.01745329f * this.target.transform.eulerAngles.y)), this.target.transform.position.y + this.height, this.target.transform.position.z - (this.distance * Mathf.Cos(0.01745329f * this.target.transform.eulerAngles.y)));
            base.transform.LookAt(new Vector3(this.target.transform.position.x, this.target.transform.position.y + this.lookAtHeight, this.target.transform.position.z));
        }

        private void Update()
        {
            if (this.target != null)
            {
                if (Time.timeScale == 0f)
                {
                    base.transform.RotateAround(this.target.transform.position, Vector3.up, 0.2f);
                }
                else
                {
                    Vector3 vector = new Vector3(this.target.transform.position.x - (this.distance * Mathf.Sin(0.01745329f * this.target.transform.eulerAngles.y)), this.target.transform.position.y + this.height, this.target.transform.position.z - (this.distance * Mathf.Cos(0.01745329f * this.target.transform.eulerAngles.y)));
                    Vector3 vector2 = vector - base.transform.position;
                    if (this.maxMagnitude <= vector2.magnitude)
                    {
                        Transform transform1 = base.transform;
                        transform1.position += (Vector3)(vector2 * (1f - (this.maxMagnitude / vector2.magnitude)));
                    }
                    else
                    {
                        Transform transform2 = base.transform;
                        transform2.position += (Vector3)((vector - base.transform.position) / this.lag);
                    }
                    base.transform.LookAt(new Vector3(this.target.transform.position.x, this.target.transform.position.y + this.lookAtHeight, this.target.transform.position.z));
                }
            }
            Transform transform = base.transform;
            transform.localPosition += (Vector3)(UnityEngine.Random.insideUnitSphere * this.shake);
            this.shake /= 1.1f;
            if (this.shake < 0.1)
            {
                this.shake = 0f;
            }
        }
    }
}