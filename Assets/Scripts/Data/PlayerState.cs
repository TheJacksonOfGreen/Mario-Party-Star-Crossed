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
    // 5: Bowser Suit
    private int shroom;
    // 3 spots
    private List<BoardItem> items;
    private int minigameTeam;
    private int externalPlacing;

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
        if (avatar < 0 || avatar > 13) {
            throw new System.ArgumentException("Avatar Param is invalid");
        } else {
            this.avatar = avatar;
        }
        this.coins = 0;
        this.stars = 0;
        this.items = new List<BoardItem>();
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
        this.externalPlacing = 1;
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

    public List<BoardItem> getItems() {
        return this.items;
    }

    public void setItems(List<BoardItem> li) {
        this.items = li;
    }

    public bool addItem(BoardItem i) {
        if (this.items.Count == 3) {
            return false;
        } else {
            this.items.Add(i);
            return true;
        }
    }

    public bool removeItem(BoardItem i) {
        if (this.items.Contains(i)) {
            items.Remove(i);
            return true;
        } else {
            return false;
        }
    }

    public bool hasItem(BoardItem i) {
        return this.items.Contains(i);
    }

    public void setSpace(int s) {
        this.space = s;
    }

    public int getSpaceID() {
        return this.space;
    }

    public void setMovement(int s) {
        this.shroom = s;
    }

    public int getMovement() {
        return this.shroom;
    }

    public int getPlacing() {
        return this.externalPlacing;
    }

    public void setPlacing(int p) {
        this.externalPlacing = p;
    }

    public int getTeam() {
        return this.minigameTeam;
    }

    public void setTeam(int t) {
        this.minigameTeam = t;
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

    public Color charColor() {
        switch (this.avatar) {
            case 0:
                return new Color(1.0f, 0.0f, 0.0f, 1.0f);
            case 1:
                return new Color(0.0f, 0.8f, 0.0f, 1.0f);
            case 2:
                return new Color(1.0f, 0.5f, 1.0f, 1.0f);
            case 3:
                return new Color(0.0f, 1.0f, 0.0f, 1.0f);
            case 4:
                return new Color(0.6f, 0.3f, 0.0f, 1.0f);
            case 5:
                return new Color(1.0f, 1.0f, 0.0f, 1.0f);
            case 6:
                return new Color(1.0f, 0.5f, 0.0f, 1.0f);
            case 7:
                return new Color(0.5f, 0.0f, 1.0f, 1.0f);
            case 8:
                return new Color(1.0f, 0.8f, 0.8f, 1.0f);
            case 9:
                return new Color(0.0f, 0.8f, 0.8f, 1.0f);
            case 10:
                return new Color(0.4f, 0.4f, 0.4f, 1.0f);
            case 11:
                return new Color(0.0f, 0.4f, 0.0f, 1.0f);
            case 12:
                return new Color(1.0f, 0.0f, 1.0f, 1.0f);
            default:
                return new Color(0.6f, 0.6f, 0.0f, 1.0f);
        }
    }
}