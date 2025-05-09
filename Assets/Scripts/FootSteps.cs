using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public AudioClip[] footstepClip;
    private AudioSource audioSource;
    private Rigidbody _rigidbody;
    public float footstepThreshold;
    public float footstepRate;
    private float lasgFootstepTime;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(Mathf.Abs(_rigidbody.velocity.y)<0.1f)
        {
            if(_rigidbody.velocity.magnitude > footstepThreshold)
            {
                if(Time.time - lasgFootstepTime > footstepRate)
                {
                    lasgFootstepTime = Time.time;
                    audioSource.PlayOneShot(footstepClip[Random.Range(0, footstepClip.Length)]);
                }
            }
        }
    }
}
