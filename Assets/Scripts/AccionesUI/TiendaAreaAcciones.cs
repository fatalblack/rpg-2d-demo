using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class TiendaAreaAcciones : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	// variables privadas
	GameObject EspacioTienda;
	GameObject Tooltip;

	void Start()
	{
		EspacioTienda = GetComponentInParent<Transform>().gameObject.transform.parent.gameObject;
		Tooltip = EspacioTienda.GetComponentsInChildren<RectTransform>().FirstOrDefault(objeto => objeto.CompareTag(Tags.Tooltip))?.gameObject;

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
			AccionarEspacioTienda();
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

	private void AccionarEspacioTienda()
	{
		// obtenemos el índice que le corresponde a nuestro espacio en EspaciosTienda
		int indiceEspacioTienda = EspacioTienda.transform.GetSiblingIndex();

		// procedemos a comprar el objeto y actualizar el inventario del jugador
		bool compro = PersonajeTiendaAcciones.ComprarItemPorIndice(indiceEspacioTienda, 1);

		if (compro)
		{
			// reproducimos el audio de la acción de comprar
			GameManager.Instance.reproductorPanel.ReproducirVenderComprar();
		}
		else
		{
			// reproducimos el audio de la acción de error
			GameManager.Instance.reproductorPanel.ReproducirError();
		}
	}
}