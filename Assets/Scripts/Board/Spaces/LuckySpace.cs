using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckySpace : BoardSpace {
    public bool booItemsAllowed = true;
    public bool chompCallAllowed = true;
    public bool magicLampAllowed = true;
    public bool doubleStarCardAllowed = true;
    public bool chompTreatAllowed = false;
    public GameObject coinPrefab;

    public override void setup() {
        this.blueChance = 100;
        doneLanding = false;
    }

    public override IEnumerator land(Player p) {
        doneLanding = false;
        List<string> normalEvents = new List<string>() {
            "Get 5 Coins",
            "Get 7 Coins",
            "Get 10 Coins",
            "Get 12 Coins",
            "Get 15 Coins",
            "Everyone Gets 3 Coins",
            "Everyone Gets 5 Coins",
            "Everyone Gets 7 Coins",
            "Get a Mushroom",
            "Get a Poison Mushroom",
            "Get a Skeleton Key",
            "Get a Warp Pipe",
            "Get a Golden Drink",
            "Get a Plunder Chest"
        };
        List<string> rareEvents = new List<string>() {
            "Everyone Gets 10 Coins",
            "Everyone Gets 12 Coins",
            "Get 20 Coins",
            "Get a Golden Mushroom",
            "Get a Magic Mushroom",
            "Get a Vac Pack",
            "Get a Tweester Totem",
            "Get a Dueling Glove",
            "Get a Bowser Suit"
        };
        List<string> superRareEvents = new List<string>() {
            "Get 25 Coins",
            "Get 30 Coins",
            "Everyone Gets 20 Coins",
            "Get an Item Bag"
        };
        PlayerState[] st = game.state.GetStandings();
        if (game.state.GetType() == typeof(TreasureTempleGameState)) {
            normalEvents.Add("Invest 5 Coins in a Random Shrine");
            rareEvents.Add("Invest 15 Coins in a Random Shrine");
            rareEvents.Add("Invest 3 Coins in Every Shrine");
            rareEvents.Add("Get a Wiggler Whistle");
        }
        if (game.state.GetType() == typeof(MarvelousMetroGameState)) {
            rareEvents.Add("Lower the Price of Stars by 5 Coins");
            rareEvents.Add("Lower the Price of Stars by 10 Coins");
        }
        if (booItemsAllowed) {
            rareEvents.Add("Get a Gaddlight");
            superRareEvents.Add("Get a Boo Bell");
        }
        if (chompCallAllowed) {
            rareEvents.Add("Get a Chomp Call");
        }
        if (magicLampAllowed) {
            superRareEvents.Add("Get a Magic Lamp");
        }
        if (doubleStarCardAllowed) {
            superRareEvents.Add("Get a Double Star Card");
        }
        if (chompTreatAllowed) {
            rareEvents.Add("Get a Chomp Treat");
        }
        List<string> luckyOptions = new List<string>();
        string foo = normalEvents[Random.Range(0, normalEvents.Count - 1)];
        luckyOptions.Add(foo);
        normalEvents.Remove(foo);
        foo = normalEvents[Random.Range(0, normalEvents.Count - 1)];
        luckyOptions.Add(foo);
        normalEvents.Remove(foo);
        foo = normalEvents[Random.Range(0, normalEvents.Count - 1)];
        luckyOptions.Add(foo);
        normalEvents.Remove(foo);
        foo = normalEvents[Random.Range(0, normalEvents.Count - 1)];
        luckyOptions.Add(foo);
        normalEvents.Remove(foo);
        foo = rareEvents[Random.Range(0, normalEvents.Count - 1)];
        luckyOptions.Add(foo);
        rareEvents.Remove(foo);
        foo = rareEvents[Random.Range(0, normalEvents.Count - 1)];
        luckyOptions.Add(foo);
        rareEvents.Remove(foo);
        luckyOptions.Add(superRareEvents[Random.Range(0, normalEvents.Count - 1)]);
        ui.Spinner("Lucky Spin!", new Color(0.0f, 0.68f, 1.0f, 1.0f), luckyOptions);
        yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
        switch (ui.MostRecentDialogueAnswer()) {
            case "Get 5 Coins":
                for (int i = 0; i < 5; i++) {
                    Instantiate(coinPrefab, new Vector3(transform.position.x, transform.position.y + 5.0f, transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(1.2f);
                p.state.changeCoins(5);
                break;
            case "Get 7 Coins":
                for (int i = 0; i < 7; i++) {
                    Instantiate(coinPrefab, new Vector3(transform.position.x, transform.position.y + 5.0f, transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(1.2f);
                p.state.changeCoins(7);
                break;
            case "Get 10 Coins":
                for (int i = 0; i < 10; i++) {
                    Instantiate(coinPrefab, new Vector3(transform.position.x, transform.position.y + 5.0f, transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(1.2f);
                p.state.changeCoins(10);
                break;
            case "Get 12 Coins":
                for (int i = 0; i < 12; i++) {
                    Instantiate(coinPrefab, new Vector3(transform.position.x, transform.position.y + 5.0f, transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(1.2f);
                p.state.changeCoins(12);
                break;
            case "Get 15 Coins":
                for (int i = 0; i < 15; i++) {
                    Instantiate(coinPrefab, new Vector3(transform.position.x, transform.position.y + 5.0f, transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(1.2f);
                p.state.changeCoins(15);
                break;
            case "Get 20 Coins":
                for (int i = 0; i < 20; i++) {
                    Instantiate(coinPrefab, new Vector3(transform.position.x, transform.position.y + 5.0f, transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(1.2f);
                p.state.changeCoins(20);
                break;
            case "Get 25 Coins":
                for (int i = 0; i < 25; i++) {
                    Instantiate(coinPrefab, new Vector3(transform.position.x, transform.position.y + 5.0f, transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(1.2f);
                p.state.changeCoins(25);
                break;
            case "Get 30 Coins":
                for (int i = 0; i < 30; i++) {
                    Instantiate(coinPrefab, new Vector3(transform.position.x, transform.position.y + 5.0f, transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(1.2f);
                p.state.changeCoins(30);
                break;
            case "Everyone Gets 3 Coins":
                for (int i = 0; i < 3; i++) {
                    Instantiate(coinPrefab, new Vector3(game.p1.transform.position.x, game.p1.transform.position.y + 5.0f, game.p1.transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    Instantiate(coinPrefab, new Vector3(game.p2.transform.position.x, game.p2.transform.position.y + 5.0f, game.p2.transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    Instantiate(coinPrefab, new Vector3(game.p3.transform.position.x, game.p3.transform.position.y + 5.0f, game.p3.transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    Instantiate(coinPrefab, new Vector3(game.p4.transform.position.x, game.p4.transform.position.y + 5.0f, game.p4.transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(1.2f);
                foreach (PlayerState ps in st) {
                    ps.changeCoins(3);
                }
                break;
            case "Everyone Gets 5 Coins":
                for (int i = 0; i < 5; i++) {
                    Instantiate(coinPrefab, new Vector3(game.p1.transform.position.x, game.p1.transform.position.y + 5.0f, game.p1.transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    Instantiate(coinPrefab, new Vector3(game.p2.transform.position.x, game.p2.transform.position.y + 5.0f, game.p2.transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    Instantiate(coinPrefab, new Vector3(game.p3.transform.position.x, game.p3.transform.position.y + 5.0f, game.p3.transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    Instantiate(coinPrefab, new Vector3(game.p4.transform.position.x, game.p4.transform.position.y + 5.0f, game.p4.transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(1.2f);
                foreach (PlayerState ps in st) {
                    ps.changeCoins(5);
                }
                break;
            case "Everyone Gets 7 Coins":
                for (int i = 0; i < 7; i++) {
                    Instantiate(coinPrefab, new Vector3(game.p1.transform.position.x, game.p1.transform.position.y + 5.0f, game.p1.transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    Instantiate(coinPrefab, new Vector3(game.p2.transform.position.x, game.p2.transform.position.y + 5.0f, game.p2.transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    Instantiate(coinPrefab, new Vector3(game.p3.transform.position.x, game.p3.transform.position.y + 5.0f, game.p3.transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    Instantiate(coinPrefab, new Vector3(game.p4.transform.position.x, game.p4.transform.position.y + 5.0f, game.p4.transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(1.2f);
                foreach (PlayerState ps in st) {
                    ps.changeCoins(7);
                }
                break;
            case "Everyone Gets 10 Coins":
                for (int i = 0; i < 10; i++) {
                    Instantiate(coinPrefab, new Vector3(game.p1.transform.position.x, game.p1.transform.position.y + 5.0f, game.p1.transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    Instantiate(coinPrefab, new Vector3(game.p2.transform.position.x, game.p2.transform.position.y + 5.0f, game.p2.transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    Instantiate(coinPrefab, new Vector3(game.p3.transform.position.x, game.p3.transform.position.y + 5.0f, game.p3.transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    Instantiate(coinPrefab, new Vector3(game.p4.transform.position.x, game.p4.transform.position.y + 5.0f, game.p4.transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(1.2f);
                foreach (PlayerState ps in st) {
                    ps.changeCoins(10);
                }
                break;
            case "Everyone Gets 12 Coins":
                for (int i = 0; i < 12; i++) {
                    Instantiate(coinPrefab, new Vector3(game.p1.transform.position.x, game.p1.transform.position.y + 5.0f, game.p1.transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    Instantiate(coinPrefab, new Vector3(game.p2.transform.position.x, game.p2.transform.position.y + 5.0f, game.p2.transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    Instantiate(coinPrefab, new Vector3(game.p3.transform.position.x, game.p3.transform.position.y + 5.0f, game.p3.transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    Instantiate(coinPrefab, new Vector3(game.p4.transform.position.x, game.p4.transform.position.y + 5.0f, game.p4.transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(1.2f);
                foreach (PlayerState ps in st) {
                    ps.changeCoins(12);
                }
                break;
            case "Everyone Gets 20 Coins":
                for (int i = 0; i < 20; i++) {
                    Instantiate(coinPrefab, new Vector3(game.p1.transform.position.x, game.p1.transform.position.y + 5.0f, game.p1.transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    Instantiate(coinPrefab, new Vector3(game.p2.transform.position.x, game.p2.transform.position.y + 5.0f, game.p2.transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    Instantiate(coinPrefab, new Vector3(game.p3.transform.position.x, game.p3.transform.position.y + 5.0f, game.p3.transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    Instantiate(coinPrefab, new Vector3(game.p4.transform.position.x, game.p4.transform.position.y + 5.0f, game.p4.transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(1.2f);
                foreach (PlayerState ps in st) {
                    ps.changeCoins(20);
                }
                break;
            case "Get a Mushroom":
                yield return StartCoroutine(GivePlayerItem(p, BoardItem.Mushroom, true));
                break;
            case "Get a Golden Mushroom":
                yield return StartCoroutine(GivePlayerItem(p, BoardItem.GoldenMushroom, true));
                break;
            case "Get a Magic Mushroom":
                yield return StartCoroutine(GivePlayerItem(p, BoardItem.MagicMushroom, true));
                break;
            case "Get a Poison Mushroom":
                yield return StartCoroutine(GivePlayerItem(p, BoardItem.PoisonMushroom, true));
                break;
            case "Get a Skeleton Key":
                yield return StartCoroutine(GivePlayerItem(p, BoardItem.SkeletonKey, true));
                break;
            case "Get a Warp Pipe":
                yield return StartCoroutine(GivePlayerItem(p, BoardItem.WarpPipe, true));
                break;
            case "Get a Golden Drink":
                yield return StartCoroutine(GivePlayerItem(p, BoardItem.GoldenDrink, true));
                break;
            case "Get a Vac Pack":
                yield return StartCoroutine(GivePlayerItem(p, BoardItem.VacPack, true));
                break;
            case "Get a Boo Bell":
                yield return StartCoroutine(GivePlayerItem(p, BoardItem.BooBell, true));
                break;
            case "Get a Tweester Totem":
                yield return StartCoroutine(GivePlayerItem(p, BoardItem.TweesterTotem, true));
                break;
            case "Get a Dueling Glove":
                yield return StartCoroutine(GivePlayerItem(p, BoardItem.DuelingGlove, true));
                break;
            case "Get a Chomp Call":
                yield return StartCoroutine(GivePlayerItem(p, BoardItem.ChompCall, true));
                break;
            case "Get a Gaddlight":
                yield return StartCoroutine(GivePlayerItem(p, BoardItem.Gaddlight, true));
                break;
            case "Get a Magic Lamp":
                yield return StartCoroutine(GivePlayerItem(p, BoardItem.MagicLamp, true));
                break;
            case "Get a Bowser Suit":
                yield return StartCoroutine(GivePlayerItem(p, BoardItem.BowserSuit, true));
                break;
            case "Get a Double Star Card":
                yield return StartCoroutine(GivePlayerItem(p, BoardItem.DoubleStarCard, true));
                break;
            case "Get a Chomp Treat":
                yield return StartCoroutine(GivePlayerItem(p, BoardItem.ChompTreat, true));
                break;
            case "Get a Wiggler Whistle":
                yield return StartCoroutine(GivePlayerItem(p, BoardItem.WigglerWhistle, true));
                break;
            case "Get an Item Bag":
                while (p.state.getItems().Count < 3) {
                    int ri = Random.Range(0, 18);
                    if ((!booItemsAllowed && (ri == 9 || ri == 13)) || (game.state.GetType() != typeof(MarvelousMetroGameState) && ri == 18) || (!chompCallAllowed && ri == 12) || (!magicLampAllowed && ri == 14) || (!chompTreatAllowed && ri == 17) || (!doubleStarCardAllowed && ri == 16)) {
                        ri = Random.Range(0, 8);
                    }
                    yield return StartCoroutine(GivePlayerItem(p, (BoardItem) ri, true));
                }
                break;
            default:
                break;
        }
        doneLanding = true;
    }

    public override int AIValue(PlayerState state, List<PlayerState> rivals) {
        return 10;
    }
}
