using DG.Tweening;
using Gameplay.Audio;
using Gameplay.Harvestable;
using Gameplay.Tutorial;
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

        [Header("Storage Bouncy Settings")]
        [SerializeField] private float _storageBouncyScale;
        [SerializeField] private float _storageBouncyDuration;

        [Header("UI")]
        [SerializeField] private TutorialText _tutorialText;

        [Header("Audio")]
        [SerializeField] private HarvestAudioSystem _harvestAudioSystem;

        private int _storageBouncyAnimationId;
        private Tween _storageTween;
        private Vector3 _harvestOffset = new Vector3(0.35f, 0f, 0f);
        private Vector3 _xpOffset = new Vector3(-0.35f, 0f, 0f);

        public void PlayHarvest(HarvestableTile tile)
        {
            Vector3 basePosition = tile.transform.position;

            HarvestFlyEffect harvestFlyEffect = Instantiate(_harvestFlyEffectPrefab);

            harvestFlyEffect.Play(
                tile.FlyingEffectSprite,
                basePosition + _harvestOffset,
                _storageWorldTransform.position,
                () => PlayStorageBouncy()
            );

            _harvestAudioSystem.PlayHarvestSFX();

            Vector3 screenPositionStart = _camera.WorldToScreenPoint(basePosition + _xpOffset);

            XPFlyEffect XPFlyEffect = Instantiate(_xpFlyEffectPrefab, _xpBarRectTransform.parent);

            XPFlyEffect.Play(
                screenPositionStart,
                _xpBarRectTransform.position,
                null
            );

            _tutorialText.Hide();
        }

        private void PlayStorageBouncy()
        {
            int myId = ++_storageBouncyAnimationId;

            _storageTween?.Kill();
            _storageWorldTransform.localScale = Vector3.one;

            _storageTween = _storageWorldTransform
                .DOScale(_storageBouncyScale, _storageBouncyDuration)
                .SetLoops(2, LoopType.Yoyo)
                .OnKill(() =>
                {
                    if (myId != _storageBouncyAnimationId)
                        return;
                });
        }
    }
}
