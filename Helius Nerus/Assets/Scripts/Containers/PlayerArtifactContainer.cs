﻿using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerArtifactContainer : GenericContainer<PlayerArtifactIconDesc, ArtifactType, PlayerArtifact>
{
    private List<Type> _artifactTypes = new List<Type>();
    private List<PlayerArtifact> _artifacts = new List<PlayerArtifact>();

    public override PlayerArtifactIconDesc GetValueByKey(ArtifactType key)
    {
        for (int i = 0; i < _allModifiers.Count; ++i)
        {
            if (_allModifiers[i].Modifier == key)
                return _allModifiers[i];
        }
        return null;
    }

    public override PlayerArtifact GetArtifact(ArtifactType key)
    {
        return _artifacts[(int)key].Clone();
    }

    protected override void PreCookTypes()
    {
        _artifactTypes.Clear();
        _artifacts.Clear();
        foreach (ArtifactType type in (ArtifactType[])Enum.GetValues(typeof(ArtifactType)))
        {
            Type ctype = Type.GetType(type.ToString());
#if UNITY_EDITOR
            if (ctype == null)
            {
                Debug.LogError("ArtifactsCollection handle wrong artifact name: " + type.ToString());
                Debug.Break();
            }
#endif
            _artifactTypes.Add(ctype);
            _artifacts.Add((PlayerArtifact)Activator.CreateInstance(ctype));
        }
    }
}