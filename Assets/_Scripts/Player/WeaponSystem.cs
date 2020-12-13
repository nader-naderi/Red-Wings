using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NDRChopper
{
    public class WeaponSystem : MonoBehaviour
    {
        [SerializeField]
        weap[] weapons;

        [SerializeField] GameObject bulletResource;
        [SerializeField] float bulletsPerSecond = 15f;
        protected float fireAngle = 2f;
        protected float fireTime;
        [SerializeField] GameObject homingMissileResource;
        [SerializeField] float homingMissilesPerSecond = 2f;
        [SerializeField] GameObject leftHomingMissileLauncher;
        [SerializeField] GameObject leftMachineGun;
        [SerializeField] GameObject leftMissileLauncher;
        [SerializeField] GameObject missileResource;
        [SerializeField] float missilesPerSecond = 2f;
        [SerializeField] float numOfHomingMissiles;
        [SerializeField] float numOfMissiles;
        [SerializeField] GameObject rightHomingMissileLauncher;
        [SerializeField] GameObject rightMachineGun;
        [SerializeField] GameObject rightMissileLauncher;
        [SerializeField] int weapon;
        [SerializeField] bool weaponLeft = true;
        protected GameObject defaultTarget;
        [SerializeField] GameObject target;
        public GameObject Target { get { return target; } }
        public int Weapon
        {
            get
            {
                return weapon;
            }
            set
            {
                weapon = value;
            }
        }

        private void Start()
        {

        }

        private void Update()
        {
            GameObject obj5;
            switch (weapon)
            {
                case 0:
                    MachineGunController();
                    break;
                case 1:
                    leftMachineGun.GetComponentInChildren<ParticleSystem>().Stop();
                    rightMachineGun.GetComponentInChildren<ParticleSystem>().Stop();
                    if (!InputManager.Fire || (numOfMissiles <= 0f))
                    {
                        break;
                    }
                    fireTime += Time.deltaTime;
                    if (fireTime < (1f / missilesPerSecond))
                    {
                        break;
                    }
                    numOfMissiles--;
                    fireTime -= 1f / missilesPerSecond;
                    weaponLeft = !weaponLeft;
                    obj5 = UnityEngine.Object.Instantiate(missileResource) as GameObject;
                    obj5.layer = LayerMask.NameToLayer("AllyBullets");
                    if (!weaponLeft)
                    {
                        obj5.transform.position = rightMissileLauncher.transform.position;
                        obj5.transform.rotation = rightMissileLauncher.transform.rotation;
                        break;
                    }
                    obj5.transform.position = leftMissileLauncher.transform.position;
                    obj5.transform.rotation = leftMissileLauncher.transform.rotation;
                    break;

                case 2:
                    leftMachineGun.GetComponentInChildren<ParticleSystem>().Stop();
                    rightMachineGun.GetComponentInChildren<ParticleSystem>().Stop();
                    if (InputManager.Fire && (numOfHomingMissiles > 0f))
                    {
                        fireTime += Time.deltaTime;
                        if (fireTime >= (1f / homingMissilesPerSecond))
                        {
                            numOfHomingMissiles--;
                            fireTime -= 1f / homingMissilesPerSecond;
                            weaponLeft = !weaponLeft;
                            GameObject obj6 = UnityEngine.Object.Instantiate(homingMissileResource) as GameObject;
                            obj6.GetComponent<HomingMissile>().Target = target;
                            obj6.layer = LayerMask.NameToLayer("AllyBullets");
                            if (!weaponLeft)
                            {
                                obj6.transform.position = rightHomingMissileLauncher.transform.position;
                            }
                            else
                            {
                                obj6.transform.position = leftHomingMissileLauncher.transform.position;
                            }
                            obj6.transform.rotation = transform.rotation;
                        }
                    }
                    //goto Label_182E;
                    break;
                default:
                    break;
                   // goto Label_182E;
            }
       // Label_182E:
            
        }

        private void MachineGunController()
        {
            if (!InputManager.Fire)
            {
                leftMachineGun.GetComponentInChildren<ParticleSystem>().Stop();
                rightMachineGun.GetComponentInChildren<ParticleSystem>().Stop();
            }
            else
            {
                if (!leftMachineGun.GetComponentInChildren<ParticleSystem>().isPlaying)
                {
                    leftMachineGun.GetComponentInChildren<ParticleSystem>().Play();
                }
                if (!rightMachineGun.GetComponentInChildren<ParticleSystem>().isPlaying)
                {
                    rightMachineGun.GetComponentInChildren<ParticleSystem>().Play();
                }
                fireTime += Time.deltaTime;
                if (fireTime >= (1f / bulletsPerSecond))
                {
                    fireTime -= 1f / bulletsPerSecond;
                    weaponLeft = !weaponLeft;
                    GameObject obj4 = UnityEngine.Object.Instantiate(bulletResource) as GameObject;
                    obj4.layer = LayerMask.NameToLayer("AllyBullets");
                    if (weaponLeft)
                    {
                        obj4.transform.position = leftMachineGun.transform.position;
                    }
                    else
                    {
                        obj4.transform.position = rightMachineGun.transform.position;
                    }
                    if (target != null)
                    {
                        obj4.transform.LookAt(target.transform.position);
                    }
                    else
                    {
                        BulletSpread(obj4);
                    }
                    obj4.transform.eulerAngles = new Vector3(obj4.transform.eulerAngles.x - UnityEngine.Random.Range(-fireAngle, fireAngle), obj4.transform.eulerAngles.y + UnityEngine.Random.Range(-fireAngle, fireAngle), obj4.transform.eulerAngles.z);
                    //obj4.transform.Translate((Vector3)(obj4.transform.forward * 10f), Space.World);
                }
            }
        }

        private void BulletSpread(GameObject obj4)
        {
            obj4.transform.eulerAngles = new Vector3(5f, transform.eulerAngles.y + UnityEngine.Random.Range(-fireAngle, fireAngle), 0f);
        }
    }
    [System.Serializable]
    class weap
    {
        [SerializeField]
        string name;
        [SerializeField]
        GameObject prefab;
        [SerializeField]
        GameObject[] leftRight;
        [SerializeField]
        float fireAngle = 2f;
        float fireTime;
        protected float projectilesPerSecond = 15f;
        private bool weaponLeft;

    }
}