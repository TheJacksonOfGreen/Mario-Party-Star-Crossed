using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoardItem { Mushroom, GoldenMushroom, MagicMushroom, PoisonMushroom, SkeletonKey, WarpPipe, PlunderChest, GoldenDrink, VacPack, BooBell, TweesterTotem, DuelingGlove, ChompCall, Gaddlight, MagicLamp, BowserSuit, DoubleStarCard, ChompTreat, WigglerWhistle }

public class ItemSpace : BoardSpace {
    public static List<string> itemNames = new List<string>() {
        "Mushroom",
        "Golden Mushroom",
        "Magic Mushroom",
        "Poison Mushroom",
        "Skeleton Key",
        "Warp Pipe",
        "Plunder Chest",
        "Golden Drink",
        "Vac Pack",
        "Boo Bell",
        "Tweester Totem",
        "Dueling Glove",
        "Chomp Call",
        "Gaddlight",
        "Magic Lamp",
        "Bowser Suit",
        "Double Star Card",
        "Chomp Treat",
        "Wiggler Whistle"
    };

    public bool booItemsAllowed = true;
    public bool chompCallAllowed = true;
    public bool magicLampAllowed = true;
    public bool doubleStarCardAllowed = true;
    public bool chompTreatAllowed = false;
    public bool wigglerWhistleAllowed = false;

    private List<BoardItem> tier1;
    private List<BoardItem> tier2;
    private List<BoardItem> tier3;
    private List<BoardItem> tier4;
    private List<BoardItem> tier5;

    private int[,,] odds = new int[4, 4, 5] {
        {{10, 8, 2, 0, 0}, {9, 8, 3, 0, 0}, {8, 7, 4, 1, 0}, {8, 6, 4, 2, 0}},
        {{10, 8, 2, 0, 0}, {8, 6, 3, 2, 1}, {6, 5, 4, 3, 2}, {4, 5, 5, 3, 3}},
        {{9, 8, 2, 1, 0}, {8, 6, 3, 2, 1}, {4, 5, 5, 4, 3}, {2, 4, 4, 6, 4}},
        {{8, 6, 4, 2, 0}, {6, 5, 4, 3, 2}, {2, 4, 4, 6, 4}, {1, 1, 4, 8, 6}}
    };

    public override void setup() {
        this.canLandHere = false;
        tier5 = new List<BoardItem>() {
            BoardItem.Mushroom,
            BoardItem.PoisonMushroom,
            BoardItem.SkeletonKey
        };
        tier4 = new List<BoardItem>() {
            BoardItem.WarpPipe,
            BoardItem.PlunderChest,
            BoardItem.GoldenDrink
        };
        tier3 = new List<BoardItem>() {
            BoardItem.GoldenMushroom,
            BoardItem.MagicMushroom,
            BoardItem.VacPack,
            BoardItem.TweesterTotem
        };
        tier2 = new List<BoardItem>() {
            BoardItem.DuelingGlove,
            BoardItem.BowserSuit
        };
        tier1 = new List<BoardItem>();
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
        if (tier1.Count < 1) {
            tier1.AddRange(tier2);
            tier1.AddRange(tier3);
        }
    }

    public override IEnumerator pass(Player p) {
        if (game.state.getTurnStatus() != 4) {
            donePassing = false;
            ui.MoveCounter(false);
            BoardItem choice = CalcOdds(Random.Range(1, 20), p.state.getPlacing(), game.state.getTurnStatus());
            GetComponentsInChildren<AudioSource>()[0].Play();
            yield return StartCoroutine(GivePlayerItem(p, choice, true));
        }
        donePassing = true;
        ui.MoveCounter(true);
    }

    private BoardItem CalcOdds(int r, int p, int t) {
        int i = 0;
        while (r > odds[p-1, t, i]) {
            r -= odds[p-1, t, i];
            i += 1;
        }
        if (i == 4) {
            return tier1[Random.Range(0, tier1.Count - 1)];
        } else if (i == 3) {
            return tier2[Random.Range(0, tier2.Count - 1)];
        } else if (i == 2) {
            return tier3[Random.Range(0, tier3.Count - 1)];
        } else if (i == 1) {
            return tier4[Random.Range(0, tier4.Count - 1)];
        } else {
            return tier5[Random.Range(0, tier5.Count - 1)];
        }
    }
}
