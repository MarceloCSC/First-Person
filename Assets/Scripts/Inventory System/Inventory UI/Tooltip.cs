using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FirstPerson.Inventory
{

    public class Tooltip : MonoBehaviour, IPointerExitHandler
    {

        public Button examineButton, useButton, equipButton, dropButton;

        [Header("Screen Positioning")]
        [SerializeField] Canvas canvas = null;
        [SerializeField] RectTransform rectTransform = null;
        [SerializeField] float padding = 5.0f;

        private Button[] buttons;


        private void Awake()
        {
            SetReferences();
            gameObject.SetActive(false);
        }

        public void ShowTooltip(Item item)
        {
            transform.position = Input.mousePosition.GetPositionOnScreen(canvas, rectTransform, padding);
            RestoreButtons();
            GetItemType(item);
            gameObject.SetActive(true);
        }

        public void HideTooltip()
        {
            if (gameObject.activeSelf)
            {
                Slot.ItemSelected = null;
                gameObject.SetActive(false);
            }
        }

        private void GetItemType(Item item)
        {
            switch (item.Root.Type)
            {
                case ItemType.Food:
                    Deactivate(equipButton);
                    break;
                case ItemType.Valuable:
                    Deactivate(useButton);
                    break;
                case ItemType.Readable:
                    Deactivate(useButton);
                    Deactivate(equipButton);
                    break;
                default:
                    break;
            }
        }

        private void Deactivate(Button button)
        {
            button.interactable = false;
            button.GetComponentInChildren<Text>().color = Color.grey;
        }

        private void RestoreButtons()
        {
            foreach (Button button in buttons)
            {
                button.interactable = true;
                button.GetComponentInChildren<Text>().color = Color.white;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!eventData.pointerCurrentRaycast.isValid || eventData.pointerCurrentRaycast.gameObject.transform.parent != transform)
            {
                HideTooltip();
            }
        }

        private void SetReferences()
        {
            buttons = new Button[] { examineButton, useButton, equipButton, dropButton };
        }

    }

}
