﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap.Unity.Interaction;

// Se indica que es requerido que exista este componente
[RequireComponent(typeof(InteractionBehaviour))]
public class HoverGlow : MonoBehaviour {
    [Header("Activacion del Glow")]
    [Tooltip("Si está activado, cambiará de color el objeto en función de la distancia de la mano")]
    public bool useHover = true;

    [Tooltip("Si está activado, el objeto utilizará su primaryHoverColor cuando se acerque la mano")]
    public bool usePrimaryHover = true;

    [Header("Colores")]
    // Hacemos una interpolación lineal entre dos colores para conseguir un color intermedio
    // - en este caso concreto un gris muy oscuro
    public Color defaultColor = Color.Lerp(Color.black, Color.white, 0.1f);
    // - en este caso concreto un gris más claro
    public Color hoverColor = Color.Lerp(Color.black, Color.white, 0.7f);
    // - en este caso concreto un gris muy claro
    public Color primaryHoverColor = Color.Lerp(Color.black, Color.white, 0.8f);
    // Suavizado del cambio de color
    public float smoothColor = 5f;

    // Referencia al material de objeto
    private Material material;
    // Referencia al script InteractionBehaviour
    private InteractionBehaviour intObj;


    // Use this for initialization
    void Start () {
        // Recuperamos la referencia al componente InteractionBehaviour
        intObj = GetComponent<InteractionBehaviour>();

        // Intentamos recuperar el componente Renderer (a partir de Renderer vamos a
        // obtener el materia del objeto)
        Renderer renderer = GetComponent<Renderer>();

        // Si no existe el componente Renderer en el objeto, lo buscamos en sus hijos
        if (renderer == null)
        {
            renderer = GetComponentInChildren<Renderer>();
        }

        // Si hemos encontrado un Renderer, recuperamos el material
        if (renderer != null)
        {
            material = renderer.material;
        }
	}
	
	// Update is called once per frame
	void Update () {
		// Verificamos si existe la referencia al material
        if (material != null)
        {
            Color targetColor = defaultColor;

            // Si el evento detectado es primaryHover, el color objetivo será el definido como primaryHover
            if (intObj.isPrimaryHovered && usePrimaryHover)
            {
                targetColor = primaryHoverColor;
            }
            else
            {
                // Si el evento detectado es el hover, el color objetivo será el definido como hoverColor
                if (intObj.isHovered && useHover)
                {
                    // Calculamos el glow en función de la distancia a la palma de la mano
                    // convertimos con un Map el valor de 0 a 0.2 de forma proporciona de 1 a 0
                    float glow = intObj.closestHoveringControllerDistance.Map(0f, 0.2f, 1f, 0.0f);

                    targetColor = Color.Lerp(defaultColor, hoverColor, glow);
                }
            }

            // Interpolacion lineal del color inicial al color objetivo
            material.color = Color.Lerp(material.color, targetColor, smoothColor * Time.deltaTime);
        }
	}
}
