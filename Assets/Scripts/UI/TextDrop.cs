using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDrop : MonoBehaviour {
    private Text[] drops;
    private Text me;

    // Start is called before the first frame update
    void Start() {
        drops = this.GetComponentsInChildren<Text>();
        me = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        foreach (Text t in drops) {
            t.text = me.text;
        }
    }
}
