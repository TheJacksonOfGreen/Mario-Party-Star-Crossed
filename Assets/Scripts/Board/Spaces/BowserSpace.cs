using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowserSpace : BoardSpace {
    public override void setup() {
        this.blueChance = 0;
        doneLanding = false;
    }

    public override IEnumerator land(Player p) {
        doneLanding = false;
        ui.Dialogue("Bowser", "BWAHAHAHAHAHA!", false);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
        ui.Dialogue("Bowser", "Welcome to the Bowser Space, where it's MY TURN!", false);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
        switch (p.state.charName()) {
            case "Mario":
                ui.Dialogue("Bowser", "Well, well, well...if it isn't my old pal Mario!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "I'm going to ENJOY ruining your day!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "Luigi":
                ui.Dialogue("Bowser", "GRAAAAAH!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "Wait, did that actually scare you?! HA! I don't know what I expected from such a scaredy cat as you, Luigi!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "Peach":
                ui.Dialogue("Bowser", "Ah, Princess! I, uh...I was wondering if we could maybe meet up when you're done here? Maybe get some dinner?", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "NO?! How dare you!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "Yoshi":
                ui.Dialogue("Bowser", "Oh, hey, it's a Yoshi! I just remembered, I have this really delicious fruit that I found, you'd probably love it!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "Oh, wait! Silly me! How could I forget? I already ate the whole thing!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "Wario":
                ui.Dialogue("Bowser", "Ugh...what is that smell?!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "Oh, it's just you, Wario. Great. Looks like I'll have to take this shell to the cleaners again...", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "DK":
                ui.Dialogue("Bowser", "Oh...if it isn't my old nemesis!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "Not so high and mighty without a space of your own, eh, DK?", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "Daisy":
                ui.Dialogue("Bowser", "Oh, Princess Daisy! So nice of you to grace us with your presence!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "...SAID NOBODY EVER!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "Waluigi":
                ui.Dialogue("Bowser", "Oh, it's you, Waluigi. We'd better make this quick.", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "I have to go meet up with some friends I made in Super Smash Bros!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "Toad":
                ui.Dialogue("Bowser", "You know what, I'll give you a deal...you get one shot at me! Hit me, and you're off scot-free!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "Oh, wait, I forgot...you're just a scrawny little Toad! All you can do is PANIC! Gwahahaha!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "Rosalina":
                ui.Dialogue("Bowser", "Whaddaya know...Rosalina herself! That observatory of yours still all powered up?", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "I might stop by and borrow a Power Star or two...or three...or ALL OF THEM!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "Hammer Bro":
                ui.Dialogue("Bowser", "What the...WHAT ARE YOU DOING HERE?! You're supposed to be guarding my castle!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "I did NOT approve these vacation days of yours, Hammer Bro!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "Dry Bones":
                ui.Dialogue("Bowser", "Hey, Dry Bones, why are you even at this party? It's not like you have any-BODY to go with! Gwahahaha!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "What do you mean, you've heard that one before?! That was a Bowser Original! For that, it's punish time!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "Birdo":
                ui.Dialogue("Bowser", "Well, if it isn't the blossoming beauty that makes all the hearts go a-flutter...", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "Oops, my bad! I thought I was talking to Peach for a second. But it's just you, Birdo!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "Tosta":
                ui.Dialogue("Bowser", "Oh, a Tostarenan! Say, where's that Binding Band of yours nowadays?", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "Still in the pyramid, you say? Hm...might wanna go check again, bub! Bwahaha!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            default:
                break;
        }
        ui.Dialogue("Bowser", "Go on! Spin Bowser's Wheel of Magical Misfortune! SPIN IT!", false);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
        List<string> normalEvents = new List<string>() {
            "Lose 10 Coins",
            "Lose 20 Coins",
            "Lose 30 Coins",
            "Everyone Loses 10 Coins",
            "Go Back to Start"
        };
        List<string> specialEvents = new List<string>() {
            "Bowser Revolution",
            "Bowser Shuffle",
            "Bowser Suit Giveaway",
            "Poison Mushrooms for Everybody!"
        };
        List<string> gagEvents = new List<string>() {
            "1,000 Coin Present",
            "100 Star Present",
            "Free Hug"
        };
        PlayerState[] st = game.state.GetStandings();
        if (p.state.getItems().Count >= 2) {
            normalEvents.Add("Lose All Your Items");
        }
        if (st[3] != p.state && p.state.getCoins() >= 20) {
            specialEvents.Add("Bowser's Chump Charity");
        }
        if (game.state.GetType() == typeof(TreasureTempleGameState)) {
            specialEvents.Add("Bowser's Cash Grab");
        } else if (p.state.getStars() >= 1) {
            normalEvents.Add("Lose a Star");
            if (st[3] != p.state && st[3].getStars() != st[2].getStars()) {
                specialEvents.Add("Bowser's Choice Charity");
            }
        }
        List<string> bowserOptions = new List<string>();
        string foo = normalEvents[Random.Range(0, normalEvents.Count - 1)];
        bowserOptions.Add(foo);
        normalEvents.Remove(foo);
        foo = normalEvents[Random.Range(0, normalEvents.Count - 1)];
        bowserOptions.Add(foo);
        normalEvents.Remove(foo);
        foo = normalEvents[Random.Range(0, normalEvents.Count - 1)];
        bowserOptions.Add(foo);
        normalEvents.Remove(foo);
        foo = specialEvents[Random.Range(0, normalEvents.Count - 1)];
        bowserOptions.Add(foo);
        specialEvents.Remove(foo);
        foo = specialEvents[Random.Range(0, normalEvents.Count - 1)];
        bowserOptions.Add(foo);
        specialEvents.Remove(foo);
        foo = specialEvents[Random.Range(0, normalEvents.Count - 1)];
        bowserOptions.Add(foo);
        specialEvents.Remove(foo);
        bowserOptions.Add(gagEvents[Random.Range(0, normalEvents.Count - 1)]);
        ui.Spinner("Bowser Time!", new Color(1.0f, 0.23f, 0.0f, 1.0f), bowserOptions);
        yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
        switch (ui.MostRecentDialogueAnswer()) {
            case "Lose 10 Coins":
                ui.Dialogue("Bowser", "Alright, then! Gimme 10 Coins!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                if (p.state.getCoins() >= 10) {
                    p.state.changeCoins(-10);
                } else {
                    ui.Dialogue("Bowser", "Wait...you don't even have 10 Coins?! Wow...I almost feel bad for you!", false);
                    yield return new WaitForSeconds(0.1f);
                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                    ui.Dialogue("Bowser", "You know what? Here! Take this Bowser Suit!", false);
                    yield return new WaitForSeconds(0.1f);
                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                    yield return StartCoroutine(GivePlayerItem(p, BoardItem.BowserSuit, false));
                    ui.Dialogue("Bowser", "There! Now go cause me some trouble!", false);
                    yield return new WaitForSeconds(0.1f);
                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                }
                ui.Dialogue("Bowser", "BWAHAHAHA! Man, it feels good bein' evil!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "Hope to see you on another Bowser Space real soon!", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "Lose 20 Coins":
                ui.Dialogue("Bowser", "Alright, then! Gimme 20 Coins!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                if (p.state.getCoins() >= 20) {
                    p.state.changeCoins(-20);
                } else {
                    ui.Dialogue("Bowser", "Too steep, huh?", false);
                    yield return new WaitForSeconds(0.1f);
                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                    ui.Dialogue("Bowser", "Well, never let it be said that I'm not a generous guy...how's about you give me 10?", false);
                    yield return new WaitForSeconds(0.1f);
                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                    if (p.state.getCoins() <= 10) {
                        ui.Dialogue("Bowser", "Wait...you don't even have 10 Coins?! Wow...I almost feel bad for you!", false);
                        yield return new WaitForSeconds(0.1f);
                        yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                        ui.Dialogue("Bowser", "You know what? Here! Take this Bowser Suit!", false);
                        yield return new WaitForSeconds(0.1f);
                        yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                        yield return StartCoroutine(GivePlayerItem(p, BoardItem.BowserSuit, false));
                        ui.Dialogue("Bowser", "There! Now go cause me some trouble!", false);
                        yield return new WaitForSeconds(0.1f);
                        yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                    } else {
                        p.state.changeCoins(-10);
                    }
                }
                ui.Dialogue("Bowser", "BWAHAHAHA! Man, it feels good bein' evil!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "Hope to see you on another Bowser Space real soon!", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "Lose 30 Coins":
                ui.Dialogue("Bowser", "Alright, then! Gimme 30 Coins!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                if (p.state.getCoins() >= 30) {
                    p.state.changeCoins(-30);
                } else {
                    ui.Dialogue("Bowser", "Too steep, huh?", false);
                    yield return new WaitForSeconds(0.1f);
                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                    ui.Dialogue("Bowser", "Well, never let it be said that I'm not a generous guy...how's about you give me 15?", false);
                    yield return new WaitForSeconds(0.1f);
                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                    if (p.state.getCoins() <= 15) {
                        ui.Dialogue("Bowser", "Wait...you don't even have 15 Coins?! Wow...I almost feel bad for you!", false);
                        yield return new WaitForSeconds(0.1f);
                        yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                        ui.Dialogue("Bowser", "You know what? Here! Take this Bowser Suit!", false);
                        yield return new WaitForSeconds(0.1f);
                        yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                        yield return StartCoroutine(GivePlayerItem(p, BoardItem.BowserSuit, false));
                        ui.Dialogue("Bowser", "There! Now go cause me some trouble!", false);
                        yield return new WaitForSeconds(0.1f);
                        yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                    } else {
                        p.state.changeCoins(-15);
                    }
                }
                ui.Dialogue("Bowser", "BWAHAHAHA! Man, it feels good bein' evil!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "Hope to see you on another Bowser Space real soon!", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "Everyone Loses 10 Coins":
                ui.Dialogue("Bowser", "Alright, then! Everyone loses 10 Coins!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                foreach (PlayerState ps in st) {
                    ps.changeCoins(-10);
                }
                ui.Dialogue("Bowser", "BWAHAHAHA! Man, it feels good bein' evil!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "Hope to see you on another Bowser Space real soon!", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "Lose all of Your Items":
                ui.Dialogue("Bowser", "You don't need all those items, do you? Gimme!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                p.state.setItems(new List<BoardItem>());
                ui.Dialogue("Bowser", "BWAHAHAHA! Man, it feels good bein' evil!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "Hope to see you on another Bowser Space real soon!", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "Go Back to Start":
                ui.Dialogue("Bowser", "You know, Junior's been getting into Soccer recently, and he's been asking me how far I can kick something.", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "And you know what? I think now would be a great time to show him!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                p.GetComponent<Rigidbody>().AddForce((Vector3.up * 10.0f) + (Vector3.forward * 2.0f), ForceMode.Impulse);
                yield return new WaitForSeconds(2.0f);
                p.transform.position = FindObjectOfType<StartSpace>().transform.position;
                p.state.setSpace(0);
                ui.Dialogue("Bowser", "BWAHAHAHA! Man, it feels good bein' evil!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "Hope to see you on another Bowser Space real soon!", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "Lose a Star":
                ui.Dialogue("Bowser", "Oh, you're in for it now! Gimme one of your Stars!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                p.state.changeStars(-1);
                ui.Dialogue("Bowser", "BWAHAHAHA! Man, it feels good bein' evil!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "Hope to see you on another Bowser Space real soon!", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "Bowser Revolution":
                ui.Dialogue("Bowser", "You know, I've been thinking lately. Everyone nowadays is so focused on who has the most money...but that just blinds them to what's really important in life!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "Everything would be so much better if we all were equal! NOW EVERYONE GIMME ALL YOUR COINS!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                int totalCoins = 0;
                foreach (PlayerState ps in st) {
                    totalCoins += ps.getCoins();
                    ps.setCoins(0);
                }
                ui.Dialogue("Bowser", "Alright, that's " + totalCoins + " Coins! Now, to redistribute them equally!", false);
                yield return new WaitForSeconds(0.1f);
                if (totalCoins % 4 != 0) {
                    yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                    ui.Dialogue("Bowser", "And, as a fee for my services, I'll only take the extra " + (totalCoins % 4) + "! I know, I know, I'm too kind!", false);
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                foreach (PlayerState ps in st) {
                    ps.setCoins(totalCoins / 4);
                }
                ui.Dialogue("Bowser", "Now, you're all equal! See? Doesn't that just feel so much better?", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "Hope to see you on another Bowser Space real soon!", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "Bowser Shuffle":
                ui.Dialogue("Bowser", "Hey, I've been working on this new dance. It's called the Bowser Shuffle! Wanna see it?", new List<string>() {"Yes", "No"}, false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "Sorry, I wasn't listening. Anyway, here we go!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "First, you slide to the left...then you slide to the right...and WA-BAM!", false);
                yield return new WaitForSeconds(0.1f);
                //TODO: Implement Bowser Shuffle
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "What did you think? I'd say I really busted a...move! BWAHAHA!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "Hope to see you on another Bowser Space real soon!", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "Bowser Suit Giveaway":
                ui.Dialogue("Bowser", "Oh, looks like it's your lucky day! I'm giving away a Bowser Suit!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "...oh, not you specifically. I meant your opponents.", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                List<string> names = new List<string>();
                foreach (PlayerState ps in st) {
                    if (ps.charName() != p.state.charName()) {
                        names.Add(ps.charName());
                    }
                }
                ui.Dialogue("Bowser", "Gah, stop whining! I'm feeling generous today, so I'll let you pick who gets it!", names, false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                if (game.p1.state.charName() == ui.MostRecentDialogueAnswer()) {
                    yield return StartCoroutine(GivePlayerItem(game.p1, BoardItem.BowserSuit, false));
                } else if (game.p2.state.charName() == ui.MostRecentDialogueAnswer()) {
                    yield return StartCoroutine(GivePlayerItem(game.p2, BoardItem.BowserSuit, false));
                } else if (game.p3.state.charName() == ui.MostRecentDialogueAnswer()) {
                    yield return StartCoroutine(GivePlayerItem(game.p3, BoardItem.BowserSuit, false));
                } else if (game.p4.state.charName() == ui.MostRecentDialogueAnswer()) {
                    yield return StartCoroutine(GivePlayerItem(game.p4, BoardItem.BowserSuit, false));
                }
                ui.Dialogue("Bowser", "BWAHAHAHA! Man, it feels good bein' evil!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "Hope to see you on another Bowser Space real soon!", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "Bowser's Chump Charity":
                ui.Dialogue("Bowser", "You know, I think charity work is very important! I think we should all give to those in need! If only I had something to give...", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "I know! I'm gonna take half your coins, and give 'em to " + st[3].charName() + "! Aren't I such a swell guy?", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                p.state.changeCoins(p.state.getCoins() / -2);
                st[3].changeCoins(p.state.getCoins());
                ui.Dialogue("Bowser", "See? Doesn't helping the less fortunate make you feel all warm and fuzzy inside?", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "Hope to see you on another Bowser Space real soon!", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "Bowser's Choice Charity":
                ui.Dialogue("Bowser", "You know, I think charity work is very important! I think we should all give to those in need! If only I had something to give...", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "I know! I'm gonna take one of your stars, and give it to " + st[3].charName() + "! Aren't I such a swell guy?", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                p.state.changeStars(-1);
                st[3].changeStars(1);
                ui.Dialogue("Bowser", "See? Doesn't helping the less fortunate make you feel all warm and fuzzy inside?", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "Hope to see you on another Bowser Space real soon!", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "Poison Mushrooms for Everybody!":
                ui.Dialogue("Bowser", "Hey, guess what?", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "Poison Mushrooms!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                foreach (PlayerState ps in st) {
                    ps.setMovement(4);
                }
                ui.Dialogue("Bowser", "BWAHAHAHA! Man, it feels good bein' evil!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "Hope to see you on another Bowser Space real soon!", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "1,000 Coin Present":
                ui.Dialogue("Bowser", "Wait, what? Who put that on the wheel?!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "Uh, right. 1,000 Coins...", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "...", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "...here!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                p.state.changeCoins(1);
                ui.Dialogue("Bowser", "Now, uh, if you'll excuse me, I'm off to go grab the rest!", false);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                yield return new WaitForSeconds(2.0f);
                ui.Dialogue("...he's probably not coming back.", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "100 Star Present":
                ui.Dialogue("Bowser", "Wait, what? Who put that on the wheel?!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "Uh, right. 100 Stars...", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "...", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "...I left them in my castle. Yeah! If you'd just, uh...gimme a second...", false);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                yield return new WaitForSeconds(2.0f);
                ui.Dialogue("...he's probably not coming back.", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            case "Free Hug":
                ui.Dialogue("Bowser", "Wait, what? Who put that on the wheel?!", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                ui.Dialogue("Bowser", "Uh, right. Free Hug. Well...uh...bring it in, I guess.", false);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                //TODO: Implement Free Hug
                ui.Dialogue("Bowser", "We will never speak of this again.", true);
                yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                break;
            default:
                break;
        }
        p.state.UnluckySpaceStatTrigger();
        doneLanding = true;
    }

    public override int AIValue(PlayerState state, List<PlayerState> rivals) {
        switch (state.getPlacing()) {
            case 4:
                return 0;
            case 3:
                return -5;
            case 2:
                return -10;
            default:
                return -15;
        }
    }
}
