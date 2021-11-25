using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDynamic : MonoBehaviour {
    [SerializeField] float _deleteTimeout = 7f;
    [SerializeField] GameObject _pickupPrefab;

    private void Start() {
        Destroy(this.gameObject, _deleteTimeout);
    }

    private void OnCollisionEnter(Collision other) {
        // TODO: Decrease health
        // TODO: Enable particle system
        Instantiate(_pickupPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
