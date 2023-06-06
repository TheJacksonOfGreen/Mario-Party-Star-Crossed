using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShop : BoardSpace {
    public static List<int> prices = new List<int>() { 5, 12, 12, 3, 3, 7, 7, 5, 10, 20, 15, 15, 12, 15, 30, 25, 15, 15, 15 };

    public bool booItemsAllowed = true;
    public bool chompCallAllowed = true;
    public bool magicLampAllowed = true;
    public bool doubleStarCardAllowed = true;
    public bool chompTreatAllowed = false;
    public bool wigglerWhistleAllowed = false;

    private List<BoardItem> bucketA;
    private List<BoardItem> bucketB;
    private List<BoardItem> bucketC;

    public override void setup() {
        this.canLandHere = false;
        List<BoardItem> tier5 = new List<BoardItem>() {
            BoardItem.Mushroom,
            BoardItem.PoisonMushroom,
            BoardItem.SkeletonKey
        };
        List<BoardItem> tier4 = new List<BoardItem>() {
            BoardItem.WarpPipe,
            BoardItem.PlunderChest,
            BoardItem.GoldenDrink
        };
        List<BoardItem> tier3 = new List<BoardItem>() {
            BoardItem.GoldenMushroom,
            BoardItem.MagicMushroom,
            BoardItem.VacPack,
            BoardItem.TweesterTotem
        };
        List<BoardItem> tier2 = new List<BoardItem>() {
            BoardItem.DuelingGlove,
            BoardItem.BowserSuit
        };
        List<BoardItem> tier1 = new List<BoardItem>();
        if (booItemsAllowed) {
            tier3.Add(BoardItem.Gaddlight);
            tier1.Add(BoardItem.BooBell);
        };
        if (chompCallAllowed) {
            tier2.Add(BoardItem.ChompCall);
        }
        if (magicLampAllowed) {
            tier1.Add(BoardItem.MagicLamp);
        }
        if (doubleStarCardAllowed) {
            tier1.Add(BoardItem.DoubleStarCard);
        }
        if (chompTreatAllowed) {
            tier2.Add(BoardItem.ChompTreat);
        }
        if (wigglerWhistleAllowed) {
            tier3.Add(BoardItem.WigglerWhistle);
        }

        bucketA = new List<BoardItem>();
        bucketB = new List<BoardItem>();
        bucketC = new List<BoardItem>();

        bucketA.AddRange(tier5);
        bucketA.AddRange(tier4);
        bucketB.AddRange(tier3);
        bucketC.AddRange(tier3);
        bucketC.AddRange(tier2);
        bucketC.AddRange(tier1);
    }

    public override IEnumerator pass(Player p) {
        if (game.state.getTurnStatus() != 4) {
            donePassing = false;
            ui.MoveCounter(false);
            if (p.state.getCoins() < 3) {
                ui.Dialogue("Item Shop Owner", "Sorry, but you're a little low on coins right now. Come back some other time.", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
            } else {
                List<BoardItem> choices = new List<BoardItem>();
                if (p.state.getCoins() < 7) {
                    if (p.state.getCoins() >= 5) {
                        choices.Add(BoardItem.Mushroom);
                        choices.Add(BoardItem.GoldenDrink);
                    }
                    choices.Add(BoardItem.SkeletonKey);
                    choices.Add(BoardItem.PoisonMushroom);
                }
                List<BoardItem> temp = new List<BoardItem>(bucketA);
                BoardItem choice = bucketA[Random.Range(0, bucketA.Count - 1)];
                if (ItemShop.prices[(int) choice] <= p.state.getCoins()) {
                    choices.Add(choice);
                }
                bucketA.Remove(choice);
                while (choices.Count < 1) {
                    choice = bucketA[Random.Range(0, bucketA.Count - 1)];
                    if (ItemShop.prices[(int) choice] <= p.state.getCoins()) {
                        choices.Add(choice);
                    }
                    bucketA.Remove(choice);
                }
                choice = bucketA[Random.Range(0, bucketA.Count - 1)];
                if (ItemShop.prices[(int) choice] <= p.state.getCoins()) {
                    choices.Add(choice);
                }
                bucketA.Remove(choice);
                while (choices.Count < 2) {
                    choice = bucketA[Random.Range(0, bucketA.Count - 1)];
                    if (ItemShop.prices[(int) choice] <= p.state.getCoins()) {
                        choices.Add(choice);
                    }
                    bucketA.Remove(choice);
                }
                if (p.state.getCoins() < 12) {
                    bucketA.Add(BoardItem.VacPack);
                    choice = bucketA[Random.Range(0, bucketA.Count - 1)];
                    if (ItemShop.prices[(int) choice] <= p.state.getCoins()) {
                        choices.Add(choice);
                    }
                    bucketA.Remove(choice);
                    while (choices.Count < 3) {
                        choice = bucketA[Random.Range(0, bucketA.Count - 1)];
                        if (ItemShop.prices[(int) choice] <= p.state.getCoins()) {
                            choices.Add(choice);
                        }
                        bucketA.Remove(choice);
                    }
                    choice = bucketA[Random.Range(0, bucketA.Count - 1)];
                    if (ItemShop.prices[(int) choice] <= p.state.getCoins()) {
                        choices.Add(choice);
                    }
                    bucketA.Remove(choice);
                    while (choices.Count < 4) {
                        choice = bucketA[Random.Range(0, bucketA.Count - 1)];
                        if (ItemShop.prices[(int) choice] <= p.state.getCoins()) {
                            choices.Add(choice);
                        }
                        bucketA.Remove(choice);
                    }
                    bucketA = new List<BoardItem>(temp);
                } else {
                    bucketA = new List<BoardItem>(temp);
                    temp = new List<BoardItem>(bucketB);
                    choice = bucketB[Random.Range(0, bucketB.Count - 1)];
                    if (ItemShop.prices[(int) choice] <= p.state.getCoins()) {
                        choices.Add(choice);
                    }
                    bucketB.Remove(choice);
                    while (choices.Count < 3) {
                        choice = bucketB[Random.Range(0, bucketB.Count - 1)];
                        if (ItemShop.prices[(int) choice] <= p.state.getCoins()) {
                            choices.Add(choice);
                        }
                        bucketB.Remove(choice);
                    }
                    bucketB = new List<BoardItem>(temp);
                    temp = new List<BoardItem>(bucketC);
                    foreach (BoardItem bi in choices) {
                        bucketC.Remove(bi);
                    }
                    choice = bucketC[Random.Range(0, bucketC.Count - 1)];
                    if (ItemShop.prices[(int) choice] <= p.state.getCoins()) {
                        choices.Add(choice);
                    }
                    bucketC.Remove(choice);
                    while (choices.Count < 4) {
                        choice = bucketC[Random.Range(0, bucketC.Count - 1)];
                        if (ItemShop.prices[(int) choice] <= p.state.getCoins()) {
                            choices.Add(choice);
                        }
                        bucketC.Remove(choice);
                    }
                    bucketC = new List<BoardItem>(temp);
                }
                List<string> choiceNames = new List<string>();
                foreach (BoardItem bi in choices) {
                    choiceNames.Add(ItemSpace.itemNames[(int) bi] + " (" + ItemShop.prices[(int) bi] + ")");
                }
                choiceNames.Add("Actually, I'm Good.");
                ui.Dialogue("Welcome to the Item Shop! What would you like?", choiceNames, false);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                int i = (choiceNames.Count - 1) - choiceNames.IndexOf(ui.MostRecentDialogueAnswer());
                if (i == 4) {
                    ui.Dialogue("Item Shop Owner", "Alright, then. We hope to see you again soon!", true);
                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                } else {
                    p.state.changeCoins(-1 * ItemShop.prices[(int) choices[i]]);
                    yield return StartCoroutine(GivePlayerItem(p, choices[i], false));
                    yield return new WaitForSeconds(0.1f);
                    ui.Dialogue("Item Shop Owner", "We hope to see you again soon!", true);
                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                }
            }
        }
        donePassing = true;
        ui.MoveCounter(true);
    }

    public override int AIValue(PlayerState state, List<PlayerState> rivals) {
        return 10;
    }
}
