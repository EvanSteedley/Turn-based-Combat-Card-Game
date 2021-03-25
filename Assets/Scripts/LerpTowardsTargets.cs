using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTowardsTargets : MonoBehaviour
{

    public GameObject Target { get; set; }
    public float timeToMove = 0.5f;
    public GameObject particleGO;
    public GameObject spawnedOnReach;
    public ParticleSystem particles;
    public ParticleSystem spawnedOnReachParticles;

    // Start is called before the first frame update
    void Start()
    {
        particles = particleGO.GetComponent<ParticleSystem>();
        spawnedOnReachParticles = spawnedOnReach.GetComponent<ParticleSystem>();
        StartCoroutine(MoveObject(this.gameObject.transform.position, Target.transform.position, timeToMove));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator MoveObject(Vector3 currentPos, Vector3 destinationPos, float timeToMove)
    {
        float elapsedTime = 0f;
        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(currentPos, destinationPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = destinationPos;

        GameObject spawned = Instantiate(spawnedOnReach);
        spawned.transform.position = transform.position;
        //Destroy(this.gameObject, particles.main.startLifetime.constant);
        StartCoroutine(WaitForParticleDespawn(particles.main.startLifetime.constant));
        particles.Stop();
        spawnedOnReachParticles.Play();
        //Debug.Log(spawnedOnReachParticles.main.startLifetime.constant);
        Destroy(spawned, spawnedOnReachParticles.main.startLifetime.constant);
        Destroy(this.gameObject, spawnedOnReachParticles.main.startLifetime.constant);
    }

    public IEnumerator WaitForParticleDespawn(float timeToWait)
    {
        float elapsedTime = 0f;
        while (elapsedTime < timeToWait)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
