using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSpace : BoardSpace {
    public GameObject coinPrefab;

    public override IEnumerator land(Player p) {
        doneLanding = false;
        Instantiate(coinPrefab, new Vector3(transform.position.x, transform.position.y + 5.0f, transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
        yield return new WaitForSeconds(0.2f);
        Instantiate(coinPrefab, new Vector3(transform.position.x, transform.position.y + 5.0f, transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
        yield return new WaitForSeconds(0.2f);
        Instantiate(coinPrefab, new Vector3(transform.position.x, transform.position.y + 5.0f, transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
        yield return new WaitForSeconds(0.4f);
        p.state.changeCoins(3);
        doneLanding = true;
    }
}
