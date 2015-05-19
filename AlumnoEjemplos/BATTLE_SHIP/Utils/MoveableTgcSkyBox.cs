using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer.Utils.Terrain;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.BATTLE_SHIP.Utils
{
    public class MoveableTgcSkyBox : TgcSkyBox
    {
        private Vector3 UltimoCentro;

        public MoveableTgcSkyBox(Vector3 centroInicial) : 
            base() 
        {
            Center = centroInicial;
            UltimoCentro = Center;
        }

        public void ActualizarCentro(Vector3 nuevaPosicion)
        {
            Vector3 desplazamiento = (nuevaPosicion - UltimoCentro);

            foreach (TgcMesh face in this.Faces)
            {
                face.move(desplazamiento);
            }

            UltimoCentro = nuevaPosicion;
        }
    }
}
