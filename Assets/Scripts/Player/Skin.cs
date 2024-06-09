using System;
using UnityEngine;

namespace BGSTask
{
    //Class responsible for changing the character's clothes. There are no validations here.

    public enum CharacterSkinPart { Hood, Face, Shoulder, Torso, Leg, Boot}

    public class Skin : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _hood;
        [SerializeField] private SpriteRenderer _face;
        [SerializeField] private SpriteRenderer _shoulderl;
        [SerializeField] private SpriteRenderer _shoulderr;
        [SerializeField] private SpriteRenderer _torso;
        [SerializeField] private SpriteRenderer _legl;
        [SerializeField] private SpriteRenderer _legr;
        [SerializeField] private SpriteRenderer _bootl;
        [SerializeField] private SpriteRenderer _bootr;

        private void Awake()
        {
            EventController.SetPlayerSkinPart += SetPart;
        }

        private void SetPart(string part, string code)
        {
            Sprite aux = Resources.Load<Sprite>($"Icons/{part}/{code}");

            Enum.TryParse(part, out CharacterSkinPart partEnum);

            switch (partEnum) 
            {
                case CharacterSkinPart.Hood:
                    _hood.sprite = aux;
                    break;

                case CharacterSkinPart.Face:
                    _face.sprite = aux;

                    break;

                case CharacterSkinPart.Shoulder:
                    _shoulderl.sprite = aux;
                    _shoulderr.sprite = Resources.Load<Sprite>($"Icons/{part}/r{code}");

                    break;

                case CharacterSkinPart.Torso:
                    _torso.sprite = aux;

                    break;

                case CharacterSkinPart.Leg:
                    _legl.sprite = aux;
                    _legr.sprite = Resources.Load<Sprite>($"Icons/{part}/r{code}");

                    break;

                case CharacterSkinPart.Boot:
                    _bootl.sprite = aux;
                    _bootr.sprite = Resources.Load<Sprite>($"Icons/{part}/r{code}");

                    break;
            }
        }
    }
}

