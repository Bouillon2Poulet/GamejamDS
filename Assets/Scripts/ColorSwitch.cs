using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorSwitch : MonoBehaviour
{
    public Volume volume; // R�f�rence au Global Volume
    private VolumeProfile volumeProfile;
    private ColorCurves colorCurves; // Effet Color Curves � contr�ler
    public PlayersManager playersManager;

    void Start()
    {
        if (volume == null)
        {
            Debug.LogError("Volume component is missing! Assign your Global Volume.");
            return;
        }

        // R�cup�rer le VolumeProfile depuis le composant Volume
        volumeProfile = volume.profile;

        if (volumeProfile == null)
        {
            Debug.LogError("VolumeProfile is null! Assign a valid Volume Profile in the Volume component.");
            return;
        }

        // R�cup�rer l'effet Color Curves depuis le VolumeProfile
        if (!volumeProfile.TryGet(out colorCurves))
        {
            Debug.LogError("ColorCurves effect not found in the Volume Profile. Add it manually.");
            return;
        }
    }

    void Update()
    {
        if (playersManager != null && colorCurves != null)
        {
            // Activer ou d�sactiver l'effet Color Curves en fonction du joueur actif
            colorCurves.active = (playersManager.getPlayerId() == 1);
        }
    }
}