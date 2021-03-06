﻿using AlumnoEjemplos.BATTLE_SHIP.Elementos;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.BATTLE_SHIP.Naves
{
    public class NaveFactory
    {
        private ElementosManager ManagerTGC { get; set; }
        private TgcSceneLoader tgcLoader { get; set; }

        public NaveFactory(ElementosManager managerTGC, TgcSceneLoader loader) 
        {
            ManagerTGC = managerTGC;
            tgcLoader = loader;
        }

        public Nave CrearNavePrincipal() 
        {
            float escala = 0.5f;
            var filePath = GuiController.Instance.ExamplesMediaDir + "MeshCreator\\Meshes\\Vehiculos\\NaveEspacial\\NaveEspacial-TgcScene.xml";
            //var filePath = GuiController.Instance.ExamplesMediaDir + "MeshCreator\\Meshes\\Vehiculos\\StarWars-Speeder\\StarWars-Speeder-TgcScene.xml";
            //Cargar nave
            TgcSceneLoader loader = new TgcSceneLoader();
            TgcScene scene = loader.loadSceneFromFile(filePath);

            var nave = new Nave("Enterprice", 
                scene.Meshes[0], 
                new Vector3(0f, 0f, 0f), 
                new Vector3(0f, Geometry.DegreeToRadian(0f), 0f), 
                new Vector3(escala, escala, escala),
                new Vector3(0f, Geometry.DegreeToRadian(180f), 0f),
                //new Vector3(0f, Geometry.DegreeToRadian(270f), 0f),
                250f,
                70f,
                2000000);

            ManagerTGC.AddNavePrincipal(nave);

            return nave;
        }

        public Nave CrearNaveEnemiga1(Vector3 pos)
        {
            float escala = 1f;
            var filePath = GuiController.Instance.ExamplesMediaDir + "MeshCreator\\Meshes\\Vehiculos\\StarWars-Speeder\\StarWars-Speeder-TgcScene.xml";
            //Cargar nave
            TgcSceneLoader loader = new TgcSceneLoader();
            TgcScene scene = loader.loadSceneFromFile(filePath);

            var nave = new Nave("Malo1",
                scene.Meshes[0],
                pos,
                //new Vector3(1000, 200, 500),
                new Vector3(0f, Geometry.DegreeToRadian(45f), 0f),
                new Vector3(escala, escala, escala),
                new Vector3(0f, Geometry.DegreeToRadian(270f), 0f),
                150f,
                40f,
                2000);

            ManagerTGC.Add(nave);

            return nave;
        }

        public Nave CrearNaveEnemigaVIP(Vector3 pos)
        {
            float escala = 2f;
            var filePath = GuiController.Instance.ExamplesMediaDir + "MeshCreator\\Meshes\\Vehiculos\\StarWars-YWing\\StarWars-YWing-TgcScene.xml";
            //Cargar nave
            TgcSceneLoader loader = new TgcSceneLoader();
            TgcScene scene = loader.loadSceneFromFile(filePath);

            var nave = new Nave("MaloVIP",
                scene.Meshes[0],
                pos,
                new Vector3(0f, Geometry.DegreeToRadian(45f), 0f),
                new Vector3(escala, escala, escala),
                new Vector3(0f, Geometry.DegreeToRadian(270f), 0f),
                180,
                40f,
                4000);

            ManagerTGC.Add(nave);

            return nave;
        }
    }
}
