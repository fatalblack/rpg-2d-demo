using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventarioAreaAcciones : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	// variables privadas
	GameObject EspacioInventario;
	GameObject Tooltip;

	void Start()
	{
		EspacioInventario = GetComponentInParent<Transform>().gameObject.transform.parent.gameObject;
		Tooltip = EspacioInventario.GetComponentsInChildren<RectTransform>().FirstOrDefault(objeto => objeto.CompareTag(Tags.Tooltip))?.gameObject;

		OcultarTooltip();
	}
	
	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		MostrarTooltip();
	}

	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		OcultarTooltip();
	}

	void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
	{
		// si la acción es un click derecho procedemos, de lo contrario no realizamos acción alguna
		if (eventData.button == PointerEventData.InputButton.Right)
		{
			AccionarEspacioInventario();
		}
	}

	private void MostrarTooltip()
	{
		// Mostramos el tooltip
		if (Tooltip != null)
		{
			Tooltip.SetActive(true);
		}
	}

	private void OcultarTooltip()
	{
		// Ocultamos el tooltip
		if (Tooltip != null)
		{
			Tooltip.SetActive(false);
		}
	}

	private void AccionarEspacioInventario()
	{
		// obtenemos el índice que le corresponde a nuestro espacio en EspaciosInventario
		int indiceEspacioInventario = EspacioInventario.transform.GetSiblingIndex();
		// obtenemos el espacio equivalente en el GameManager
		PersonajeInventario personajeInventario = PersonajeInventarioAcciones.ObtenerPersonajeInventarioPorIndice(indiceEspacioInventario);

		// si no se encontró dicho espacio, no hacemos nada
		if (personajeInventario == null)
		{
			return;
		}

		// tenemos 2 tipos de acciones comerciales y efecto:
		// comerciales (si está abierta la tienda y el inventario)
		if (GameManager.Instance.PuedeVender)
		{
			bool vendio = PersonajeTiendaAcciones.VenderObjetoInventario(personajeInventario.Id);

			if (vendio)
			{
				// reproducimos el audio de la acción de vender
				GameManager.Instance.reproductorPanel.ReproducirVenderComprar();
			}
			else
			{
				// reproducimos el audio de la acción de error
				GameManager.Instance.reproductorPanel.ReproducirError();
			}
		}
		// las de efecto (solo inventario abierto)
		else
		{
			// evaluamos si el objeto es consumible o equipable
			if (personajeInventario.Objeto.Caracteristica.Equipable)
			{
				// reproducimos el audio de la acción de equipar
				GameManager.Instance.reproductorPanel.ReproducirEquiparItem();

				// equipamos
				PersonajeInventarioAcciones.EquiparObjetoInventario(personajeInventario.Id);
			}

			if (personajeInventario.Objeto.Caracteristica.Consumible)
			{
				// reproducimos el audio de la acción de consumir
				GameManager.Instance.reproductorPanel.ReproducirUsarItem();

				// consumimos
				PersonajeInventarioAcciones.ConsumirCuraPersonajeInventario(personajeInventario.Id);
			}
		}
	}
}