using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NDRChopper
{
    public class HelicopterController : MonoBehaviour
    {
        protected float angleAmplitude = 2f;
        protected float angleFrequency = 0.7853982f;
        protected float angleOffset;

        protected float heightAmplitude = 0.01f;
        protected float heightFrequency = 2.513274f;
        protected float heightOffset;

        protected Vector3 rotation = new Vector3(0f, 0f, 0f);
        protected Vector3 rotationAcceleration = new Vector3(50f, 50f, 20f);
        protected Vector3 rotationDeceleration = new Vector3(30f, 50f, 30f);
        protected Vector3 rotationLimit = new Vector3(20f, 0f, 20f);
        [SerializeField]
        protected GameObject mainRotor;
        [SerializeField]
        protected GameObject tailRotor;
        protected Vector3 movement = new Vector3(0f, 0f, 0f);
        protected Vector2 movementAcceleration = new Vector2(40f, 20f);
        [SerializeField]
        protected Vector2 movementLimit = new Vector2(99999f, 99999f);
        protected Vector3 movementDeceleration = new Vector3(100f, 40f);
        [SerializeField]
        protected AudioSource helicopterEngine;
        protected float yPosition;
        bool helipadControl;
        protected int numberOfPassengers;

        private void Start()
        {
            //rotation = transform.eulerAngles;
            //transform.position = new Vector3(transform.position.x, Globals.HELICOPTER_Y, transform.position.z);
            //yPosition = transform.position.y;
        }

        private void Update()
        {
            Vector2 vector;

            float num3 = 600f;
            float num4 = 600f;
            if (InputManager.MovementX < 0)
            {
                rotation.z = Ease(rotationAcceleration.z * Time.deltaTime, rotation.z, rotationLimit.z);
                rotation.y -= rotationAcceleration.y * Time.deltaTime;
                num4 *= 2;
                helicopterEngine.pitch -= 0.5f * Time.deltaTime;
            }
            else if(InputManager.MovementX > 0)
            {
                rotation.z = Ease(rotationAcceleration.z * Time.deltaTime, rotation.z, -rotationLimit.z);
                rotation.y += rotationAcceleration.y * Time.deltaTime;
                num4 /= 2f;
                helicopterEngine.pitch += 0.5f * Time.deltaTime;
            }
            
            if(InputManager.MovementY > 0)
            {
                rotation.x = Ease(rotationAcceleration.x * Time.deltaTime, rotation.x, rotationLimit.x);
                movement.x = Ease(movementAcceleration.x * Time.deltaTime, movement.x, movementLimit.x);
                helicopterEngine.pitch += 0.5f * Time.deltaTime;
            }
            else if (InputManager.MovementY < 0)
            {
                rotation.x = Ease(rotationAcceleration.x * Time.deltaTime, rotation.x, -rotationLimit.x);
                movement.x = Ease(movementAcceleration.x * Time.deltaTime, movement.x, -movementLimit.x);
                helicopterEngine.pitch -= 0.5f * Time.deltaTime;
            }
            else
            {
                rotation.x = Ease(rotationDeceleration.x * Time.deltaTime, rotation.x, 0f);
                movement.x = Ease(movementDeceleration.x * Time.deltaTime, movement.x, 0f);
                helicopterEngine.pitch = 1;
            }

            if (Input.GetKey(KeyCode.Q))
            {
                movement.y = Ease(movementAcceleration.y * Time.deltaTime, movement.y, -movementLimit.y);
                rotation.z = Ease(rotationAcceleration.z * Time.deltaTime, rotation.z, rotationLimit.z);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                movement.y = Ease(movementAcceleration.y * Time.deltaTime, movement.y, movementLimit.y);
                rotation.z = Ease(rotationAcceleration.z * Time.deltaTime, rotation.z, -rotationLimit.z);
            }
            else
            {
                movement.y = Ease(movementDeceleration.y * Time.deltaTime, movement.y, 0f);
            }
            if (InputManager.ElevateUp)
            {
                movement.z = Ease(movementDeceleration.z * Time.deltaTime, movement.z, 20f);
            }
            else if (InputManager.ElevateDown)
            {
                movement.z = Ease(movementDeceleration.z * Time.deltaTime, movement.z, -20f);
            }

            // transform.Translate(0, InputManager.MovementY * 2 * Time.deltaTime, 0);

            if (((InputManager.MovementX.Equals(0))) && (!Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q)))
            {
                rotation.z = Ease(rotationDeceleration.z * Time.deltaTime, rotation.z, 0f);
            }

            helicopterEngine.pitch = Mathf.Clamp(helicopterEngine.pitch, 1, 1.6f);

            transform.eulerAngles = rotation;
            transform.Translate((movement.x * Time.deltaTime / 40) * -Mathf.Cos((0.01745329f * rotation.y) + 1.570796f), 0f, 
                (movement.x * Time.deltaTime / 40) * Mathf.Sin((0.01745329f * rotation.y) + 1.570796f), Space.World);

            transform.Translate((movement.y * Time.deltaTime / 40) * -Mathf.Cos((0.01745329f * rotation.y) + 3.141593f), 0f,
                (movement.y * Time.deltaTime / 40) * Mathf.Sin((0.01745329f * rotation.y) + 3.141593f), Space.World);

            

            vector.x = 0;
            vector.y = 0;
            //vector = new Vector2(transform.position.x, transform.position.z)
            //{
            //    x = Mathf.Min(Mathf.Max(vector.x, mission.Boundaries.xMin), mission.Boundaries.xMax),
            //    y = Mathf.Min(Mathf.Max(vector.y, mission.Boundaries.yMin), mission.Boundaries.yMax)
            //};
            //transform.position = new Vector3(vector.x, transform.position.y, vector.y);

            mainRotor.transform.Rotate((float)0f, (float)0f, (num3 + (Mathf.Abs(movement.x) * 5f)) * Time.deltaTime);
            tailRotor.transform.Rotate(0f, (float)(num4 * Time.deltaTime), (float)0f);

            //if (!helipadControl)
            //{
            //    if (Time.timeScale != 0f)
            //    {
            //        heightOffset += heightFrequency * Time.deltaTime;
            //        transform.Translate(0f, heightAmplitude * Mathf.Sin(heightOffset), 0f);
            //    }
            //    angleOffset += angleFrequency * Time.deltaTime;
            //    transform.Rotate((float)0f, angleAmplitude * Mathf.Sin(angleOffset), (float)0f);
            //}



            if ((Time.timeScale == 0f) && helicopterEngine.isPlaying)
            {
                helicopterEngine.Stop();
                //backgroundWar.Stop();
            }
            else if ((Time.timeScale > 0f) && !helicopterEngine.isPlaying)
            {
                helicopterEngine.volume = Globals.getVolume();
                helicopterEngine.Play();
            }
        }

        public float Ease(float step, float v, float to)
        {
            return ((v <= to) ? Mathf.Min(v + step, to) : Mathf.Max(v - step, to));
        }
    }
}