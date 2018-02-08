using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour {

	public float startSliderMoveSpeed = 3; // The speed for the slider to initially fill up
	public float sliderMoveSpeed = 2; // The speed at which the slider moves to its target position
	public Health health; // The health script from which to track the health
	public Color fullColor; // The color of the slider when it is full
	public Color halfColor; // The color of the slider when it is at half
	public Color emptyColor; // The color of the slider when it is empty

	private Slider slider; // Reference to health slider
	private Image fillImage; // Reference to the fill image on the slider
	private float currentSliderMoveSpeed; // The current speed the slider moves at
	private bool sliderUpdating = true; // Whether the slider should be moving

	// Use this for initialization
	void Awake () {
		slider = GetComponent<Slider> (); // Getting reference
		fillImage = transform.Find ("Fill Area/Fill").GetComponent<Image> (); // Getting the reference
		slider.maxValue = health.startHealth; // Setting initial value
		currentSliderMoveSpeed = startSliderMoveSpeed; // Setting the slider move speed
	}
	
	// Update is called once per frame
	void Update () {
		if (health.healthChanged) { // If the health has changed
			if (currentSliderMoveSpeed == startSliderMoveSpeed) { // If the slider move speed is the start movespeed
				currentSliderMoveSpeed = sliderMoveSpeed; // Sets the speed to the default move speed
			}
			sliderUpdating = true; // Setting the bool
			health.healthChanged = false; // Setting the bool
		}
		if (sliderUpdating) { // If the slider is updating
			MoveSlider ();
		}
	}

	void MoveSlider () { // Moves the slider and sets the colors based on the value
		slider.value = Mathf.MoveTowards(slider.value, health.currentHealth, currentSliderMoveSpeed * Time.deltaTime); // Setting the slider to match the current health
		if (slider.value > slider.maxValue / 2) { // If the value is over half
			fillImage.color = Color.Lerp (halfColor, fullColor, (slider.value - (slider.maxValue / 2)) / (slider.value / 2)); // Sets the color of the image
		} else { // If the value is under half
			fillImage.color = Color.Lerp (emptyColor, halfColor, slider.value / (slider.maxValue / 2)); // Sets the color of the image
		}
		if (slider.value == health.currentHealth) { // If the value has reached the health
			sliderUpdating = false; // Setting the bool
		}
	}
}
