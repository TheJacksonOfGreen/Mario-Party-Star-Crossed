using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState {
    // PLAYER CONFIG
    // 0: Local Human
    // 1: Easy COM
    // 2: Normal COM
    // 3: Hard COM
    // 4: Master COM
    // 5: Remote Human
    private int controller;

    // VISIBLE PLAYER DATA
    private int coins;
    private int stars;
    private int avatar;
    // 0-<# of solid, non-start spaces on board>
    private int space;
    // 0: No Mushroom
    // 1: Mushroom
    // 2: Golden Mushroom
    // 3: Magic Mushroom
    // 4: Poison Mushroom
    private int shroom;
    // 3 spots
    // 0: No Item
    // 1: Mushroom
    // 2: Golden Mushroom
    // 3: Magic Mushroom
    // 4: Poison Mushroom
    // 5: Skeleton Key
    // 6: Warp Pipe
    // 7: Plunder Chest
    // 8: Golden Drink
    // 9: Vac Pack
    // 10: Boo Bell
    // 11: Tweester Totem
    // 12: Dueling Glove
    // 13: Chomp Call
    // 14: Gaddlight
    // 15: Magic Lamp
    // 16: Bowser Suit
    // 17: Double Star Card
    // 18: Chomp Treat
    // 19: Wiggler Whistle
    private List<int> items;

    // INTERNAL PLAYER DATA
    // maxTurn Spots
    private List<int> coinTracking;
    // maxTurn Spots
    private List<int> starTracking;
    private int totalSpacesMoved;
    private int totalMinigameRewards;
    private int totalDuelsPlayed;
    private int totalUnluckySpaces;
    private int totalHappeningSpaces;
    private int totalItemsUsed;

    // AI MOTIVES
    private List<int> hiddenBlockCandidates;
    private int p1Grudge;
    private int p2Grudge;
    private int p3Grudge;
    private int p4Grudge;

    public PlayerState(int controller, int avatar) {
        if (controller < 0 || controller > 5) {
            throw new System.ArgumentException("Controller Param is invalid");
        } else {
            this.controller = controller;
        }
        if (avatar < 0 || controller > 13) {
            throw new System.ArgumentException("Avatar Param is invalid");
        } else {
            this.avatar = avatar;
        }
        this.coins = 0;
        this.stars = 0;
        this.items = new List<int>();
        this.space = 0;
        this.shroom = 0;
        this.coinTracking = new List<int>();
        this.starTracking = new List<int>();
        this.totalSpacesMoved = 0;
        this.totalMinigameRewards = 0;
        this.totalDuelsPlayed = 0;
        this.totalUnluckySpaces = 0;
        this.totalHappeningSpaces = 0;
        this.totalItemsUsed = 0;
        this.hiddenBlockCandidates = new List<int>();
        this.p1Grudge = 0;
        this.p2Grudge = 0;
        this.p3Grudge = 0;
        this.p4Grudge = 0;
    }

    public int getCoins() {
        return this.coins;
    }

    public bool setCoins(int c) {
        this.coins = c;
        if (this.coins < 0) {
            this.coins = 0;
            return false;
        } else if (this.coins > 999) {
            this.coins = 999;
            return false;
        }
        return true;
    }

    public bool changeCoins(int c) {
        this.coins += c;
        if (this.coins < 0) {
            this.coins = 0;
            return false;
        } else if (this.coins > 999) {
            this.coins = 999;
            return false;
        }
        return true;
    }

    public int getStars() {
        return this.stars;
    }

    public bool setStars(int s) {
        this.stars = s;
        if (this.stars < 0) {
            this.stars = 0;
            return false;
        } else if (this.stars > 99) {
            this.stars = 99;
            return false;
        }
        return true;
    }

    public bool changeStars(int s) {
        this.stars += s;
        if (this.stars < 0) {
            this.stars = 0;
            return false;
        } else if (this.stars > 99) {
            this.stars = 99;
            return false;
        }
        return true;
    }

    public bool addItem(int i) {
        if (this.items.Count == 3) {
            return false;
        } else {
            this.items.Add(i);
            return true;
        }
    }

    public bool removeItem(int i) {
        if (this.items.Contains(i)) {
            items.Remove(i);
            return true;
        } else {
            return false;
        }
    }

    public void setSpace(int s) {
        this.space = s;
    }

    public string charName() {
        switch (this.avatar) {
            case 0:
                return "Mario";
            case 1:
                return "Luigi";
            case 2:
                return "Peach";
            case 3:
                return "Yoshi";
            case 4:
                return "DK";
            case 5:
                return "Wario";
            case 6:
                return "Daisy";
            case 7:
                return "Waluigi";
            case 8:
                return "Toad";
            case 9:
                return "Rosalina";
            case 10:
                return "Dry Bones";
            case 11:
                return "Hammer Bro";
            case 12:
                return "Birdo";
            default:
                return "Tosta";
        }
    }
}