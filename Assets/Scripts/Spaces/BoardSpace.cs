using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoardSpace : MonoBehaviour {
    [Tooltip("The Space directly after this one in the board layout.")]
    public BoardSpace next;
    [Tooltip("This space's internal ID Number.")]
    public int id;

    protected UIManager ui;
    protected GameStateManager gsm;
    protected bool donePassing;
    protected bool doneLanding;

    // Start is called before the first frame update
    void Start() {
        ui = FindObjectOfType<UIManager>();
        gsm = FindObjectOfType<GameStateManager>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void passHere(Player p) {
        StartCoroutine(pass(p));
    }

    public void landHere(Player p) {
        StartCoroutine(land(p));
    }

    public virtual IEnumerator pass(Player p) {
        yield return null;
    }

    public abstract IEnumerator land(Player p);
}
