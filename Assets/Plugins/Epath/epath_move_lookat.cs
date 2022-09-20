using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class epath_move_lookat: MonoBehaviour {

	public epath			_epath;

	public float			_data_speed=1;
	public float			_status_time=0;

	public Transform		_transform_target;


	void Start() {

		this.transform.position = this._epath.evaluate_position_time(this._status_time);
	}

	void LateUpdate() {

		this._status_time += this._data_speed * Time.deltaTime;

		this.transform.position = this._epath.evaluate_position_time(this._status_time);

		if (this._transform_target.position== this.transform.position) {

			return;
		}

		this.transform.LookAt(this._transform_target.position,Vector3.up);

	}


}
