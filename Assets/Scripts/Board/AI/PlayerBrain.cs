using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : AIBrain {
    public PlayerBrain(PlayerState state, PlayerState rival1, PlayerState rival2, PlayerState rival3, BoardManager game) : base(state, rival1, rival2, rival3, game) {} 

    public override string Prompt(string question, List<string> options, int spacesLeft) {
        return "";
    }
}
