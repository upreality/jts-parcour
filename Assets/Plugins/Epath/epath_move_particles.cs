using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class epath_move_particles: MonoBehaviour {

	public epath			_epath;

	private ParticleSystem					_particle;
	private ParticleSystem.Particle[]		_particles;


	void Awake() {

		this._particle = this.GetComponent<ParticleSystem>();
	}

	void LateUpdate() {

		this._particles = new ParticleSystem.Particle[this._particle.main.maxParticles];

		int __count = this._particle.GetParticles(this._particles);

		for (int __index = 0; __index < __count; __index++) {

			this._particles[__index].position = this.transform.InverseTransformPoint(this._epath.evaluate_position_time((1 - (this._particles[__index].remainingLifetime / this._particles[__index].startLifetime))));
		}

		this._particle.SetParticles(this._particles,__count);

	}


}
