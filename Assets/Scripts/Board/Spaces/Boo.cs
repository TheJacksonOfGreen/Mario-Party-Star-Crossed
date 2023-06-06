using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boo : BoardSpace {
    public override IEnumerator pass(Player p) {
        donePassing = false;
        ui.MoveCounter(false);
        ui.Dialogue("Boo", "Eeeeeheeheehee! I just love causing trouble! I think I'll go steal a little something from one of your rivals!", false);
        yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
        yield return new WaitForSeconds(0.1f);
        bool stealingStars = false;
        List<string> coinTheftChoices = new List<string>();
        if (game.p1.state.getCoins() > 0 && game.MyPlayerNumber(p) != 1) {
            coinTheftChoices.Add(game.p1.state.charName());
        }
        if (game.p2.state.getCoins() > 0 && game.MyPlayerNumber(p) != 2) {
            coinTheftChoices.Add(game.p2.state.charName());
        }
        if (game.p3.state.getCoins() > 0 && game.MyPlayerNumber(p) != 3) {
            coinTheftChoices.Add(game.p3.state.charName());
        }
        if (game.p4.state.getCoins() > 0 && game.MyPlayerNumber(p) != 4) {
            coinTheftChoices.Add(game.p4.state.charName());
        }
        List<string> starTheftChoices = new List<string>();
        if (game.p1.state.getStars() > 0 && game.MyPlayerNumber(p) != 1) {
            coinTheftChoices.Add(game.p1.state.charName());
        }
        if (game.p2.state.getStars() > 0 && game.MyPlayerNumber(p) != 2) {
            coinTheftChoices.Add(game.p2.state.charName());
        }
        if (game.p3.state.getStars() > 0 && game.MyPlayerNumber(p) != 3) {
            coinTheftChoices.Add(game.p3.state.charName());
        }
        if (game.p4.state.getStars() > 0 && game.MyPlayerNumber(p) != 4) {
            coinTheftChoices.Add(game.p4.state.charName());
        }
        if (p.state.getCoins() >= 40 && starTheftChoices.Count > 0) {
            ui.Dialogue("Boo", "I'll steal Coins for free, but Stars will cost you 40 Coins!", new List<string>() {"I want Coins!", "I want Stars!", "No, Stealing is Wrong!"}, false);
            yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
            yield return new WaitForSeconds(0.1f);
            if (ui.MostRecentDialogueAnswer() == "I want Stars!") {
                stealingStars = true;
            }
            if (ui.MostRecentDialogueAnswer() != "No, Stealing is Wrong!") {
                if (stealingStars) {
                    ui.Dialogue("Boo", "Alright, alright! Who should I take them from?", starTheftChoices, false);
                } else {
                    ui.Dialogue("Boo", "Alright, alright! Who should I take them from?", coinTheftChoices, false);
                }
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                yield return new WaitForSeconds(0.1f);
            }
        } else if (coinTheftChoices.Count > 0) {
            coinTheftChoices.Add("No, Stealing is Wrong!");
            ui.Dialogue("Boo", "Whose Coins do you want me to take?", coinTheftChoices, false);
            yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
            yield return new WaitForSeconds(0.1f);
        } else {
            ui.Dialogue("Boo", "Well...I would if anyone had anything good to steal.", false);
            yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
            yield return new WaitForSeconds(0.1f);
        }
        if (ui.MostRecentDialogueAnswer() == game.p1.state.charName()) {
            ui.Dialogue("Boo", "Alright! Here I go!", false);
            yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
            yield return new WaitForSeconds(0.1f);
            if (game.p1.state.hasItem(BoardItem.Gaddlight)) {
                game.p1.state.removeItem(BoardItem.Gaddlight);
                ui.Dialogue("Boo", "Ugh! You didn't mention they had a Gaddlight! I'm outta here!", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                yield return new WaitForSeconds(0.1f);
            } else if (stealingStars) {
                p.state.changeCoins(-40);
                p.state.changeStars(1);
                game.p1.state.changeStars(-1);
                ui.Dialogue("Boo", "Oh, aren't I simply devilish? I just stole you a Star!", false);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                yield return new WaitForSeconds(0.1f);
                ui.Dialogue("Boo", "Ueeheeheeheehee! Come back real soon!", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                yield return new WaitForSeconds(0.1f);
            } else {
                int coinsToSteal = Random.Range(game.p1.state.getCoins() / 4, 20);
                p.state.changeCoins(coinsToSteal);
                game.p1.state.changeCoins(-1 * coinsToSteal);
                ui.Dialogue("Boo", "How do you like that? I just stole you " + coinsToSteal + " Coins!", false);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                yield return new WaitForSeconds(0.1f);
                ui.Dialogue("Boo", "Ueeheeheeheehee! Come back real soon!", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                yield return new WaitForSeconds(0.1f);
            }
        } else if (ui.MostRecentDialogueAnswer() == game.p2.state.charName()) {
            ui.Dialogue("Boo", "Alright! Here I go!", false);
            yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
            yield return new WaitForSeconds(0.1f);
            if (game.p2.state.hasItem(BoardItem.Gaddlight)) {
                game.p2.state.removeItem(BoardItem.Gaddlight);
                ui.Dialogue("Boo", "Ugh! You didn't mention they had a Gaddlight! I'm outta here!", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                yield return new WaitForSeconds(0.1f);
            } else if (stealingStars) {
                p.state.changeCoins(-40);
                p.state.changeStars(1);
                game.p2.state.changeStars(-1);
                ui.Dialogue("Boo", "Oh, aren't I simply devilish? I just stole you a Star!", false);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                yield return new WaitForSeconds(0.1f);
                ui.Dialogue("Boo", "Ueeheeheeheehee! Come back real soon!", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                yield return new WaitForSeconds(0.1f);
            } else {
                int coinsToSteal = Random.Range(game.p2.state.getCoins() / 4, 20);
                p.state.changeCoins(coinsToSteal);
                game.p2.state.changeCoins(-1 * coinsToSteal);
                ui.Dialogue("Boo", "How do you like that? I just stole you " + coinsToSteal + " Coins!", false);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                yield return new WaitForSeconds(0.1f);
                ui.Dialogue("Boo", "Ueeheeheeheehee! Come back real soon!", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                yield return new WaitForSeconds(0.1f);
            }
        } else if (ui.MostRecentDialogueAnswer() == game.p3.state.charName()) {
            ui.Dialogue("Boo", "Alright! Here I go!", false);
            yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
            yield return new WaitForSeconds(0.1f);
            if (game.p3.state.hasItem(BoardItem.Gaddlight)) {
                game.p3.state.removeItem(BoardItem.Gaddlight);
                ui.Dialogue("Boo", "Ugh! You didn't mention they had a Gaddlight! I'm outta here!", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                yield return new WaitForSeconds(0.1f);
            } else if (stealingStars) {
                p.state.changeCoins(-40);
                p.state.changeStars(1);
                game.p3.state.changeStars(-1);
                ui.Dialogue("Boo", "Oh, aren't I simply devilish? I just stole you a Star!", false);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                yield return new WaitForSeconds(0.1f);
                ui.Dialogue("Boo", "Ueeheeheeheehee! Come back real soon!", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                yield return new WaitForSeconds(0.1f);
            } else {
                int coinsToSteal = Random.Range(game.p3.state.getCoins() / 4, 20);
                p.state.changeCoins(coinsToSteal);
                game.p3.state.changeCoins(-1 * coinsToSteal);
                ui.Dialogue("Boo", "How do you like that? I just stole you " + coinsToSteal + " Coins!", false);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                yield return new WaitForSeconds(0.1f);
                ui.Dialogue("Boo", "Ueeheeheeheehee! Come back real soon!", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                yield return new WaitForSeconds(0.1f);
            }
        } else if (ui.MostRecentDialogueAnswer() == game.p4.state.charName()) {
            ui.Dialogue("Boo", "Alright! Here I go!", false);
            yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
            yield return new WaitForSeconds(0.1f);
            if (game.p4.state.hasItem(BoardItem.Gaddlight)) {
                game.p4.state.removeItem(BoardItem.Gaddlight);
                ui.Dialogue("Boo", "Ugh! You didn't mention they had a Gaddlight! I'm outta here!", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                yield return new WaitForSeconds(0.1f);
            } else if (stealingStars) {
                p.state.changeCoins(-40);
                p.state.changeStars(1);
                game.p4.state.changeStars(-1);
                ui.Dialogue("Boo", "Oh, aren't I simply devilish? I just stole you a Star!", false);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                yield return new WaitForSeconds(0.1f);
                ui.Dialogue("Boo", "Ueeheeheeheehee! Come back real soon!", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                yield return new WaitForSeconds(0.1f);
            } else {
                int coinsToSteal = Random.Range(game.p4.state.getCoins() / 4, 20);
                p.state.changeCoins(coinsToSteal);
                game.p4.state.changeCoins(-1 * coinsToSteal);
                ui.Dialogue("Boo", "How do you like that? I just stole you " + coinsToSteal + " Coins!", false);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                yield return new WaitForSeconds(0.1f);
                ui.Dialogue("Boo", "Ueeheeheeheehee! Come back real soon!", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                yield return new WaitForSeconds(0.1f);
            }
        } else {
            ui.Dialogue("Boo", "Ugh! Laaaame!", true);
            yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
            yield return new WaitForSeconds(0.1f);
        }
        donePassing = true;
        ui.MoveCounter(true);
    }

    public override int AIValue(PlayerState state, List<PlayerState> rivals) {
        if (state.getCoins() >= 40 && rivals.Any(p => p.getStars() >= 1)) {
            return 30;
        } else {
            return 15;
        }
    }
}
