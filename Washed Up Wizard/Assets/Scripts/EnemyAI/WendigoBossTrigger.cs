using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WendigoBossTrigger : MonoBehaviour {

    public GameObject wendigo;
    public GameObject wenidgoHealthBar;
    public GameObject playerCamera;
    public GameObject harpy;
    public GameObject inputController;
    public Rigidbody player;
    public Rigidbody cameraStartPos;
    public ActivateFollowTarget startIceWall;
    public ActivateFollowTarget endIceWall;

    private Animator cameraAnim;
    private FollowTargetLerp cameraMove;
    private MoveInputReceiver moveInputReceiver;
    private SpellInputReceiver spellInputReceiver;

	// Use this for initialization
	void Awake () {
        wendigo.SetActive(false);
        wenidgoHealthBar.SetActive(false);
        harpy.SetActive(false);
        cameraAnim = playerCamera.GetComponent<Animator>();
        cameraMove = playerCamera.GetComponent<FollowTargetLerp>();
        moveInputReceiver = inputController.GetComponent<MoveInputReceiver>();
        spellInputReceiver = inputController.GetComponent<SpellInputReceiver>();
        cameraAnim.enabled = false;

	}

    void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player")) {
            StartCoroutine(ActivateBoss ());
        }
    }

    IEnumerator ActivateBoss () {
        moveInputReceiver.enabled = false;
        spellInputReceiver.enabled = false;
        cameraMove.target = cameraStartPos;
        wendigo.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        cameraMove.enabled = false;
        cameraAnim.enabled = true;
        cameraAnim.SetTrigger("WendigoBossStart");
        yield return new WaitForSeconds(6f);
        endIceWall.Activate();
        yield return new WaitForSeconds(3f);
        startIceWall.Activate();
        wenidgoHealthBar.SetActive(true);
        yield return new WaitForSeconds(1f);
        harpy.SetActive(true);
        wendigo.GetComponent<WendigoAI>().enabled = true;
        cameraMove.enabled = true;
        moveInputReceiver.enabled = true;
        spellInputReceiver.enabled = true;
        cameraAnim.enabled = false;
        cameraMove.target = player;
        Destroy(gameObject, 1f);
    }
}
