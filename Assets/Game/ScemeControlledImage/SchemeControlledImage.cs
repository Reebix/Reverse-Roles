using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

namespace Game.ScemeControlledImage
{
    public class SchemeControlledImage : MonoBehaviour
    {
        public Sprite hasGamepadSprite, noGamepadSprite;
        
        private bool _hasGamepad;
        private UnityEngine.UI.Image _image;
        
        // Start is called before the first frame update
        private void Start()
        {
               _image = GetComponent<UnityEngine.UI.Image>();
        }

        // Update is called once per frame
        private void Update()
        {
            _hasGamepad = Gamepad.current != null;
            _image.sprite = _hasGamepad ? hasGamepadSprite : noGamepadSprite;
        }
    }
}