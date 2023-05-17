using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpace : BoardSpace {
    public override IEnumerator pass(Player p) {
        this.canLandHere = false;
        donePassing = false;
        ui.MoveCounter(false);
        if (p.state.hasItem(BoardItem.DoubleStarCard) && p.state.getCoins() >= 40) {
            ui.Dialogue("Oh, Lucky You! You have a Double Star Card! Would you like to buy 2 Stars for 40 Coins?", new List<string>() {"40 Coins => 2 Stars", "20 Coins => 1 Star", "No Thanks"}, true);
        } else if (p.state.getCoins() >= 20) {
            ui.Dialogue("Would you like to buy a Star for 20 Coins?", new List<string>() {"20 Coins => 1 Star", "No Thanks"}, true);
        } else {
            ui.Dialogue("You don't have enough Coins to buy a Star.", true);
        }
        yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
        switch (ui.MostRecentDialogueAnswer()) {
            case "20 Coins => 1 Star":
                p.state.changeCoins(-20);
                p.state.changeStars(1);
                break;
            case "40 Coins => 2 Stars":
                p.state.changeCoins(-40);
                p.state.changeStars(2);
                break;
            default:
                break;
        }
        donePassing = true;
        ui.MoveCounter(true);
    }
}
