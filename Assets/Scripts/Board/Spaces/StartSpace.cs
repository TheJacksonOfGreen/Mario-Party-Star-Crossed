using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSpace : BoardSpace {
    public override int AIValue(PlayerState state, List<PlayerState> rivals) {
        return 0;
    }
}
