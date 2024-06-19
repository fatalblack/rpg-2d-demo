using System.Collections.Generic;

public static class TiendaAcciones
{
    public static List<Objeto> ObtenerStockTienda()
    {
        // retornamos la lista de objetos a vender en la tienda
        return new List<Objeto>
		{
			CreadorObjetos.CrearPocionVidaMenor(),
			CreadorObjetos.CrearPocionVida(),
			CreadorObjetos.CrearKitTala(),
			CreadorObjetos.CrearKitMineria(),
			CreadorObjetos.CrearSetNovato(),
			CreadorObjetos.CrearSetGuerrero(),
			CreadorObjetos.CrearSetPaladin(),
			CreadorObjetos.CrearSetMatadragones()
		};
	}
}