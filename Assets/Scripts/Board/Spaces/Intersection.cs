using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersection : BoardSpace {
    [Tooltip("The other space a player could move to from here.")]
    public BoardSpace option;
    [Tooltip("Material to use for active phantom.")]
    public Material activeMat;
    [Tooltip("Material to use for inactive phantom.")]
    public Material inactiveMat;
    [Tooltip("Key to press to select the default route.")]
    public KeyCode activeKey;
    [Tooltip("Key to press to select the alternative route.")]
    public KeyCode inactiveKey;
    [Tooltip("Gate attached to the base route.")]
    public SkeletonGate gate;
    [Tooltip("Gate attached to the alternate route.")]
    public SkeletonGate optionGate;

    private Renderer phantom;
    private Renderer phantomAlt;
    private bool goingAlt;

    public override void setup() {
        this.canLandHere = false;
        phantom = transform.Find("PhantomPrism").gameObject.GetComponent<Renderer>();
        phantomAlt = transform.Find("PhantomPrism (ALT)").gameObject.GetComponent<Renderer>();
        phantom.material = activeMat;
        phantomAlt.material = inactiveMat;
        phantom.enabled = false;
        phantomAlt.enabled = false;
    }

    public override IEnumerator pass(Player p) {
        BoardSpace temp = next;
        if ((gate == null && optionGate == null) || p.state.hasItem(BoardItem.SkeletonKey) || p.state.getMovement() == 5) {
            donePassing = false;
            goingAlt = false;
            phantom.material = activeMat;
            phantomAlt.material = inactiveMat;
            phantom.enabled = true;
            phantomAlt.enabled = true;
            bool waitingForChoice = true;
            while (waitingForChoice) {
                if (Input.GetKeyDown(activeKey)) {
                    goingAlt = false;
                    phantom.material = activeMat;
                    phantomAlt.material = inactiveMat;
                } else if (Input.GetKeyDown(inactiveKey)) {
                    goingAlt = true;
                    phantom.material = inactiveMat;
                    phantomAlt.material = activeMat;
                }
                if (Input.GetKeyDown(KeyCode.Space)) {
                    if ((gate != null && !goingAlt) || (optionGate != null && goingAlt)) {
                        if (p.state.getMovement() != 5) {
                            ui.Dialogue("Skeleton Key", "Whoa, whoa, wait up! Are you sure you want me to unlock this gate for you?", new List<string>() { "Yes", "No"}, true);
                            yield return new WaitUntil(() => ui.WaitForDialogueAnswer());
                        }
                        if (p.state.getMovement() == 5 || ui.MostRecentDialogueAnswer() == "Yes") {
                            waitingForChoice = false;
                            if (p.state.getMovement() != 5) {
                                p.state.removeItem(BoardItem.SkeletonKey);
                            }
                            if (gate == null) {
                                optionGate.Open();
                            } else {
                                gate.Open();
                            }
                        }
                    } else {
                        waitingForChoice = false;
                    }
                }
                yield return null;
            }
            if (goingAlt) {
                phantom.enabled = false;
            } else {
                phantomAlt.enabled = false;
            }
            yield return new WaitForSeconds(0.2f);
            if (goingAlt) {
                next = option;
            }
            phantom.enabled = false;
            phantomAlt.enabled = false;
            donePassing = true;
        } else if (gate != null) {
            next = option;
            donePassing = true;
        }
        yield return new WaitForSeconds(1.0f);
        if (goingAlt) {
            option = next;
            next = temp;
        }
        if (optionGate != null) {
            optionGate.Close();
        } 
        if (gate != null) {
            gate.Close();
        }
    }
}
