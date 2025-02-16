using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorSwitch : MonoBehaviour
{
    public Volume volume; // Référence au Global Volume
    private VolumeProfile volumeProfile;
    private ColorCurves colorCurves; // Effet Color Curves à contrôler
    public PlayersManager playersManager;

    void Start()
    {
        if (volume == null)
        {
            Debug.LogError("Volume component is missing! Assign your Global Volume.");
            return;
        }

        // Récupérer le VolumeProfile depuis le composant Volume
        volumeProfile = volume.profile;

        if (volumeProfile == null)
        {
            Debug.LogError("VolumeProfile is null! Assign a valid Volume Profile in the Volume component.");
            return;
        }

        // Récupérer l'effet Color Curves depuis le VolumeProfile
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
            // Activer ou désactiver l'effet Color Curves en fonction du joueur actif
            colorCurves.active = (playersManager.getPlayerId() == 1);
        }
    }
}