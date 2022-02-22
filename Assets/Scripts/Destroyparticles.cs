using UnityEngine;
using System.Collections;

public class Destroyparticles : MonoBehaviour {
	private ParticleSystem theParticleSystem;

	// Use this for initialization
	void Start () {
		theParticleSystem = GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (theParticleSystem.isPlaying) {
			return;
		}
		Destroy(gameObject);
	}
	void OnBecomeInvisible(){
		Destroy(gameObject);
	}
}
