using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSpace : BoardSpace {
    [Tooltip("Prefab of coin object.")]
    public GameObject coinPrefab;
    
    public override void setup() {
        this.blueChance = 0;
    }

    public override IEnumerator land(Player p) {
        doneLanding = false;
        GameObject[] coins = new GameObject[] {
            Instantiate(coinPrefab, new Vector3(transform.position.x + ((float) Random.Range(-20, 20) * 0.005f), transform.position.y + 2.1f, transform.position.z + ((float) Random.Range(-20, 20) * 0.005f)), Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))),
            Instantiate(coinPrefab, new Vector3(transform.position.x + ((float) Random.Range(-20, 20) * 0.005f), transform.position.y + 2.1f, transform.position.z + ((float) Random.Range(-20, 20) * 0.005f)), Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))),
            Instantiate(coinPrefab, new Vector3(transform.position.x + ((float) Random.Range(-20, 20) * 0.005f), transform.position.y + 2.1f, transform.position.z + ((float) Random.Range(-20, 20) * 0.005f)), Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))),
        };
        foreach (GameObject c in coins) {
            c.GetComponent<Coin>().MakeUncollectible();
            c.GetComponent<Rigidbody>().AddForce((c.transform.position - p.transform.position) + Vector3.up, ForceMode.Impulse);
        }
        yield return new WaitForSeconds(0.8f);
        p.state.changeCoins(-3);
        foreach (GameObject c in coins) {
            Destroy(c);
        }
        doneLanding = true;
    }
}
