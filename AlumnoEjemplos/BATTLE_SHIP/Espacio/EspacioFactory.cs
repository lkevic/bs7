using AlumnoEjemplos.BATTLE_SHIP.Elementos;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.BATTLE_SHIP.Espacio
{
    public class EspacioFactory
    {
        private ElementosManager ManagerTGC { get; set; }
        private TgcSceneLoader tgcLoader { get; set; }

        public EspacioFactory(ElementosManager managerTGC, TgcSceneLoader loader) 
        {
            ManagerTGC = managerTGC;
            tgcLoader = loader;
        }

        public void CrearSol(Device d3dDevice)
        {
            string sphere = GuiController.Instance.ExamplesMediaDir + "ModelosTgc\\Sphere\\Sphere-TgcScene.xml";
            
            var sun = new Sol("Sol",
                        tgcLoader.loadSceneFromFile(sphere).Meshes[0],
                        new Vector3(-200f, 0f, 5000f),
                        new Vector3(0f, 0f, 0f),
                        new Vector3(10f, 10f, 10f));

            sun.changeDiffuseMaps(new TgcTexture[] { TgcTexture.createTexture(d3dDevice, GuiController.Instance.ExamplesDir + "Transformations\\SistemaSolar\\SunTexture.jpg") });

            ManagerTGC.Add(sun);   
        }
    }
}
