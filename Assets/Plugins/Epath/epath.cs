using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class epath: MonoBehaviour {

	public Transform[]				_list=new Transform[0];
	public List<float>				_list_distance=new List<float>();

	public float					_distance=0;

	public int						_resolution=12;

	public bool						_loop = true;
	public bool						_clamp = true;
	protected bool					_valid = false;

	public bool						_gizmos=true;

	public float clamp_distance(float __distance) {

		if (this._loop) {

			__distance = __distance % this._distance;

			if (__distance < 0) {

				__distance = 1 - Mathf.Abs(__distance);


			}

		} else {

			if (this._clamp) {

				__distance = __distance % this._distance;

				if (__distance < 0) {

					__distance = this._distance - Mathf.Abs(__distance);
				}

			} else {

				__distance = Mathf.Clamp(__distance,0,this._distance);
			}

		}


		return
			__distance;
	}

	public float clamp_time(float __time) {

		if (this._loop) {

			__time = __time % 1;

			if (__time < 0) {

				__time = 1 - Mathf.Abs(__time);
			}

		} else {

			if (this._clamp) {

				__time = __time = __time % 1;

				if (__time < 0) {

					__time = 1 - Mathf.Abs(__time);
				}

			} else {

				__time = Mathf.Clamp01(__time);
			}

			
		}


		return
			__time;
	}


	public Vector3 evaluate_position_distance(float __distance) {

		return
			this.evaluate_position_time(this.clamp_distance(__distance) / this._distance );
	}

	public Vector3 evaluate_position_time(float __time) {

		__time = this.clamp_time(__time);

		float __distance = this._distance * __time;

		float __time_distance = this.execute_time_distance(__distance);
		int __step_distance = this.execute_step_distance(__distance);

		Vector3 __position = this.catmull_rom(__time_distance,__step_distance);

		return
			__position;
	}

	float execute_time_distance(float __distance) {

		float __distance_length = 0;

		for (int __i = 0; __i < this._list_distance.Count; __i++) {

			__distance_length += this._list_distance[__i];

			if (__distance_length >= __distance) {

				float __d = __distance_length - __distance;

				return
					Mathf.InverseLerp(this._list_distance[__i],0,__d);
			}
		}

		return
			0;
	}

	int execute_step_distance(float __distance) {

		float __distance_length = 0;

		for (int __i=0;__i<this._list_distance.Count;__i++) {

			__distance_length += this._list_distance[__i];

			if (__distance_length >= __distance) {

				return
					__i;
			}
		}

		return
			0;
	}


	public void identify() {

		this._valid = false;

		if (this._list == null) {

			return;
		}

		if (this._list.Length<4) {

			return;
		}

		foreach (Transform __transform in this._list) {

			if (__transform==null) {

				return;
			}
		}

		this._valid = true;

		this.identify_resolution();
		this.identify_distance();
	}

	void identify_resolution() {

		this._resolution = Mathf.Max(2,this._resolution);
	}

	void identify_distance() {

		this._distance = 0;

		this._list_distance.Clear();

		Vector3 __position_current = this._list[0].position;

		for (int __i = 0; __i < this._list.Length; __i++) {

			this._list_distance.Add(0);

			//Vector3 __p0 = this._list[this.identify_index(__i - 1)].position;
			//Vector3 __p1 = this._list[this.identify_index(__i)].position;
			//Vector3 __p2 = this._list[this.identify_index(__i + 1)].position;
			//Vector3 __p3 = this._list[this.identify_index(__i + 2)].position;

			for (int __r = 0; __r < this._resolution; __r++) {

				//Vector3 __position_next = this.catmull_rom((float)__r / (float)(this._resolution - 1),__p0,__p1,__p2,__p3);
				Vector3 __position_next = this.catmull_rom((float)__r / (float)(this._resolution - 1),__i);
				this._list_distance[__i] += Vector3.Distance(__position_current,__position_next);

				__position_current = __position_next;
			}

			this._distance += this._list_distance[__i];

			if (this._loop == false) {

				if (__i == this._list.Length - 2) {

					break;
				}
			}
		}

	}

#if UNITY_EDITOR
	void OnDrawGizmos() {

		if (this._gizmos==false) {

			return;
		}

		this.identify();

		if (this._valid==false) {

			return;
		}

		Gizmos.color = Color.white;

		this._distance = 0;

		this._list_distance.Clear();

		Vector3 __position_current = this._list[0].position;

		for (int __i = 0; __i < this._list.Length; __i++) {

			this._list_distance.Add(0);

			for (int __r = 0; __r < this._resolution; __r++) {

				Vector3 __position_next = this.catmull_rom((float)__r / (float)(this._resolution - 1),__i);

				Gizmos.DrawLine(__position_current,__position_next);

				this._list_distance[__i] += Vector3.Distance(__position_current,__position_next);

				__position_current = __position_next;
			}

			this._distance += this._list_distance[__i];

			if (this._loop == false) {

				if (__i == this._list.Length - 2) {

					break;
				}
			}
		}
	}
#endif

	int identify_index(int __index) {

		if (__index < 0) {

			if (this._loop) {

				return
					this._list.Length - 1;
			}

			return
				0;
		}

		if (__index == this._list.Length) {

			if (this._loop) {

				return
					0;
			}

			return
				__index-1;
		}

		if (__index > this._list.Length) {

			if (this._loop) {

				return
					__index - this._list.Length;
			}

			return
				0;
		}
		
		return
			__index;
	}

	Vector3 catmull_rom(float __time,int __index) {

		Vector3 __p0 = this._list[this.identify_index(__index - 1)].position;
		Vector3 __p1 = this._list[this.identify_index(__index)].position;
		Vector3 __p2 = this._list[this.identify_index(__index + 1)].position;
		Vector3 __p3 = this._list[this.identify_index(__index + 2)].position;

		return
			this.catmull_rom(__time,__p0,__p1,__p2,__p3);
	}

	//Returns a position between 4 Vector3 with Catmull-Rom spline algorithm
	Vector3 catmull_rom(float __time,Vector3 __p0,Vector3 __p1,Vector3 __p2,Vector3 __p3) {
		
		Vector3 __a = 2f * __p1;
		Vector3 __b = __p2 - __p0;
		Vector3 __c = 2f * __p0 - 5f * __p1 + 4f * __p2 - __p3;
		Vector3 __d = -__p0 + 3f * __p1 - 3f * __p2 + __p3;

		Vector3 __position = 0.5f * (__a + (__b * __time) + (__c * __time * __time) + (__d * __time * __time * __time));
		
		return
			__position;
	}
}
