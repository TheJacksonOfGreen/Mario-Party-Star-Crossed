using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameSet : ScriptableObject {
    // 14 spots
    // 0-27
    protected List<int> mostRecentFFAs;
    // 7 spots
    // 0-13
    protected List<int> mostRecent2v2s;
    // 6 spots
    // 0-11
    protected List<int> mostRecent1v3s;
    // 5 spots
    // 0-9
    protected List<int> mostRecentDuels;
    // 3 spots
    // 0-5
    protected List<int> mostRecentBattles;

    public MinigameSet(List<bool> choices) {
        this.init();
    }

    public void init() {
    	this.mostRecentFFAs = new List<int>();
        this.mostRecent2v2s = new List<int>();
        this.mostRecent1v3s = new List<int>();
        this.mostRecentDuels = new List<int>();
        this.mostRecentBattles = new List<int>();
    }
}