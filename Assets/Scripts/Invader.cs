using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public Sprite[] animationSprites;
    public float animationTime = 1.0f;
    private SpriteRenderer spriteRenderer;
    private int animationFrame;
    public System.Action killed;
    public ParticleSystem explosionEffectPrefab;
    private ParticleSystem explosionEffectInstance;
    public AudioSource audioSourcePrefab;
    private AudioSource audioSourceInstance;
    public AudioClip explosionSoundEffect;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
    }

    private void AnimateSprite()
    {
        animationFrame++;

        if(animationFrame >= this.animationSprites.Length)
        {
            animationFrame = 0;
        }

        spriteRenderer.sprite = this.animationSprites[animationFrame];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            explosionEffectInstance = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            audioSourceInstance = Instantiate(audioSourcePrefab, transform.position, Quaternion.identity);
            explosionEffectInstance.Play();
            audioSourceInstance.PlayOneShot(explosionSoundEffect, 0.5f);
            this.killed.Invoke();
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
