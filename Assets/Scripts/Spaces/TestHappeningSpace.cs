using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHappeningSpace : BoardSpace {
    public GameObject coinPrefab;

    public override IEnumerator land(Player p) {
        doneLanding = false;
        ui.Dialogue("Hey, thanks for playing my demo! Here, take 10 Coins!", true);
        yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
        Instantiate(coinPrefab, new Vector3(transform.position.x + ((float) Random.Range(1, 10) / 10.0f), transform.position.y + 5.0f, transform.position.z + ((float) Random.Range(1, 10) / 10.0f)), Quaternion.Euler(0, Random.Range(0, 360), 0));
        yield return new WaitForSeconds(0.1f);
        Instantiate(coinPrefab, new Vector3(transform.position.x + ((float) Random.Range(1, 10) / 10.0f), transform.position.y + 5.0f, transform.position.z + ((float) Random.Range(1, 10) / 10.0f)), Quaternion.Euler(0, Random.Range(0, 360), 0));
        yield return new WaitForSeconds(0.1f);
        Instantiate(coinPrefab, new Vector3(transform.position.x + ((float) Random.Range(1, 10) / 10.0f), transform.position.y + 5.0f, transform.position.z + ((float) Random.Range(1, 10) / 10.0f)), Quaternion.Euler(0, Random.Range(0, 360), 0));
        yield return new WaitForSeconds(0.1f);
        Instantiate(coinPrefab, new Vector3(transform.position.x + ((float) Random.Range(1, 10) / 10.0f), transform.position.y + 5.0f, transform.position.z + ((float) Random.Range(1, 10) / 10.0f)), Quaternion.Euler(0, Random.Range(0, 360), 0));
        yield return new WaitForSeconds(0.1f);
        Instantiate(coinPrefab, new Vector3(transform.position.x + ((float) Random.Range(1, 10) / 10.0f), transform.position.y + 5.0f, transform.position.z + ((float) Random.Range(1, 10) / 10.0f)), Quaternion.Euler(0, Random.Range(0, 360), 0));
        yield return new WaitForSeconds(0.1f);
        Instantiate(coinPrefab, new Vector3(transform.position.x + ((float) Random.Range(1, 10) / 10.0f), transform.position.y + 5.0f, transform.position.z + ((float) Random.Range(1, 10) / 10.0f)), Quaternion.Euler(0, Random.Range(0, 360), 0));
        yield return new WaitForSeconds(0.1f);
        Instantiate(coinPrefab, new Vector3(transform.position.x + ((float) Random.Range(1, 10) / 10.0f), transform.position.y + 5.0f, transform.position.z + ((float) Random.Range(1, 10) / 10.0f)), Quaternion.Euler(0, Random.Range(0, 360), 0));
        yield return new WaitForSeconds(0.1f);
        Instantiate(coinPrefab, new Vector3(transform.position.x + ((float) Random.Range(1, 10) / 10.0f), transform.position.y + 5.0f, transform.position.z + ((float) Random.Range(1, 10) / 10.0f)), Quaternion.Euler(0, Random.Range(0, 360), 0));
        yield return new WaitForSeconds(0.1f);
        Instantiate(coinPrefab, new Vector3(transform.position.x + ((float) Random.Range(1, 10) / 10.0f), transform.position.y + 5.0f, transform.position.z + ((float) Random.Range(1, 10) / 10.0f)), Quaternion.Euler(0, Random.Range(0, 360), 0));
        yield return new WaitForSeconds(0.1f);
        Instantiate(coinPrefab, new Vector3(transform.position.x + ((float) Random.Range(1, 10) / 10.0f), transform.position.y + 5.0f, transform.position.z + ((float) Random.Range(1, 10) / 10.0f)), Quaternion.Euler(0, Random.Range(0, 360), 0));
        yield return new WaitForSeconds(0.3f);
        p.state.changeCoins(10);
        doneLanding = true;
    }
}
