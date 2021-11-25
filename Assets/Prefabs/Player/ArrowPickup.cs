using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPickup : MonoBehaviour {
    [SerializeField] float _deleteTimeout = 60f;
    [SerializeField] int _amount = 1;

    private void Start() {
        Destroy(this.gameObject, _deleteTimeout);
    }

    private void OnTriggerEnter(Collider other) {
        // TODO: Pickup
        Debug.Log($"Picking up {_amount} arrows");
        Destroy(this.gameObject);
    }
}
