using UnityEngine;
using System.Collections.Generic;

namespace UnityEngine.UI
{
    public static class MyDefaultControls
    {
        #region code from DefaultControls.cs
        public struct Resources
        {
            public Sprite standard;
            public Sprite background;
            public Sprite inputField;
            public Sprite knob;
            public Sprite checkmark;
            public Sprite dropdown;
            public Sprite mask;
        }

        private const float kWidth = 160f;
        private const float kThickHeight = 30f;
        private const float kThinHeight = 20f;
        //private static Vector2 s_ThickElementSize = new Vector2(kWidth, kThickHeight);
        //private static Vector2 s_ThinElementSize = new Vector2(kWidth, kThinHeight);
        //private static Vector2 s_ImageElementSize = new Vector2(100f, 100f);
        //private static Color s_DefaultSelectableColor = new Color(1f, 1f, 1f, 1f);
        //private static Color s_PanelColor = new Color(1f, 1f, 1f, 0.392f);
        private static Color s_TextColor = new Color(50f / 255f, 50f / 255f, 50f / 255f, 1f);

        // Helper methods at top

        private static GameObject CreateUIElementRoot(string name, Vector2 size)
        {
            GameObject child = new GameObject(name);
            RectTransform rectTransform = child.AddComponent<RectTransform>();
            rectTransform.sizeDelta = size;
            return child;
        }

        static GameObject CreateUIObject(string name, GameObject parent)
        {
            GameObject go = new GameObject(name);
            go.AddComponent<RectTransform>();
            SetParentAndAlign(go, parent);
            return go;
        }

        private static void SetDefaultTextValues(Text lbl)
        {
            // Set text values we want across UI elements in default controls.
            // Don't set values which are the same as the default values for the Text component,
            // since there's no point in that, and it's good to keep them as consistent as possible.
            lbl.color = s_TextColor;
        }

        private static void SetDefaultColorTransitionValues(Selectable slider)
        {
            ColorBlock colors = slider.colors;
            colors.highlightedColor = new Color(0.882f, 0.882f, 0.882f);
            colors.pressedColor = new Color(0.698f, 0.698f, 0.698f);
            colors.disabledColor = new Color(0.521f, 0.521f, 0.521f);
        }

        private static void SetParentAndAlign(GameObject child, GameObject parent)
        {
            if (parent == null)
                return;

            child.transform.SetParent(parent.transform, false);
            SetLayerRecursively(child, parent.layer);
        }

        private static void SetLayerRecursively(GameObject go, int layer)
        {
            go.layer = layer;
            Transform t = go.transform;
            for (int i = 0; i < t.childCount; i++)
                SetLayerRecursively(t.GetChild(i).gameObject, layer);
        }
        #endregion
        
        public static GameObject CreateOptionControl(DefaultControls.Resources resources)
        {
            GameObject root = CreateUIElementRoot("SimpleOptionControl", new Vector2(300, 100));
            root.transform.localPosition = Vector2.zero;
            //GameObject init
            GameObject prevBtnGo = DefaultControls.CreateButton(resources);
            prevBtnGo.name = "prev_btn";
            prevBtnGo.transform.SetParent(root.transform, false);
            RectTransform rect1 = prevBtnGo.GetComponent<RectTransform>();
            rect1.anchorMin = new Vector2(0, 0.5f);
            rect1.anchorMax = new Vector2(0, 0.5f);
            rect1.sizeDelta = new Vector2(160, 30);
            rect1.pivot = new Vector2(0.5f, 0.5f);

            GameObject nextBtnGo = DefaultControls.CreateButton(resources);
            nextBtnGo.name = "next_btn";
            nextBtnGo.transform.SetParent(root.transform, false);
            RectTransform rect2 = nextBtnGo.GetComponent<RectTransform>();
            rect2.anchorMin = new Vector2(1, 0.5f);
            rect2.anchorMax = new Vector2(1, 0.5f);
            rect2.sizeDelta = new Vector2(160, 30);
            rect2.pivot = new Vector2(0.5f, 0.5f);

            GameObject captionBackGo = CreateUIObject("Caption", root);
            RectTransform rect3 = captionBackGo.GetComponent<RectTransform>();
            rect3.anchorMin = new Vector2(0.5f, 0.5f);
            rect3.anchorMax = new Vector2(0.5f, 0.5f);
            rect3.sizeDelta = new Vector2(120, 30);
            rect3.pivot = new Vector2(0.5f, 0.5f);

            GameObject captionTextGo =  CreateUIObject("Text", captionBackGo);
            RectTransform rect4 = captionTextGo.GetComponent<RectTransform>();
            rect4.sizeDelta = new Vector2(120, 30);
            rect4.anchorMin = new Vector2(0, 0);
            rect4.anchorMax = new Vector2(1, 1);
            rect4.pivot = new Vector2(0.5f, 0.5f);

            // Setup UI components.
            SimpleOptionControl optionControl = root.AddComponent<SimpleOptionControl>();
            Button prevBtn = prevBtnGo.GetComponent<Button>();
            Button nextBtn = nextBtnGo.GetComponent<Button>();
            Image captionImage = captionBackGo.AddComponent<Image>();
            Text prevText = prevBtn.transform.Find("Text").GetComponent<Text>();
            Text nextText = nextBtn.transform.Find("Text").GetComponent<Text>();
            Text captionText = captionTextGo.AddComponent<Text>();

            optionControl.prevBtn = prevBtn;
            optionControl.nextBtn = nextBtn;
            optionControl.caption = captionText;

            //Set Component Value
            Color imgColor = new Color(0, 0, 0, 0.6f);
            captionImage.sprite = resources.standard;
            captionImage.color = imgColor;
            captionImage.type = Image.Type.Sliced;
            prevBtn.image.sprite = resources.standard;
            prevBtn.image.color = imgColor;
            nextBtn.image.sprite = resources.standard;
            nextBtn.image.color = imgColor;

            prevText.text = "Prev";
            prevText.color = Color.white;
            prevText.fontSize = 20;
            nextText.text = "Next";
            nextText.color = Color.white;
            nextText.fontSize = 20;
            captionText.color = Color.white;
            captionText.alignment = TextAnchor.MiddleCenter;
            captionText.fontSize = 20;

            return root;
        }
    }
}
