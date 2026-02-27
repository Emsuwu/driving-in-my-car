using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _kidGObj;
    [SerializeField] private GameObject _kidOrigin;
    [SerializeField] private Animator _kidAnimator;

    private Dictionary<string, Vector3> _animOffsets = new();

    private void Awake()
    {
        _animOffsets.Add("T-Pose", Vector3.zero);
        _animOffsets.Add("Breakdance", new Vector3(0f, -0.77f, 0f));
    }

    private void Start()
    {
        StartCoroutine(BreakDanceInABit());
    }

    IEnumerator BreakDanceInABit()
    {
        yield return new WaitForSeconds(10);
        PlayAnim("Breakdance");
    }

    public void PlayAnim(string animationName)
    {
        if (_animOffsets.ContainsKey(animationName)) _kidOrigin.transform.localPosition = _animOffsets[animationName];
        else _kidOrigin.transform.localPosition = Vector3.zero;
        _kidAnimator.Play(animationName);
    }

    public void TPose() { PlayAnim("T-Pose"); }
    public void Breakdance() { PlayAnim("Breakdance"); }
}
