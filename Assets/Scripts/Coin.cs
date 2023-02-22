using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    private bool collectible = true;

    public void MakeUncollectible() {
        collectible = false;
    }

    void OnCollisionEnter(Collision c) {
        if (c.gameObject.GetComponent<CoinCollector>() != null && this.collectible) {
            Destroy(this.GetComponent<Rigidbody>());
            foreach (MeshRenderer r in this.GetComponentsInChildren<MeshRenderer>()) {
                r.enabled = false;
            }
            foreach (ParticleSystem p in this.GetComponentsInChildren<ParticleSystem>()) {
                p.Play();
            }
            foreach (AudioSource a in this.GetComponentsInChildren<AudioSource>()) {
                a.Play();
            }
            Invoke("DelayedDestroy", 1.0f);
        }
    }

    private void DelayedDestroy() {
        Destroy(this.gameObject);
    }
}
