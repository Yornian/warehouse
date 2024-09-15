using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void PerformInteraction(GameObject Player);
    void PerformInteractionLongPress(GameObject Player);
    void PerformInteractionClick(GameObject Player);
    void PerformInteractionF(GameObject Player);
}
