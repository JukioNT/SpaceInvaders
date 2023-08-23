using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public System.Action destroyed;
    public ParticleSystem explosionEffectPrefab;
    private ParticleSystem explosionEffectInstance;
    public AudioSource audioSourcePrefab;
    private AudioSource audioSourceInstance;
    public AudioClip explosionSoundEffect;

    private void FixedUpdate()
    {
        this.transform.position += this.direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(destroyed != null)
        {
            this.destroyed.Invoke();
        }
        if (this.gameObject.layer == LayerMask.NameToLayer("Missile") && other.gameObject.layer != LayerMask.NameToLayer("Border"))
        {
            audioSourceInstance = Instantiate(audioSourcePrefab, transform.position, Quaternion.identity);
            audioSourceInstance.PlayOneShot(explosionSoundEffect, 0.5f);
            explosionEffectInstance = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            explosionEffectInstance.Play();
        }
        Destroy(this.gameObject);
    }
}
