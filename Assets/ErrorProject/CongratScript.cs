using System.Collections.Generic;
using UnityEngine;

namespace ErrorProject
{
    public class CongratScript : MonoBehaviour
    {
        public TextMesh Text;
        public ParticleSystem SparksParticles;

        [SerializeField] private List<string> TextToDisplay = new List<string>();

        [SerializeField] private float RotatingSpeed = 1.0f;
        private float TimeToNextText;

        private int CurrentText;

        // Start is called before the first frame update
        void Start()
        {
            TimeToNextText = 0.0f;
            CurrentText = 0;

            TextToDisplay.Add("Congratulation");
            TextToDisplay.Add("All Errors Fixed");

            Text.text = TextToDisplay[0];

            SparksParticles.Play();
        }

        // Update is called once per frame
        void Update()
        {
            TimeToNextText += Time.deltaTime;
            Text.transform.Rotate(new Vector3(0.3f, 0.8f, 0.5f), RotatingSpeed * Time.deltaTime);

            if (TimeToNextText > 1.5f)
            {
                TimeToNextText = 0.0f;

                CurrentText++;
                if (CurrentText >= TextToDisplay.Count)
                {
                    CurrentText = 0;
                }
                Text.text = TextToDisplay[CurrentText];
            }
        }
    }
}