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
using TgcViewer.Utils.Terrain;
using AlumnoEjemplos.BATTLE_SHIP.Utils;
using AlumnoEjemplos.BATTLE_SHIP.Espacio.Terreno;
using AlumnoEjemplos.BATTLE_SHIP.IA;

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
            return "Grupo 99 - BATTLE_SHIP";
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
            //Device de DirectX para crear primitivas
            GuiController.Instance.Logger.log("");
            GuiController.Instance.Logger.log("Cargando DirectX 3D Device");
            Microsoft.DirectX.Direct3D.Device d3dDevice = GuiController.Instance.D3dDevice;

            //Carpeta de archivos Media del alumno
            string alumnoMediaFolder = GuiController.Instance.AlumnoEjemplosMediaDir;

            // TgcViewer.Utils
            GuiController.Instance.Logger.log("Cargando TgcViewer.Utils");
            pickingRay = new TgcPickingRay();
            TgcSceneLoader loader = new TgcSceneLoader();

            // Se crea el manejador principal de elementos
            GuiController.Instance.Logger.log("Cargando Administrador de Elementos");
            managerPrincipal = new ElementosManager();

            // Panel de usuario
            GuiController.Instance.Logger.log("Cargando Panel de Usuario");
            panel = new PanelUsuario();
            
            GuiController.Instance.Logger.log("Cargando Factorys");
            creadorDeNaves = new NaveFactory(managerPrincipal, loader);
            creadorDeObjetosEspaciales = new EspacioFactory(managerPrincipal, loader);

            GuiController.Instance.Logger.log("Creado Nave Principal");
            navePrincipal = creadorDeNaves.CrearNavePrincipal();

            // Enemigos
            GuiController.Instance.Logger.log("Creado Naves Enemigas");

            var enemigo1 = creadorDeNaves.CrearNaveEnemiga1(new Vector3(1000, 100, 700));
            var enemigo2 = creadorDeNaves.CrearNaveEnemiga1(new Vector3(1000, -100, 700));
            var enemigo3 = creadorDeNaves.CrearNaveEnemiga1(new Vector3(1000, 50, 700));
            var enemigo4 = creadorDeNaves.CrearNaveEnemiga1(new Vector3(-1000, -50, 700));
            var enemigoCapo = creadorDeNaves.CrearNaveEnemigaVIP(new Vector3(1400, 0, 500));
            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(1000, 200, 300));
            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(1000, 0, 700));
            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(1000, 0, 300));
            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(1000, 100, 500));

            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(1400, 100, 700));
            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(1400, 100, 300));
            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(1400, 200, 500));
            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(1400, 0, 500));

            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(1700, 100, 600));
            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(1700, 100, 400));

            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(2000, 100, 500));

            //creadorDeNaves.CrearNaveEnemigaVIP(new Vector3(1400, 100, 500));
            
            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(200,   50, 1500));
            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(100,  100, 1500));
            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(200,  150, 1500));
            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(100,  200, 1500));
            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(200,  250, 1500));
            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(100,  300, 1500));
            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(200,  350, 1500));
            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(100,  400, 1500));

            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(200,    0, 1500));

            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(200, - 50, 1500));
            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(100, -100, 1500));
            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(200, -150, 1500));
            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(100, -200, 1500));
            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(200, -250, 1500));
            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(100, -300, 1500));
            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(200, -350, 1500));
            //creadorDeNaves.CrearNaveEnemiga1(new Vector3(100, -400, 1500));


            GuiController.Instance.Logger.log("Creado Objetos del Espacio");
            creadorDeObjetosEspaciales.CrearSol(d3dDevice);

            // IA
            GuiController.Instance.Logger.log("Creado IA");
            comandanteMalo = new IAManager();
            comandanteMalo.AddObjetivo(navePrincipal);

            var recorrido1 = new List<Vector3>();
            recorrido1.Add(new Vector3(500f, 0f, 2500f));
            recorrido1.Add(new Vector3(500f, 0f, 3500f));
            recorrido1.Add(new Vector3(-600f, 0f, 3500f));
            recorrido1.Add(new Vector3(-600f, 0f, 2500f));
            recorrido1.Add(new Vector3(-200f, 0f, 1800f));

            comandanteMalo.AddNave(enemigoCapo, recorrido1);

            recorrido1 = new List<Vector3>();
            recorrido1.Add(new Vector3(5000f, 0f, 1500f));
            recorrido1.Add(new Vector3(-5000f, 0f, 1500f));

            comandanteMalo.AddNave(enemigo1, recorrido1);

            recorrido1 = new List<Vector3>();
            recorrido1.Add(new Vector3(4000f, 0f, 1500f));
            recorrido1.Add(new Vector3(3000f, 0f, 2500f));
            recorrido1.Add(new Vector3(1000f, 0f, 2500f));
            recorrido1.Add(new Vector3(0f, 0f, 1000f));
            recorrido1.Add(new Vector3(-2000f, 0f, 1000f));

            comandanteMalo.AddNave(enemigo2, recorrido1);
            comandanteMalo.AddNave(enemigo3, recorrido1);

            recorrido1 = new List<Vector3>();
            recorrido1.Add(new Vector3(-800f, 0f, 1500f));
            recorrido1.Add(new Vector3(800f, 0f, 1500f));
            recorrido1.Add(new Vector3(800f, 0f, 1000f));
            recorrido1.Add(new Vector3(-800f, 0f, 1000f));

            comandanteMalo.AddNave(enemigo4, recorrido1);

            //Posicion inicial de la camara
            GuiController.Instance.Logger.log("Cargando C�mara");
            camara = new CamaraUsuario(navePrincipal);

            //Color de fondo
            GuiController.Instance.Logger.log("Cargando Fondo");
            GuiController.Instance.BackgroundColor = Color.Red;

            //Crear SkyBox 
            GuiController.Instance.Logger.log("Cargando SkyBox");
            skyBox = new BSSkyBox(camara.GetPosition(), alumnoMediaFolder);

            //Listo
            GuiController.Instance.Logger.log("Listo!", Color.Green);
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

            // Actualizar todos los elementos
            comandanteMalo.Actualizar(elapsedTime);
            managerPrincipal.Actualizar(elapsedTime);
            camara.Actualizar();
            managerPrincipal.ActualizarListaDeObjetosInteractivos();

            // Render
            managerPrincipal.RenderAll();

            // Terreno
            skyBox.Actualizar(camara.GetPosition());
            skyBox.render();

            // Panel
            panel.render(managerPrincipal.NavePrincipal, managerPrincipal.NavesEnemigas);
        }

        /// <summary>
        /// M�todo que se llama cuando termina la ejecuci�n del ejemplo.
        /// Hacer dispose() de todos los objetos creados.
        /// </summary>
        public override void close()
        {
            //TODO: dispose de todo!!!
        }


        public Nave navePrincipal { get; set; }

        public NaveFactory creadorDeNaves { get; set; }

        internal CamaraUsuario camara { get; set; }

        TgcPickingRay pickingRay;

        private ElementosManager managerPrincipal;
        private EspacioFactory creadorDeObjetosEspaciales;

        public PanelUsuario panel { get; set; }

        public ISkyBox skyBox { get; set; }

        public IAManager comandanteMalo { get; set; }
    }
}
