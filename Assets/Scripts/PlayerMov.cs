using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMov : MonoBehaviour
{
    public Laser laserPrefab;
    public float speed = 5.0f;
    private bool laserActive;
    public AudioSource audioSource;
    public AudioClip laserSound;
    public ParticleSystem explosionEffectPrefab;
    private ParticleSystem explosionEffectInstance;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (!laserActive)
        {
            audioSource.PlayOneShot(laserSound);
            Laser laser = Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
            laser.destroyed += LaserDestroy;
            laserActive = true;
        }
    }

    private void LaserDestroy()
    {
        laserActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            explosionEffectInstance = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            explosionEffectInstance.Play();
            Destroy(this.gameObject);
        }
    }
}
