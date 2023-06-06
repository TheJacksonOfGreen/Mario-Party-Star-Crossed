using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBrain {
    protected PlayerState state;
    protected List<PlayerState> rivals;
    protected BoardManager game;

    public AIBrain(PlayerState state, PlayerState rival1, PlayerState rival2, PlayerState rival3, BoardManager game) {
        this.state = state;
        this.rivals = new List<PlayerState>() { rival1, rival2, rival3 };
        this.game = game;
    } 

    public abstract string Prompt(string question, List<string> options, int spacesLeft);

    public int GetValueOfRolling(int n) {
        return game.GetSpaceFromID(state.getSpaceID()).CumulativeValue(state, rivals, n);
    }

    protected static List<float[]> rollPercentages = new List<float[]>() {
        new float[] { 0.01f, 0.02f, 0.03f, 0.04f, 0.05f, 0.06f, 0.07f, 0.08f, 0.09f, 0.1f, 0.09f, 0.08f, 0.07f, 0.06f, 0.05f, 0.04f, 0.03f, 0.02f, 0.01f }, 
        new float[] { 0.001f, 0.003f, 0.006f, 0.01f, 0.015f, 0.021f, 0.028f, 0.036f, 0.045f, 0.055f, 0.063f, 0.069f, 0.073f, 0.075f, 0.075f, 0.073f, 0.069f, 0.063f, 0.055f, 0.045f, 0.036f, 0.028f, 0.021f, 0.015f, 0.01f, 0.006f, 0.003f, 0.001f }, 
        new float[] { 0.0041f, 0.0206f, 0.0617f, 0.1235f, 0.1852f, 0.2099f, 0.1852f, 0.1235f, 0.0617f, 0.0206f, 0.0041f }
    };
}
