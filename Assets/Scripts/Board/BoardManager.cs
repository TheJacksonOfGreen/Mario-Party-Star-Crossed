using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    public GameState state;
    public Player p1;
    public Player p2;
    public Player p3;
    public Player p4;

    private int phase;
    private PlayerState[] players;
    private List<BoardSpace> registry;

    void Awake() {
        state = new GameState(new PlayerState(0, 0), new PlayerState(0, 1), new PlayerState(0, 2), new PlayerState(0, 3), 10, 0, false, false, false, new List<int>() { 0, 0, 0, 0 }, new List<bool>() { false });
        registry = new List<BoardSpace>();
        foreach (BoardSpace space in GetComponentsInChildren<BoardSpace>()) {
            if (space.id != -1) {
                while (registry.Count <= space.id) {
                    registry.Add(null);
                }
                registry[space.id] = space;
            }
        }
    }

    // Start is called before the first frame update
    void Start() {
        players = state.GetPlayers();
        p1.SetPlayer(players[0]);
        p2.SetPlayer(players[1]);
        p3.SetPlayer(players[2]);
        p4.SetPlayer(players[3]);
    }

    // Update is called once per frame
    void Update() {
        foreach (PlayerState pA in players) {
            pA.setPlacing(4);
            foreach (PlayerState pB in players) {
                if (pA != pB && (pA.getStars() > pB.getStars() || (pA.getStars() == pB.getStars() && pA.getCoins() > pB.getCoins()))) {
                    pA.setPlacing(pA.getPlacing() - 1);
                }
            }
        }

        // 0: P1 Pre-Roll
        // 1: P1 Post-Roll
        // 2: P2 Pre-Roll
        // 3: P2 Post-Roll
        // 4: P3 Pre-Roll
        // 5: P3 Post-Roll
        // 6: P4 Pre-Roll
        // 7: P4 Post-Roll
        // 8: Minigame

        switch (phase) {
            case 0:
                p1.StartTurn();
                phase = 1;
                break;
            case 1:
                if (p1.TurnOver()) {
                    phase = 2;
                }
                break;
            case 2:
                p2.StartTurn();
                phase = 3;
                break;
            case 3:
                if (p2.TurnOver()) {
                    phase = 4;
                }
                break;
            case 4:
                p3.StartTurn();
                phase = 5;
                break;
            case 5:
                if (p3.TurnOver()) {
                    phase = 6;
                }
                break;
            case 6:
                p4.StartTurn();
                phase = 7;
                break;
            case 7:
                if (p4.TurnOver()) {
                    phase = 8;
                    // TODO: Implement Minigame Phase
                }
                break;
            default:
                phase = 0;
                break;
        }
    }

    public BoardSpace GetSpaceFromID(int id) {
        return this.registry[id];
    }
}
