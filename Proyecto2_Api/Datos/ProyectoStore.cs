using Proyecto2_Api.Modelo.NewFolder;
using static System.Net.WebRequestMethods;

namespace Proyecto2_Api.Datos
{
    public static class ProyectoStore
    {
      public static List<ProyectoDto> ProyectoList = new List<ProyectoDto>
    {   new ProyectoDto{Id=1, Nombre="Vista a la Piscina", Cantidades=2, MetrosCuadrados=150},
        new ProyectoDto{Id=2,Nombre="Vista a la Playa", Cantidades=3, MetrosCuadrados=180},
        new ProyectoDto{Id=3,Nombre="Vista al cerro", Cantidades=10, MetrosCuadrados=50},
        new ProyectoDto{Id=4,Nombre="Vista al parque", Cantidades=1, MetrosCuadrados=80}
    };
    }
}
