using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelSpace : BoardSpace {
    public override void setup() {
        this.blueChance = 37;
    }

    public override IEnumerator land(Player p) {
        doneLanding = false;
        ui.Dialogue("Boom Boom", "AWRIGHT! It's Duelin' Time!", false);
        yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
        // TODO: Bring in Boom Boom
        ui.Dialogue("Boom Boom", "Listen up, shrimpy! You and someone else are gonna go play a Duel Minigame! You'll each bet some coins or stars, and the winner TAKES IT ALL!", false);
        yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
        List<PlayerState> players = new List<PlayerState>(game.state.GetPlayers());
        players.Remove(p.state);
        if ((p.state.getCoins() >= 5 && (players[0].getCoins() >= 5 || players[1].getCoins() >= 5 || players[2].getCoins() >= 5)) || ((p.state.getCoins() >= 30 || p.state.getStars() >= 1) && (players[0].getStars() >= 1 || players[1].getStars() >= 1 || players[2].getStars() >= 1))) {
            List<string> duelOptions = new List<string>();
            foreach (PlayerState ps in players) {
                duelOptions.Add(ps.charName());
            }
            ui.Dialogue("Boom Boom", "Let's get this party started! Who do ya wanna duel?", duelOptions, false);
            yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
        } else {
            ui.Dialogue("Boom Boom", "...wait a minute.", false);
            yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
            ui.Dialogue("Boom Boom", "WHADDAYA MEAN, Y'ALL AIN'T GOT ANYTHIN' WORTH FIGHTIN' OVER?!", false);
            yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
            ui.Dialogue("Boom Boom", "Well, that just takes all the fun outta it...ugh. Here, take this Duelin' Glove. Use it to call me back when y'all can pay to play!", false);
            yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
            // TODO: Dueling Glove
            ui.Dialogue("Boom Boom", "Later, losers!", false);
            yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
        }
        doneLanding = true;
    }

    public override int AIValue(PlayerState state, List<PlayerState> rivals) {
        switch (state.getPlacing()) {
            case 1:
                return -5;
            default:
                return 15;
        }
    }
}
