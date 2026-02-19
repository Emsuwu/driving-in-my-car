using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NaughtyAttributes;

public class BuildVersion : MonoBehaviour
{
    public enum BUILDTYPE { Dev, Alpha, Beta, PreRelease, Release }
    private Dictionary<BUILDTYPE, string> _buildTypeDict = new Dictionary<BUILDTYPE, string>();

    [BoxGroup("Build Version")]
    public string Version = "";
    [Range(0,10)] public int Subversion = 0;
    public BUILDTYPE BuildType = BUILDTYPE.Dev;

    [Button("Generate version number")] private void SetVersionButton()
    {
        SetVersionBasedOnDate();
    }

    [Button("Increment Subversion")]
    private void IncrementSubversion()
    {
        Subversion += 1;
        SetVersionBasedOnDate();
    }
    [Button("Decrement Subversion")]
    private void DecrementSubversion()
    {
        Subversion -= 1;
        Subversion = Math.Clamp(Subversion, 0, int.MaxValue);
        SetVersionBasedOnDate();
    }


    [BoxGroup("References")]
    [SerializeField] private TMPro.TMP_Text _versionText;

    private void BuildDict()
    {
        _buildTypeDict.Add(BUILDTYPE.Dev, "dev"); _buildTypeDict.Add(BUILDTYPE.Alpha, "alpha");
        _buildTypeDict.Add(BUILDTYPE.Beta, "beta"); _buildTypeDict.Add(BUILDTYPE.PreRelease, "pre-release");
        _buildTypeDict.Add(BUILDTYPE.Release, "release");
    }
    private void SetVersionBasedOnDate()
    {
        if (!_buildTypeDict.ContainsKey(BUILDTYPE.Dev)) BuildDict();

        string subheading;
        _buildTypeDict.TryGetValue(BuildType, out subheading);
        Version = $"{DateTime.Now.Month}.{DateTime.Now.Day}.{Subversion}-{subheading}";
        _versionText.text = Version;
    }
    private void Awake()
    {
        _versionText.text = Version;
    }
}
