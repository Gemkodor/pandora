using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Pandora {

public class LoadingTextAnimation : MonoBehaviour {
    [SerializeField] string _message = "Loading";
    [SerializeField] float _delay = 0.5f;

    void Start() {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText() {
        var text = GetComponent<TextMeshProUGUI>();
        Assert.IsNotNull(text);

        while (true) {
            text.text = _message;
            yield return new WaitForSeconds(_delay);
            text.text = $"{_message}.";
            yield return new WaitForSeconds(_delay);
            text.text = $"{_message}..";
            yield return new WaitForSeconds(_delay);
            text.text = $"{_message}...";
            yield return new WaitForSeconds(_delay);
        }
    }
}

}