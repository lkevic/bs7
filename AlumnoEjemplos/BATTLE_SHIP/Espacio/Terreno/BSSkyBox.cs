using AlumnoEjemplos.BATTLE_SHIP.Utils;
using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer.Utils.Terrain;

namespace AlumnoEjemplos.BATTLE_SHIP.Espacio.Terreno
{
    public class BSSkyBox : ISkyBox
    {
        private MoveableTgcSkyBox skyBox;

        public BSSkyBox(Vector3 centro, string alumnoMediaFolder) 
        {
            //Crear SkyBox 
            skyBox = new MoveableTgcSkyBox(centro);
            skyBox.Size = new Vector3(11500, 11500, 11500);

            //Configurar color
            //skyBox.Color = Color.OrangeRed;

            //Configurar las texturas para cada una de las 6 caras
            skyBox.setFaceTexture(TgcSkyBox.SkyFaces.Up, alumnoMediaFolder + @"BATTLE_SHIP\Texturas\SkyBox\techo.jpg");
            skyBox.setFaceTexture(TgcSkyBox.SkyFaces.Down, alumnoMediaFolder + @"BATTLE_SHIP\Texturas\SkyBox\piso.jpg");
            skyBox.setFaceTexture(TgcSkyBox.SkyFaces.Left, alumnoMediaFolder + @"BATTLE_SHIP\Texturas\SkyBox\lat1.jpg");
            skyBox.setFaceTexture(TgcSkyBox.SkyFaces.Right, alumnoMediaFolder + @"BATTLE_SHIP\Texturas\SkyBox\lat2.jpg");
            skyBox.setFaceTexture(TgcSkyBox.SkyFaces.Front, alumnoMediaFolder + @"BATTLE_SHIP\Texturas\SkyBox\lat3.jpg");
            skyBox.setFaceTexture(TgcSkyBox.SkyFaces.Back, alumnoMediaFolder + @"BATTLE_SHIP\Texturas\SkyBox\lat4.jpg");

            //Actualizar todos los valores para crear el SkyBox
            skyBox.updateValues();
        }

        public void render()
        {
            skyBox.render();
        }

        public void Actualizar(Vector3 centro) 
        {
            skyBox.ActualizarCentro(centro);
        }
    }
}
