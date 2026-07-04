using Gameplay.Harvestable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Effects
{
    public class HarvestAnimationSystem : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private HarvestFlyEffect _harvestFlyEffectPrefab;
        [SerializeField] private XPFlyEffect _xpFlyEffectPrefab;

        [Header("Targets")]
        [SerializeField] private Transform _storageWorldTransform;
        [SerializeField] private RectTransform _xpBarRectTransform;
        [SerializeField] private Camera _camera;

        Vector3 _harvestOffset = new Vector3(0.35f, 0f, 0f);
        Vector3 _xpOffset = new Vector3(-0.35f, 0f, 0f);

        public void PlayHarvest(HarvestableTile tile)
        {
            Vector3 basePosition = tile.transform.position;



            HarvestFlyEffect harvestFlyEffect = Instantiate(_harvestFlyEffectPrefab);

            harvestFlyEffect.Play(
                tile.FlyingEffectSprite,
                basePosition + _harvestOffset,
                _storageWorldTransform.position,
                () => Debug.Log("Storage reached")
            );

            Vector3 screenPositionStart = _camera.WorldToScreenPoint(basePosition + _xpOffset);

            XPFlyEffect XPFlyEffect = Instantiate(_xpFlyEffectPrefab, _xpBarRectTransform.parent);

            XPFlyEffect.Play(
                screenPositionStart,
                _xpBarRectTransform.position,
                () => Debug.Log("XP reached")
            );
        }
    }
}
