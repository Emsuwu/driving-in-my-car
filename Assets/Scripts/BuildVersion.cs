using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildVersion : MonoBehaviour
{
    public enum BUILDTYPE { Dev, Alpha, Beta, PreRelease, Release }
    private Dictionary<BUILDTYPE, string> _buildTypeDict = new Dictionary<BUILDTYPE, string>();

    [Header("Build Version")]
    [ContextMenuItem("Generate version number", "SetVersionBasedOnDate")] public string Version = "";
    [Header("Subversion")]
    [ContextMenuItem("Generate version number", "SetVersionBasedOnDate"), Range(0, 10)] public int Subversion = 0;
    [ContextMenuItem("Generate version number", "SetVersionBasedOnDate")] public BUILDTYPE BuildType = BUILDTYPE.Dev;

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
