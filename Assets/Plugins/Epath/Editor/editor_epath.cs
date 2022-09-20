using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(epath))]
public class editor_epath:Editor {

	protected epath		_script;

	override public void OnInspectorGUI() {

		this._script = target as epath;


		if (GUILayout.Button("identify path")) {

			this.identify_path();
			this.identify_update();
		}

		if (GUILayout.Button("identify name")) {

			this.identify_name();
		}

		if (GUILayout.Button("identify update")) {

			this.identify_update();
		}

		base.OnInspectorGUI();
	}


	void identify_path() {

		Transform[] __transform_array = this._script.GetComponentsInChildren<Transform>();
		List<Transform> __transform_list = new List<Transform>();

		foreach (Transform __transform in __transform_array) {

			if (__transform.parent!=this._script.transform) {

				continue;
			}

			__transform_list.Add(__transform);
		}

		this._script._list = __transform_list.ToArray();
	}

	void identify_name() {

		for (int __index=0;__index<this._script._list.Length;__index++) {

			this._script._list[__index].name = "__path ("+__index+")";
		}

	}

	void identify_update() {

		this._script.identify();
	}

}
