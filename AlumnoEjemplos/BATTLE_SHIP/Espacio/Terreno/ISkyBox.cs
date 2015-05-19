using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlumnoEjemplos.BATTLE_SHIP.Espacio.Terreno
{
    public interface ISkyBox
    {
        void render();

        void Actualizar(Vector3 centro);
    }
}
