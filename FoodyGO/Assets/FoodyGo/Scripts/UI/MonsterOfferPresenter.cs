using UnityEngine;
using packt.FoodyGO.Services;
using UnityEngine.UI;

namespace packt.FoodyGO.UI
{
    public class MonsterOfferPresenter : MonoBehaviour
    {
        public Text NameText;
        public Text CPText;
        public Text LevelText;
        public Text SkillsText;
        public Text OfferText;
        public void PresentOffer(MonsterOffer offer)
        {
            NameText.text = offer.Monster.Name;
            CPText.text = (offer.Monster.Power * offer.Monster.Level).ToString();
            LevelText.text = offer.Monster.Level.ToString();
            SkillsText.text = offer.Monster.Skills;
            OfferText.text = offer.OfferText;
        }
    }
}