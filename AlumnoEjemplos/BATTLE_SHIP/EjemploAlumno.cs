using System;
using System.Collections.Generic;
using System.Text;
using TgcViewer.Example;
using TgcViewer;
using Microsoft.DirectX.Direct3D;
using System.Drawing;
using Microsoft.DirectX;
using TgcViewer.Utils.Modifiers;
using AlumnoEjemplos.BATTLE_SHIP.Naves;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.Input;
using Microsoft.DirectX.DirectInput;
using AlumnoEjemplos.BATTLE_SHIP.Usuario;
using AlumnoEjemplos.BATTLE_SHIP.Elementos;
using AlumnoEjemplos.BATTLE_SHIP.Espacio;

namespace AlumnoEjemplos.BATTLE_SHIP
{
    /// <summary>
    /// Ejemplo del alumno
    /// </summary>
    public class EjemploAlumno : TgcExample
    {
        /// <summary>
        /// Categor�a a la que pertenece el ejemplo.
        /// Influye en donde se va a haber en el �rbol de la derecha de la pantalla.
        /// </summary>
        public override string getCategory()
        {
            return "AlumnoEjemplos";
        }

        /// <summary>
        /// Completar nombre del grupo en formato Grupo NN
        /// </summary>
        public override string getName()
        {
            return "Grupo 99 - BATTLE_SHIP .algo";
        }

        /// <summary>
        /// Completar con la descripci�n del TP
        /// </summary>
        public override string getDescription()
        {
            return "MiIdea - Descripcion de la idea";
        }

        /// <summary>
        /// M�todo que se llama una sola vez,  al principio cuando se ejecuta el ejemplo.
        /// Escribir aqu� todo el c�digo de inicializaci�n: cargar modelos, texturas, modifiers, uservars, etc.
        /// Borrar todo lo que no haga falta
        /// </summary>
        public override void init()
        {
            //GuiController.Instance: acceso principal a todas las herramientas del Framework

            //Device de DirectX para crear primitivas
            Microsoft.DirectX.Direct3D.Device d3dDevice = GuiController.Instance.D3dDevice;

            //Carpeta de archivos Media del alumno
            string alumnoMediaFolder = GuiController.Instance.AlumnoEjemplosMediaDir;

            pickingRay = new TgcPickingRay();
            TgcSceneLoader loader = new TgcSceneLoader();

            managerPrincipal = new ElementosManager();

            panel = new PanelUsuario();

            creadorDeNaves = new NaveFactory(managerPrincipal, loader);
            creadorDeObjetosEspaciales = new EspacioFactory(managerPrincipal, loader);

            navePrincipal = creadorDeNaves.CrearNavePrincipal();

            //Cargar obstaculos y posicionarlos. Los obst�culos se crean con TgcBox en lugar de cargar un modelo.
            TgcBox obstaculo;


            //Obstaculo 1
            obstaculo = TgcBox.fromSize(
                new Vector3(-100, 0, 0),
                new Vector3(80, 150, 80),
                TgcTexture.createTexture(d3dDevice, GuiController.Instance.ExamplesMediaDir + "Texturas\\baldosaFacultad.jpg"));
            managerPrincipal.Add(obstaculo);

            //Obstaculo 2
            obstaculo = TgcBox.fromSize(
                new Vector3(50, 0, 200),
                new Vector3(80, 300, 80),
                TgcTexture.createTexture(d3dDevice, GuiController.Instance.ExamplesMediaDir + "Texturas\\madera.jpg"));
            managerPrincipal.Add(obstaculo);

            //Obstaculo 3
            obstaculo = TgcBox.fromSize(
                new Vector3(300, 0, 100),
                new Vector3(80, 100, 150),
                TgcTexture.createTexture(d3dDevice, GuiController.Instance.ExamplesMediaDir + "Texturas\\granito.jpg"));
            managerPrincipal.Add(obstaculo);

            creadorDeNaves.CrearNaveEnemiga1(new Vector3(1000, 200, 700));
            creadorDeNaves.CrearNaveEnemiga1(new Vector3(1000, 200, 300));
            creadorDeNaves.CrearNaveEnemiga1(new Vector3(1000, 0, 700));
            creadorDeNaves.CrearNaveEnemiga1(new Vector3(1000, 0, 300));
            creadorDeNaves.CrearNaveEnemiga1(new Vector3(1000, 100, 500));

            creadorDeNaves.CrearNaveEnemiga1(new Vector3(1400, 100, 700));
            creadorDeNaves.CrearNaveEnemiga1(new Vector3(1400, 100, 300));
            creadorDeNaves.CrearNaveEnemiga1(new Vector3(1400, 200, 500));
            creadorDeNaves.CrearNaveEnemiga1(new Vector3(1400, 0, 500));

            creadorDeNaves.CrearNaveEnemiga1(new Vector3(1700, 100, 600));
            creadorDeNaves.CrearNaveEnemiga1(new Vector3(1700, 100, 400));

            creadorDeNaves.CrearNaveEnemiga1(new Vector3(2000, 100, 500));

            creadorDeNaves.CrearNaveEnemigaVIP(new Vector3(1400, 100, 500));

            creadorDeObjetosEspaciales.CrearSol(d3dDevice);

            //Posicion inicial de la camara
            camara = new CamaraUsuario(navePrincipal);

            //Color de fondo
            GuiController.Instance.BackgroundColor = Color.Black;

        }


        /// <summary>
        /// M�todo que se llama cada vez que hay que refrescar la pantalla.
        /// Escribir aqu� todo el c�digo referido al renderizado.
        /// Borrar todo lo que no haga falta
        /// </summary>
        /// <param name="elapsedTime">Tiempo en segundos transcurridos desde el �ltimo frame</param>
        public override void render(float elapsedTime)
        {
            //Device de DirectX para renderizar
            Microsoft.DirectX.Direct3D.Device d3dDevice = GuiController.Instance.D3dDevice;

            // Calcular interaccion de usuario
            ControlUsuario.CampurarInteraccionDeUsuario(GuiController.Instance.D3dInput, pickingRay, managerPrincipal);

            // Actualizar todo
            managerPrincipal.Actualizar(elapsedTime);
            camara.Actualizar();
            managerPrincipal.ActualizarListaDeObjetosInteractivos();

            // Render
            managerPrincipal.RenderAll();

            // Panel
            panel.render(managerPrincipal.NavePrincipal, managerPrincipal.NavesEnemigas);

        }

        /// <summary>
        /// M�todo que se llama cuando termina la ejecuci�n del ejemplo.
        /// Hacer dispose() de todos los objetos creados.
        /// </summary>
        public override void close()
        {

        }


        public Nave navePrincipal { get; set; }

        public NaveFactory creadorDeNaves { get; set; }

        internal CamaraUsuario camara { get; set; }

        TgcPickingRay pickingRay;

        private ElementosManager managerPrincipal;
        private EspacioFactory creadorDeObjetosEspaciales;

        public PanelUsuario panel { get; set; }
    }
}
