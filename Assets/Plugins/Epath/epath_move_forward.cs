using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class epath_move_forward: MonoBehaviour {

	public epath			_epath;

	public float			_data_speed=1;
	public float			_data_look_offset=0.01f;
	public float			_status_time=0;

	void Start() {

		this.transform.position = this._epath.evaluate_position_time(this._status_time);
	}

	void Update() {

		this._status_time += this._data_speed * Time.deltaTime;

		this.transform.position = this._epath.evaluate_position_time(this._status_time);

		Vector3 __position = this._epath.evaluate_position_time(this._status_time + this._data_look_offset);

		if (__position==this.transform.position) {

			return;
		}

		this.transform.rotation = Quaternion.LookRotation(__position - this.transform.position);

	}


}
