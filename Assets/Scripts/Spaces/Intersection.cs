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

    private Renderer phantom;
    private Renderer phantomAlt;
    private bool goingAlt;

    public override void setup() {
        this.canLandHere = true;
        phantom = transform.Find("PhantomPrism").gameObject.GetComponent<Renderer>();
        phantomAlt = transform.Find("PhantomPrism (ALT)").gameObject.GetComponent<Renderer>();
        phantom.material = activeMat;
        phantomAlt.material = inactiveMat;
        phantom.enabled = false;
        phantomAlt.enabled = false;
    }

    public override IEnumerator pass(Player p) {
        donePassing = false;
        goingAlt = false;
        phantom.material = activeMat;
        phantomAlt.material = inactiveMat;
        phantom.enabled = true;
        phantomAlt.enabled = true;
        while (!Input.GetKeyDown(KeyCode.Space)) {
            if (Input.GetKeyDown(activeKey)) {
                goingAlt = false;
                phantom.material = activeMat;
                phantomAlt.material = inactiveMat;
            } else if (Input.GetKeyDown(inactiveKey)) {
                goingAlt = true;
                phantom.material = inactiveMat;
                phantomAlt.material = activeMat;
            }
            yield return null;
        }
        if (goingAlt) {
            phantom.enabled = false;
        } else {
            phantomAlt.enabled = false;
        }
        yield return new WaitForSeconds(0.2f);
        BoardSpace temp = next;
        if (goingAlt) {
            next = option;
        }
        phantom.enabled = false;
        phantomAlt.enabled = false;
        donePassing = true;
        yield return new WaitForSeconds(1.0f);
        if (goingAlt) {
            option = next;
            next = temp;
        }
    }
}
