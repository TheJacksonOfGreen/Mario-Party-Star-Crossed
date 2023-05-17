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
    private int maxID;

    void Awake() {
        state = new GameState(new PlayerState(0, 0), new PlayerState(0, 1), new PlayerState(0, 2), new PlayerState(0, 3), 10, 0, false, false, false, new List<int>() { 0, 0, 0, 0 }, new List<bool>() { false });
        registry = new List<BoardSpace>();
        foreach (BoardSpace space in GetComponentsInChildren<BoardSpace>()) {
            if (space.id > maxID) {
                maxID = space.id;
            }
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
            pA.setPlacing(1);
            foreach (PlayerState pB in players) {
                if (pA != pB && (pB.getStars() > pA.getStars() || (pA.getStars() == pB.getStars() && pB.getCoins() > pA.getCoins()))) {
                    pA.setPlacing(pA.getPlacing() + 1);
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
        // 9: End-of-turn reset

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
                }
                break;
            case 8:
                // TODO: Implement Minigame Phase
                phase = 9;
                break;
            case 9:
                foreach (PlayerState ps in players) {
                    ps.setTeam(0);
                }
                phase = 0;
                break;
            default:
                phase = 0;
                break;
        }
    }

    public BoardSpace GetSpaceFromID(int id) {
        return this.registry[id];
    }

    public BoardSpace GetRandomSpace() {
        return this.registry[Random.Range(1, this.registry.Count - 1)];
    }

    public int MyPlayerNumber(Player p) {
        if (p == p1) {
            return 1;
        } else if (p == p2) {
            return 2;
        } else if (p == p3) {
            return 3;
        } else if (p == p4) {
            return 4;
        } else {
            return 0;
        }
    }
}
