using AlumnoEjemplos.BATTLE_SHIP.Naves;
using AlumnoEjemplos.BATTLE_SHIP.Usuario.Panel;
using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TgcViewer;
using TgcViewer.Utils._2D;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.BATTLE_SHIP.Usuario
{
    public class PanelUsuario
    {
        private Radar radar;

        public PanelUsuario() 
        {

            ////Crear Sprite del radar
            //radarEscala = 0.6f;
            //margen = 30;
            //radarBase = new TgcSprite();
            //radarBase.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosMediaDir + "\\Panel\\Radar\\Radar256x256.png");
            //radarBase.Scaling = new Vector2(radarEscala, radarEscala);
            ////Ubicarlo
            //Size screenSize = GuiController.Instance.Panel3d.Size;
            //Size textureSize = radarBase.Texture.Size;
            //radarBase.Position = new Vector2(FastMath.Max(screenSize.Width - textureSize.Width * radarEscala - margen, 0), FastMath.Max(screenSize.Height - textureSize.Height * radarEscala - margen, 0));

            radar = new Radar(0.03f);
        }

        public void render(Nave usuario, List<Nave> enemigos) 
        {
            //Iniciar dibujado de todos los Sprites de la escena (en este caso es solo uno)
            GuiController.Instance.Drawer2D.beginDrawSprite();

            //Dibujar sprite (si hubiese mas, deberian ir todos aquí)
            radar.render(usuario, enemigos);

            //Finalizar el dibujado de Sprites
            GuiController.Instance.Drawer2D.endDrawSprite();
        }
    }
}
