using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace OTTER
{
    /// <summary>
    /// -
    /// </summary>
    public partial class BGL : Form
    {

        /* ------------------- */
        #region Environment Variables

        List<Func<int>> GreenFlagScripts = new List<Func<int>>();

        /// <summary>
        /// Uvjet izvršavanja igre. Ako je <c>START == true</c> igra će se izvršavati.
        /// </summary>
        /// <example><c>START</c> se često koristi za beskonačnu petlju. Primjer metode/skripte:
        /// <code>
        /// private int MojaMetoda()
        /// {
        ///     while(START)
        ///     {
        ///       //ovdje ide kod
        ///     }
        ///     return 0;
        /// }</code>
        /// </example>
        public static bool START = true;

        //sprites
        /// <summary>
        /// Broj likova.
        /// </summary>
        public static int spriteCount = 0, soundCount = 0;

        /// <summary>
        /// Lista svih likova.
        /// </summary>
        //public static List<Sprite> allSprites = new List<Sprite>();
        public static SpriteList<Sprite> allSprites = new SpriteList<Sprite>();

        //sensing
        int mouseX, mouseY;
        Sensing sensing = new Sensing();

        //background
        List<string> backgroundImages = new List<string>();
        int backgroundImageIndex = 0;
        string ISPIS = "";

        SoundPlayer[] sounds = new SoundPlayer[1000];
        TextReader[] readFiles = new StreamReader[1000];
        TextWriter[] writeFiles = new StreamWriter[1000];
        bool showSync = false;
        int loopcount;
        DateTime dt = new DateTime();
        String time;
        double lastTime, thisTime, diff;

        #endregion
        /* ------------------- */
        #region Events

        private void Draw(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            try
            {                
                foreach (Sprite sprite in allSprites)
                {                    
                    if (sprite != null)
                        if (sprite.Show == true)
                        {
                            g.DrawImage(sprite.CurrentCostume, new Rectangle(sprite.X, sprite.Y, sprite.Width, sprite.Heigth));
                        }
                    if (allSprites.Change)
                        break;
                }
                if (allSprites.Change)
                    allSprites.Change = false;
            }
            catch
            {
                //ako se doda sprite dok crta onda se mijenja allSprites
                MessageBox.Show("Greška!");
            }
        }

        private void startTimer(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
            Init();
        }

        private void updateFrameRate(object sender, EventArgs e)
        {
            updateSyncRate();
        }

        /// <summary>
        /// Crta tekst po pozornici.
        /// </summary>
        /// <param name="sender">-</param>
        /// <param name="e">-</param>
        public void DrawTextOnScreen(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            var brush = new SolidBrush(Color.WhiteSmoke);
            string text = ISPIS;

            SizeF stringSize = new SizeF();
            Font stringFont = new Font("Arial", 14);
            stringSize = e.Graphics.MeasureString(text, stringFont);

            using (Font font1 = stringFont)
            {
                RectangleF rectF1 = new RectangleF(0, 0, stringSize.Width, stringSize.Height);
                e.Graphics.FillRectangle(brush, Rectangle.Round(rectF1));
                e.Graphics.DrawString(text, font1, Brushes.Black, rectF1);
            }
        }

        private void mouseClicked(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = true;
            sensing.MouseDown = true;
        }

        private void mouseDown(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = true;
            sensing.MouseDown = true;            
        }

        private void mouseUp(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = false;
            sensing.MouseDown = false;
        }

        private void mouseMove(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;

            //sensing.MouseX = e.X;
            //sensing.MouseY = e.Y;
            //Sensing.Mouse.x = e.X;
            //Sensing.Mouse.y = e.Y;
            sensing.Mouse.X = e.X;
            sensing.Mouse.Y = e.Y;

        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            sensing.Key = e.KeyCode.ToString();
            sensing.KeyPressedTest = true;
        }

        private void keyUp(object sender, KeyEventArgs e)
        {
            sensing.Key = "";
            sensing.KeyPressedTest = false;
        }

        private void Update(object sender, EventArgs e)
        {
            if (sensing.KeyPressed(Keys.Escape))
            {
                START = false;
            }

            if (START)
            {
                this.Refresh();
            }
        }

        #endregion
        /* ------------------- */
        #region Start of Game Methods

        //my
        #region my

        //private void StartScriptAndWait(Func<int> scriptName)
        //{
        //    Task t = Task.Factory.StartNew(scriptName);
        //    t.Wait();
        //}

        //private void StartScript(Func<int> scriptName)
        //{
        //    Task t;
        //    t = Task.Factory.StartNew(scriptName);
        //}

        private int AnimateBackground(int intervalMS)
        {
            while (START)
            {
                setBackgroundPicture(backgroundImages[backgroundImageIndex]);
                Game.WaitMS(intervalMS);
                backgroundImageIndex++;
                if (backgroundImageIndex == 3)
                    backgroundImageIndex = 0;
            }
            return 0;
        }

        private void KlikNaZastavicu()
        {
            foreach (Func<int> f in GreenFlagScripts)
            {
                Task.Factory.StartNew(f);
            }
        }

        #endregion

        /// <summary>
        /// BGL
        /// </summary>
        public BGL()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Pričekaj (pauza) u sekundama.
        /// </summary>
        /// <example>Pričekaj pola sekunde: <code>Wait(0.5);</code></example>
        /// <param name="sekunde">Realan broj.</param>
        public void Wait(double sekunde)
        {
            int ms = (int)(sekunde * 1000);
            Thread.Sleep(ms);
        }

        //private int SlucajanBroj(int min, int max)
        //{
        //    Random r = new Random();
        //    int br = r.Next(min, max + 1);
        //    return br;
        //}

        /// <summary>
        /// -
        /// </summary>
        public void Init()
        {
            if (dt == null) time = dt.TimeOfDay.ToString();
            loopcount++;
            //Load resources and level here
            this.Paint += new PaintEventHandler(DrawTextOnScreen);
            //SetupGame();
            Opening();
        }

        /// <summary>
        /// -
        /// </summary>
        /// <param name="val">-</param>
        public void showSyncRate(bool val)
        {
            showSync = val;
            if (val == true) syncRate.Show();
            if (val == false) syncRate.Hide();
        }

        /// <summary>
        /// -
        /// </summary>
        public void updateSyncRate()
        {
            if (showSync == true)
            {
                thisTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
                diff = thisTime - lastTime;
                lastTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;

                double fr = (1000 / diff) / 1000;

                int fr2 = Convert.ToInt32(fr);

                syncRate.Text = fr2.ToString();
            }

        }

        //stage
        #region Stage

        /// <summary>
        /// Postavi naslov pozornice.
        /// </summary>
        /// <param name="title">tekst koji će se ispisati na vrhu (naslovnoj traci).</param>
        public void SetStageTitle(string title)
        {
            this.Text = title;
        }

        /// <summary>
        /// Postavi boju pozadine.
        /// </summary>
        /// <param name="r">r</param>
        /// <param name="g">g</param>
        /// <param name="b">b</param>
        public void setBackgroundColor(int r, int g, int b)
        {
            this.BackColor = Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Postavi boju pozornice. <c>Color</c> je ugrađeni tip.
        /// </summary>
        /// <param name="color"></param>
        public void setBackgroundColor(Color color)
        {
            this.BackColor = color;
        }

        /// <summary>
        /// Postavi sliku pozornice.
        /// </summary>
        /// <param name="backgroundImage">Naziv (putanja) slike.</param>
        public void setBackgroundPicture(string backgroundImage)
        {
            this.BackgroundImage = new Bitmap(backgroundImage);
        }

        /// <summary>
        /// Izgled slike.
        /// </summary>
        /// <param name="layout">none, tile, stretch, center, zoom</param>
        public void setPictureLayout(string layout)
        {
            if (layout.ToLower() == "none") this.BackgroundImageLayout = ImageLayout.None;
            if (layout.ToLower() == "tile") this.BackgroundImageLayout = ImageLayout.Tile;
            if (layout.ToLower() == "stretch") this.BackgroundImageLayout = ImageLayout.Stretch;
            if (layout.ToLower() == "center") this.BackgroundImageLayout = ImageLayout.Center;
            if (layout.ToLower() == "zoom") this.BackgroundImageLayout = ImageLayout.Zoom;
        }

        #endregion

        //sound
        #region sound methods

        /// <summary>
        /// Učitaj zvuk.
        /// </summary>
        /// <param name="soundNum">-</param>
        /// <param name="file">-</param>
        public void loadSound(int soundNum, string file)
        {
            soundCount++;
            sounds[soundNum] = new SoundPlayer(file);
        }

        /// <summary>
        /// Sviraj zvuk.
        /// </summary>
        /// <param name="soundNum">-</param>
        public void playSound(int soundNum)
        {
            sounds[soundNum].Play();
        }

        /// <summary>
        /// loopSound
        /// </summary>
        /// <param name="soundNum">-</param>
        public void loopSound(int soundNum)
        {
            sounds[soundNum].PlayLooping();
        }

        /// <summary>
        /// Zaustavi zvuk.
        /// </summary>
        /// <param name="soundNum">broj</param>
        public void stopSound(int soundNum)
        {
            sounds[soundNum].Stop();
        }

        #endregion

        //file
        #region file methods

        /// <summary>
        /// Otvori datoteku za čitanje.
        /// </summary>
        /// <param name="fileName">naziv datoteke</param>
        /// <param name="fileNum">broj</param>
        public void openFileToRead(string fileName, int fileNum)
        {
            readFiles[fileNum] = new StreamReader(fileName);
        }

        /// <summary>
        /// Zatvori datoteku.
        /// </summary>
        /// <param name="fileNum">broj</param>
        public void closeFileToRead(int fileNum)
        {
            readFiles[fileNum].Close();
        }

        /// <summary>
        /// Otvori datoteku za pisanje.
        /// </summary>
        /// <param name="fileName">naziv datoteke</param>
        /// <param name="fileNum">broj</param>
        public void openFileToWrite(string fileName, int fileNum)
        {
            writeFiles[fileNum] = new StreamWriter(fileName);
        }

        /// <summary>
        /// Zatvori datoteku.
        /// </summary>
        /// <param name="fileNum">broj</param>
        public void closeFileToWrite(int fileNum)
        {
            writeFiles[fileNum].Close();
        }

        /// <summary>
        /// Zapiši liniju u datoteku.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <param name="line">linija</param>
        public void writeLine(int fileNum, string line)
        {
            writeFiles[fileNum].WriteLine(line);
        }

        /// <summary>
        /// Pročitaj liniju iz datoteke.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <returns>vraća pročitanu liniju</returns>
        public string readLine(int fileNum)
        {
            return readFiles[fileNum].ReadLine();
        }

        /// <summary>
        /// Čita sadržaj datoteke.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <returns>vraća sadržaj</returns>
        public string readFile(int fileNum)
        {
            return readFiles[fileNum].ReadToEnd();
        }

        #endregion

        //mouse & keys
        #region mouse methods

        /// <summary>
        /// Sakrij strelicu miša.
        /// </summary>
        public void hideMouse()
        {
            Cursor.Hide();
        }

        /// <summary>
        /// Pokaži strelicu miša.
        /// </summary>
        public void showMouse()
        {
            Cursor.Show();
        }

        /// <summary>
        /// Provjerava je li miš pritisnut.
        /// </summary>
        /// <returns>true/false</returns>
        public bool isMousePressed()
        {
            //return sensing.MouseDown;
            return sensing.MouseDown;
        }

        /// <summary>
        /// Provjerava je li tipka pritisnuta.
        /// </summary>
        /// <param name="key">naziv tipke</param>
        /// <returns></returns>
        public bool isKeyPressed(string key)
        {
            if (sensing.Key == key)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Provjerava je li tipka pritisnuta.
        /// </summary>
        /// <param name="key">tipka</param>
        /// <returns>true/false</returns>
        public bool isKeyPressed(Keys key)
        {
            if (sensing.Key == key.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void buttonStart_Click_1(object sender, EventArgs e)
        {
            panelStart.Visible = false;
            panelOpening.Visible = false;
            
            CreatePlayer();
            SetupGame();
        }

        private void buttonExitGame_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            panelStart.Visible = true;
            
        }
        private void buttonLoadGame_Click(object sender, EventArgs e)
        {
            panelStart.Visible = false;
            panelOpening.Visible = false;
            LoadGame();
        }

        #endregion

        #endregion
        /* ------------------- */

        /* ------------ GAME CODE START ------------ */

        /* Game variables */
        Player player = new Player("sprites\\boy000.png", 100,100);
        NPC professor, friend1, friend2, trainer, boss;
        Item desk, bed, table, bookcase, tv, karaoke, dumbbell, computer, workdesk;
        Book bKnowledge, bCharisma, bFitness, bStress;
        bool f1 = true, f2=true; //flag-ovi za provjeravanje koji je dan - ExamsStart
        string timer; int day=1;


        private void Opening() //metoda koja se prva pokreće pri pokretanju programa
        {
            panelStart.Visible = false;
            panelOpening.Visible = true;
            setBackgroundColor(Color.Maroon);

        }
        private void CreatePlayer() //metoda za stvaranje lika iz podataka s panela
        {
            if (txtName.Text == "")
            {
                if (rbMale.Checked)
                {
                    player.Name = "John";
                    player.Gender = false;
                }
                else
                {
                    player.Name = "Jane";
                    player.Gender = true;
                }
                    
            }
            else
            {
                player.Name = txtName.Text;
                if (rbMale.Checked)
                    player.Gender = false;
                else
                    player.Gender = true;
            }

            if (player.Gender)
            {
                string[] costumes = new string[]
                {"sprites\\girl000.png",
                "sprites\\girl001.png",
                "sprites\\girl002.png",
                "sprites\\girl003.png",
                "sprites\\girl004.png",
                "sprites\\girl005.png",
                "sprites\\girl006.png",
                "sprites\\girl007.png",
                "sprites\\girl008.png",
                "sprites\\girl009.png",
                "sprites\\girl010.png",
                "sprites\\girl011.png"};
                player.Costumes.RemoveAt(0);
                player.AddCostumes(costumes);                
                player.NextCostume();
            }
            else
            {
                string[] costumes = new string[]
                {"sprites\\boy000.png",
                "sprites\\boy001.png",
                "sprites\\boy002.png",
                "sprites\\boy003.png",
                "sprites\\boy004.png",
                "sprites\\boy005.png",
                "sprites\\boy006.png",
                "sprites\\boy007.png",
                "sprites\\boy008.png",
                "sprites\\boy009.png",
                "sprites\\boy010.png",
                "sprites\\boy011.png"};
                player.Costumes.RemoveAt(0);
                player.AddCostumes(costumes);
                player.NextCostume();
            }
        }

        private void SetupGame()
        {
            //1. setup stage
            SetStageTitle("University Simulator");           
            setBackgroundPicture("backgrounds\\background.jpg");
            setPictureLayout("left");

            //2. add sprites
            player.X = 0;
            player.Y = GameOptions.DownEdge/2 - player.Heigth;
            player.RotationStyle = "all around";
            player.SetHeading(0);
            player.SetSize(150);
            GameOptions.SpriteHeight = (int) Math.Ceiling(player.Heigth * 1.5);
            GameOptions.SpriteWidth = (int)Math.Ceiling(player.Width*1.5);
            
            Random r = new Random (); // skills (knowledge, charisma i fitness) postavljamo na random početne vrijednosti između 1 i 15
            player.Knowledge = r.Next(1,16);
            player.Charisma= r.Next(1, 16);
            player.Fitness = r.Next(1, 16);
            player.Energy = 80;
            player.Place = "main";

            Game.AddSprite(player);

            timer = "08:00";
            TimerUpdate(0); //postavljamo vrijeme u gornjem lijevom kutu

            //3. scripts that start
            Game.StartScript(PlayerMove);          

        } //nakon Start, ovom metodom se postavlja igra

        private void SetupGame2()
        {
            allSprites.RemoveRange(1, allSprites.Count() - 1); //brišemo sve sprite-ove osim prvog - glavnog lika
            setBackgroundPicture("backgrounds\\transition.jpg");
            player.SetVisible(false);

            Wait(0.2);
            setBackgroundPicture("backgrounds\\background.jpg");
            int y = GameOptions.DownEdge / 2 - GameOptions.SpriteHeight;

            //kad player izađe iz sobe, želimo da se stvori ispred te sobe
            if (player.Place == "home")
                player.GotoXY(75, y);
            if (player.Place == "university")
                player.GotoXY(250, y);
            if (player.Place == "lounge")
                player.GotoXY(450, y);
            if (player.Place == "gym")
                player.GotoXY(450, y + 70);
            if (player.Place == "company")
                player.GotoXY(250, y+70);
            if (player.Place=="shop")
                player.GotoXY(75, y+70);

            player.SetVisible(true);
            player.Place = "main";

            //3. scripts that start
            Game.StartScript(PlayerMove);

        } //postavlja se "vanjski svijet" nakon izlaska iz sobe

        private void SetupHome()
        {
            setBackgroundPicture("backgrounds\\transition.jpg");
            player.SetVisible(false);
            Wait(0.3);
            setBackgroundPicture("backgrounds\\room_bg.jpg");

            player.X = 220;
            player.Y = GameOptions.DownEdge- GameOptions.SpriteHeight;
            player.SetVisible(true);
            player.Place = "home";
            

            bed = new Item("sprites\\bed.png",5,200, 0,0,0,-5, 60, 480);
            bed.SetSize(150);
            Game.AddSprite(bed);

            table = new Item("sprites\\table.png", 5, 350, 2, 0,0,5,-10,60);
            table.SetTransparentColor(Color.White);
            table.SetSize(125);
            Game.AddSprite(table);

            bookcase = new Item("sprites\\bookcase.png",100, 150, 1,0,0,0,-5,30);
            bookcase.SetSize(140);
            Game.AddSprite(bookcase);

            tv = new Item("sprites\\tv.png", 200, 200, 0,1,0,-5,0,30);
            tv.SetSize(125);
            Game.AddSprite(tv);
            
            Game.StartScript(PlayerMoveRoom);
            
        } //postavlja Home

        private void SetupUNI()
        {
            setBackgroundPicture("backgrounds\\transition.jpg");
            player.SetVisible(false);
            Wait(0.3);
            setBackgroundPicture("backgrounds\\home_bg.jpg");

            player.X = 250;
            player.Y = 300;
            player.SetVisible(true);
            player.Place = "university";


            professor = new NPC("sprites\\teacher.png", 250, 0, 1, 1, 15, "Hello " + player.Name + "! How can I help you?");
            professor.SetTransparentColor(Color.Red);
            professor.SetSize(150);
            Game.AddSprite(professor);

            desk = new Item("sprites\\desk.png", 0,100,5, 0,0,10,-20,120);
            desk.SetSize(55);
            Game.AddSprite(desk);

            //dodajemo papir na stol s kojim je moguća interakcija, da ga razlikujemo od ostalih
            Sprite p = new Sprite("sprites\\paper.png", 50,95);
            Game.AddSprite(p);

            //dodajemo jos stolova, ali je interakcija moguća samo s najgornjim lijevim
            Item d1 = new Item("sprites\\desk.png", 0, 200);
            d1.SetSize(55);
            Game.AddSprite(d1);

            Item d2 = new Item("sprites\\desk.png", 0, 300);
            d2.SetSize(55);
            Game.AddSprite(d2);

            Item d3 = new Item("sprites\\desk.png", 473, 100);
            d3.SetSize(55);
            Game.AddSprite(d3);

            Item d4 = new Item("sprites\\desk.png", 473, 200);
            d4.SetSize(55);
            Game.AddSprite(d4);

            Item d5 = new Item("sprites\\desk.png", 473, 300);
            d5.SetSize(55);
            Game.AddSprite(d5);

            Game.StartScript(PlayerMoveRoom);
        } // postavlja University

        private void SetupLounge()
        {
            setBackgroundPicture("backgrounds\\transition.jpg");
            player.SetVisible(false);
            Wait(0.3);
            setBackgroundPicture("backgrounds\\room_bg.jpg");
            player.X = 220;
            player.Y = GameOptions.DownEdge - GameOptions.SpriteHeight;
            player.SetVisible(true);
            player.Place = "lounge";

            friend1 = new NPC("sprites\\friend1.png",5,300,5,0,30, "Hi " + player.Name+"! How are you?");
            friend1.SetSize(150);
            Game.AddSprite(friend1);

            friend2 = new NPC("sprites\\friend2.png", 200, 300, 10, 0, 60, "Hi " + player.Name + "! Have you been studying lately?");
            friend2.SetSize(150);
            Game.AddSprite(friend2);

            karaoke = new Item("sprites\\karaoke.jpg",100,150,0,5,0,-10,-15,60);
            karaoke.SetTransparentColor(Color.White);
            Game.AddSprite(karaoke);
            
            Game.StartScript(PlayerMoveRoom);
        } //postavlja Lounge        

        private void SetupGym()
        {
            setBackgroundPicture("backgrounds\\transition.jpg");
            player.SetVisible(false);
            Wait(0.3);
            setBackgroundPicture("backgrounds\\room_bg2.jpg");

            player.X = 220;
            player.Y = GameOptions.UpEdge + GameOptions.SpriteHeight;
            player.SetVisible(true);
            player.Place = "gym";


            dumbbell = new Item("sprites\\dumbbell.png",5,150,0,0,5,-3,-20,60);
            dumbbell.SetSize(30);
            dumbbell.SetTransparentColor(Color.White);
            Game.AddSprite(dumbbell);
            
            trainer = new NPC("sprites\\trainer.png",200,200,1,5,60, "Hello " + player.Name + "! Have you been exercising lately?");
            trainer.SetSize(130);
            Game.AddSprite(trainer);

            Game.StartScript(PlayerMoveRoom);
        } //postavlja Gym

        private void SetupCompany()
        {
            setBackgroundPicture("backgrounds\\transition.jpg");
            player.SetVisible(false);
            Wait(0.3);
            setBackgroundPicture("backgrounds\\room_bg2.jpg");

            player.X = 220;
            player.Y = GameOptions.UpEdge + GameOptions.SpriteHeight;
            player.SetVisible(true);
            player.Place = "company";


            boss = new NPC("sprites\\boss.png",200,250,5,0,5, "Get back to work!");
            boss.SetSize(140);
            Game.AddSprite(boss);

            workdesk = new Item("sprites\\desk.png",5,150);
            workdesk.SetSize(50);
            Game.AddSprite(workdesk);

            computer = new Item("sprites\\screen.png", 35, 118, 0, 0, 0, 10, -30, 180);
            computer.SetSize(150);
            Game.AddSprite(computer);

            Game.StartScript(PlayerMoveRoom);
        } //postavlja Company

        private void SetupShop()
        {
            setBackgroundPicture("backgrounds\\transition.jpg");
            player.SetVisible(false);
            Wait(0.3);
            setBackgroundPicture("backgrounds\\room_bg2.jpg");

            player.X = 220;
            player.Y = GameOptions.UpEdge + GameOptions.SpriteHeight;
            player.SetVisible(true);
            player.Place = "shop";


            bKnowledge = new Book("sprites\\book.png",25,300,30,0,0,0,0,400);
            bKnowledge.SetSize(200);
            Game.AddSprite(bKnowledge);

            bCharisma = new Book("sprites\\book.png", 25, 225,0, 30, 0, 0, 0, 400);
            bCharisma.SetSize(200);
            Game.AddSprite(bCharisma);

            bFitness = new Book("sprites\\book.png", 25, 150,0,0,30,0,0,400);
            bFitness.SetSize(200);
            Game.AddSprite(bFitness);

            bStress = new Book("sprites\\book.png", 25, 75,0,0,0,30,0,400);
            bStress.SetSize(200);
            Game.AddSprite(bStress);

            Game.StartScript(PlayerMoveRoom);
        } //postavlja Shop


        /* Scripts */

        int step = 0; //broji korake, potrebno zbog mijenjanja kostima
        
        private int PlayerMove()
        {            
            while (START)
            {
                SaveGame();
                Instructions();
                StatsShow();

                if (sensing.KeyPressed(Keys.Left) || sensing.KeyPressed("A"))
                {
                    player.Left(5, ref step);
                    step++;
                    Wait(0.01);
                }
                if (sensing.KeyPressed(Keys.Right) || sensing.KeyPressed("D"))
                {
                    player.Right(5, ref step);
                    step++;
                    Wait(0.01);                        
                }
                if (sensing.KeyPressed(Keys.Up) || sensing.KeyPressed("W"))
                {
                    player.Up(5, ref step);
                    step++;
                    Wait(0.01);

                    //kretanjem prema gore, player može ući u Home, University ili Lounge
                    if (player.CheckPlace() == "home")
                    {
                        this.SetupHome();
                        break;
                    }
                    else if (player.CheckPlace() == "university")
                    {
                        this.SetupUNI();
                        break;
                    }
                    else if (player.CheckPlace() == "lounge")
                    {
                        this.SetupLounge();
                        break;
                    }
                }
                if (sensing.KeyPressed(Keys.Down) || sensing.KeyPressed("S"))
                {
                    player.Down(5, ref step);
                    step++;
                    Wait(0.01);

                    //kretanjem prema dolje, player može ući u Shop, Company ili Gym
                    if (player.CheckPlace()=="shop")
                    {
                        this.SetupShop();
                        break;
                    }
                    else if (player.CheckPlace()=="company")
                    {
                        this.SetupCompany();
                        break;
                    }
                    else if (player.CheckPlace()=="gym")
                    {
                        this.SetupGym();
                        break;
                    }
                }               
            }
            return 0;
        } //skripta koja služi za kretanje glavnog lika u glavnom prostoru

        private int PlayerMoveRoom()
        {     
            while (START)
            {
                SaveGame();
                Instructions();
                StatsShow();

                //ograničavanje kretanja u prostorijama
                if (player.Place == "home" || player.Place == "lounge")
                {
                    if (player.X > 230)
                        player.X = 230;
                    if (player.Y < 110)
                        player.Y = 110;
                    if (player.X < GameOptions.LeftEdge)
                        player.X = GameOptions.LeftEdge;
                }
                if (player.Place == "gym" || player.Place == "company" || player.Place == "shop")
                {
                    if (player.X > 230)
                        player.X = 230;
                    if (player.Y > 320)
                        player.Y = 320;
                    if (player.X < GameOptions.LeftEdge)
                        player.X = GameOptions.LeftEdge;
                }

                //pozivamo metode za interakciju
                if (player.Place=="home")
                    HomeInteract();
                if (player.Place=="university")
                    UniversityInteract();
                if (player.Place=="lounge")
                    LoungeInteract();
                if (player.Place=="gym")
                    GymInteract();
                if (player.Place == "company")
                    CompanyInteract();
                if (player.Place == "shop")
                    ShopInteract();          

                if (sensing.KeyPressed(Keys.Left) || sensing.KeyPressed("A"))
                {
                    player.Left(5, ref step);
                    step++;
                    Wait(0.01);
                }
                if (sensing.KeyPressed(Keys.Right) || sensing.KeyPressed("D"))
                {
                    player.Right(5, ref step);
                    step++;
                    Wait(0.01);
                }
                if (sensing.KeyPressed(Keys.Up) || sensing.KeyPressed("W"))
                {
                    player.Up(5, ref step);
                    step++;
                    Wait(0.01);
                    
                    if (player.Y < GameOptions.UpEdge + GameOptions.SpriteHeight / 3) //ako izađe iz prostorije
                    {
                        this.SetupGame2();
                        break;
                    }
                }
                if (sensing.KeyPressed(Keys.Down) || sensing.KeyPressed("S"))
                {
                    player.Down(5, ref step);
                    step++;
                    Wait(0.01);

                    if (player.Y > GameOptions.DownEdge-GameOptions.SpriteHeight/3) //ako izađe iz prostorije
                    {
                        this.SetupGame2();
                        break;
                    }
                }
            }
            return 0;
        } //skripta koja služi za kretanje lika u pojedinim sobama

        //metode za interakciju s objektima u određenoj sobi
        private void HomeInteract()
        {
            if (player.TouchingSprite(bed) && sensing.KeyPressed("T"))
                {
                    Wait(0.5);
                    player.Energy += bed.EnergyChange;
                    player.Stress += bed.StressChange;
                    TimerUpdate(bed.TimeMin);
                }
                if (player.TouchingSprite(table) && sensing.KeyPressed("T"))
                {
                    Wait(0.5);
                    try
                    {
                        player.Energy += table.EnergyChange;
                        player.Knowledge += table.KnowledgeChange;
                        player.Stress += table.StressChange;
                        TimerUpdate(table.TimeMin);

                    }
                    catch (EnergyException en)
                    {
                        Wait(0.5);
                        MessageBox.Show(en.Message);
                    }
                    catch (StressException se)
                    {
                        Wait(0.5);
                        MessageBox.Show(se.Message);
                    }                
                }

                if (player.TouchingSprite(bookcase) && sensing.KeyPressed("T"))
                {
                    Wait(0.5);
                    try
                    {
                        player.Energy += bookcase.EnergyChange;
                        player.Knowledge += bookcase.KnowledgeChange;
                        TimerUpdate(bookcase.TimeMin);
                    }
                    catch (EnergyException en)
                    {
                        Wait(0.5);
                        MessageBox.Show(en.Message);
                    }
                }

                if (player.TouchingSprite(tv) && sensing.KeyPressed("T"))
                {
                    Wait(0.5);
                    player.Charisma += tv.CharismaChange;
                    player.Stress += tv.StressChange;
                    TimerUpdate(tv.TimeMin);
                }
        }
        private void UniversityInteract()
        {
                if (player.TouchingSprite(professor) && sensing.KeyPressed("T"))
                {
                    Wait(0.5);
                    MessageBox.Show(professor.Message);
                    player.Knowledge += professor.SkillValue;
                    player.Charisma += professor.CharismaValue;

                    TimerUpdate(professor.TimeMin);
                }

                if (player.TouchingSprite(desk) && sensing.KeyPressed("T"))
                {
                    Wait(0.5);

                    try
                    {
                        player.Energy += desk.EnergyChange;
                        player.Knowledge += desk.KnowledgeChange;
                        player.Stress += desk.StressChange;
                        TimerUpdate(desk.TimeMin);
                    }
                    catch (EnergyException en)
                    {
                        Wait(0.5);
                        MessageBox.Show(en.Message);
                    }
                    catch (StressException se)
                    {
                        Wait(0.5);
                        MessageBox.Show(se.Message);
                    }                
            }
        }
        private void LoungeInteract()
        {
            if (player.TouchingSprite(friend1) && sensing.KeyPressed("T"))
            {
                Wait(0.5);
                MessageBox.Show(friend1.Message);
                player.Knowledge += friend1.SkillValue;
                player.Charisma += friend1.CharismaValue;

                TimerUpdate(friend1.TimeMin);
            }
            if (player.TouchingSprite(friend2) && sensing.KeyPressed("T"))
            {
                Wait(0.5);
                MessageBox.Show(friend2.Message);
                player.Knowledge += friend2.SkillValue;
                player.Charisma += friend2.CharismaValue;

                TimerUpdate(friend2.TimeMin);
            }
            if (player.TouchingSprite(karaoke) && sensing.KeyPressed("T"))
            {
                try
                {
                    Wait(0.5);
                    player.Charisma += karaoke.CharismaChange;
                    player.Stress += karaoke.StressChange;
                    player.Energy += karaoke.EnergyChange;
                    TimerUpdate(karaoke.TimeMin);
                }
                catch (EnergyException en)
                {
                    Wait(0.5);
                    MessageBox.Show(en.Message);
                }
            }
        }
        private void GymInteract()
        {
            if (player.TouchingSprite(dumbbell) && sensing.KeyPressed("T"))
            {
                Wait(0.5);
                try
                {
                    player.Charisma += dumbbell.CharismaChange;
                    player.Fitness += dumbbell.FitnessChange;
                    player.Stress += dumbbell.StressChange;
                    player.Energy += dumbbell.EnergyChange;
                    TimerUpdate(dumbbell.TimeMin);
                }
                catch (EnergyException en)
                {
                    MessageBox.Show(en.Message);
                }
            }

            if (player.TouchingSprite(trainer) && sensing.KeyPressed("T"))
            {
                Wait(0.5);
                MessageBox.Show(trainer.Message);
                try
                {
                    player.Charisma += trainer.CharismaValue;
                    player.Fitness += trainer.SkillValue;
                    player.Energy -= 5;
                    TimerUpdate(trainer.TimeMin);
                }
                catch (EnergyException en)
                {
                    MessageBox.Show(en.Message);
                }
            }

        }
        private void CompanyInteract()
        {
            if (player.TouchingSprite(boss) && sensing.KeyPressed("T"))
            {
                Wait(0.5);
                player.Charisma += boss.CharismaValue;
                MessageBox.Show(boss.Message);
            }

            if ((player.TouchingSprite(computer) || player.TouchingSprite(workdesk)) && sensing.KeyPressed("T"))
            {
                Wait(0.5);
                try
                {
                    player.Energy += computer.EnergyChange;
                    player.Stress += computer.StressChange;
                    player.Money += 100;
                    TimerUpdate(computer.TimeMin);
                }
                catch (EnergyException en)
                {
                    MessageBox.Show(en.Message);
                }
                catch (StressException se)
                {
                    MessageBox.Show(se.Message);
                }
            }
        }
        private void ShopInteract()
        {
            if (player.TouchingSprite(bKnowledge) && sensing.KeyPressed("T"))
            {
                Wait(0.5);
                DialogResult dr = MessageBox.Show("This is knowledge book and it costs 400 money. Do you wish to proceed?", "Knowledge book", MessageBoxButtons.YesNo);
                if (dr==DialogResult.Yes)
                {
                    try
                    {
                        player.Money -= bKnowledge.MoneyValue;
                        player.Knowledge += bKnowledge.KnowledgeChange;
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("You don't have enough money!");
                    }
                }
                else if (dr==DialogResult.No) //ne radi ništa ako se pritisne "No"
                {
                }               
            }

            if (player.TouchingSprite(bCharisma) && sensing.KeyPressed("T"))
            {
                Wait(0.5);
                DialogResult dr = MessageBox.Show("This is charisma book and it costs 400 money. Do you wish to proceed?", "Charisma book", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        player.Money -= bCharisma.MoneyValue;
                        player.Charisma += bCharisma.CharismaChange;
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("You don't have enough money!");
                    }
                }
                else if (dr == DialogResult.No)
                {
                }
            }

            if (player.TouchingSprite(bFitness) && sensing.KeyPressed("T"))
            {
                Wait(0.5);
                DialogResult dr = MessageBox.Show("This is fitness book and it costs 400 money. Do you wish to proceed?", "Fitness book", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        player.Money -= bFitness.MoneyValue;
                        player.Fitness += bFitness.FitnessChange;
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("You don't have enough money!");
                    }
                }
                else if (dr == DialogResult.No)
                {
                }
            }

            if (player.TouchingSprite(bStress) && sensing.KeyPressed("T"))
            {
                Wait(0.5);
                DialogResult dr = MessageBox.Show("This is de-stress book and it costs 400 money. Do you wish to proceed?", "De-stress book", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        player.Money -= bStress.MoneyValue;
                        player.Stress -= bStress.StressChange;
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("You don't have enough money!");
                    }
                }
                else if (dr == DialogResult.No)
                {
                }
            }
        }

        //metode za rad igre
        private void StatsShow()
        {
            string s = "";
            if (sensing.KeyPressed(Keys.Space))
            {
                s = "Knowledge: " + player.Knowledge + "/ 100";
                s += "\n" + "Charisma: " + player.Charisma + "/ 100";
                s += "\n" + "Fitness: " + player.Fitness + "/ 100";
                s += "\n" + "Stress level: " + player.Stress + "/ 100";
                s += "\n" + "Energy: " + player.Energy + "/ 100";
                s += "\n" + "Money: " + player.Money;
            }
            if (s != "")
            {
                Wait(0.5);
                MessageBox.Show(s);
            }
        } //metoda za prikazivanje trenutnog stanja skills
        private void Instructions()
        {
            if (sensing.KeyPressed("I"))
            {
                string s = "For stats window, click Space.";
                s += "\nTo interact with objects, you have to touch them and click 'T'.";
                s += "\nTo save game, click 'Q'.";
                Wait(0.5);
                MessageBox.Show(s, "Instructions");
            }
        } //metoda za prikazivanje Instructions
        private void TimerUpdate(int x)
        {
            string[] t = timer.Split(':');
            if (int.Parse(t[1]) + x >= 60) //ako prijeđe 60min
            {

                int y = int.Parse(t[0]) + (int.Parse(t[1]) + x) / 60;
                int z = (int.Parse(t[1]) + x) % 60;
                if (y.ToString().Length == 1)
                    timer = "0" + y.ToString();
                else
                    timer = y.ToString();

                if (z.ToString().Length == 1)
                    timer += ":0" + z.ToString();
                else
                    timer += ":" + z.ToString();
            }
            else
            {
                int z = int.Parse(t[1]) + x;

                if (z.ToString().Length == 1)
                    timer = t[0] + ":0" + z.ToString();
                else
                    timer = t[0] + ":" + z.ToString();

            }
            t = timer.Split(':');
            if (int.Parse(t[0]) >= 24) //ako prijeđe 24 sata
            {
                day++;
                int y = int.Parse(t[0]) % 24;
                if (y.ToString().Length == 1)
                    timer = "0" + y.ToString();
                else
                    timer = y.ToString();
                timer += ":" + t[1];
            }

            ISPIS = "Day: " + day + " Time: " + timer;
            ExamsStart();
        } //metoda koja update-a timer s vremenom u minutama koje pošaljemo
        private void ExamsStart()
        {
            if (day==5 &&f1)
            {
                string s = "Your current results:";
                s += "\nKnowledge: " + player.Knowledge + "/ 100";
                s += "\nCharisma: " + player.Charisma + "/ 100";
                s += "\nFitness: " + player.Fitness + "/ 100";
                s += "\nStress level: " + player.Stress + "/ 100";

                if (player.Knowledge >= 50 && player.Charisma >= 50 && player.Fitness >= 50)
                {
                    MessageBox.Show(s+"\n\nCongratulations! You passed all of your midterm exams!\nKepp up the good work!", "Exam day");
                }
                else
                {
                    MessageBox.Show(s+ "\n\nYou failed your midterm exams :( \n Better luck next time!", "Exam day");
                }
                f1 = false;
            }

            if (day==10 &&f2)
            {
                string s = "Your current results:";
                s += "\nKnowledge: " + player.Knowledge + "/ 100";
                s += "\nCharisma: " + player.Charisma + "/ 100";
                s += "\nFitness: " + player.Fitness + "/ 100";
                s += "\nStress level: " + player.Stress + "/ 100";

                if (player.Knowledge >= 90 && player.Charisma >= 90 && player.Fitness >= 90)
                {
                    MessageBox.Show(s + "\n\nCongratulations! You passed all of your exams!", "Exam day");
                }
                else
                {
                    MessageBox.Show(s + "\n\nYou failed :( \n Better luck next time!", "Exam day");
                }
                f2 = false;
                START = false;
                ClosingScene();
            }
            
        } //metoda za početak ispita
        private void SaveGame()
        {
            if (sensing.KeyPressed("Q"))
            {
                Wait(0.5);
                string datName="savepoint.txt";
                StreamWriter writer = new StreamWriter(datName);
                string result = player.Name + "#" + player.Gender + "#" + player.Knowledge + "#" + player.Charisma + "#";
                result += player.Fitness + "#" + player.Stress +"#"+ player.Energy + "#" + player.Money + "#" + player.Place + "#" + day + "#" + timer;
                writer.Write(result);

                writer.Close();
                MessageBox.Show("GameSaved");
            }
        } //metoda za spremanje igre
        private void LoadGame()
        {
            string datName = "savepoint.txt";
            StreamReader sr = new StreamReader(datName);
            using (sr)
            {
                string line, data="";
                while((line=sr.ReadLine())!=null)
                {
                    data += line;
                }
                string [] s= data.Split('#');

                SetupGame();

                player.Name = s[0];
                if (s[1].ToLower() == "true")
                    player.Gender = true;
                else
                    player.Gender = false;

                if (player.Gender)
                {
                    string[] costumes = new string[]
                    {"sprites\\girl000.png",
                "sprites\\girl001.png",
                "sprites\\girl002.png",
                "sprites\\girl003.png",
                "sprites\\girl004.png",
                "sprites\\girl005.png",
                "sprites\\girl006.png",
                "sprites\\girl007.png",
                "sprites\\girl008.png",
                "sprites\\girl009.png",
                "sprites\\girl010.png",
                "sprites\\girl011.png"};
                    player.Costumes.RemoveAt(0);
                    player.AddCostumes(costumes);
                    player.NextCostume();
                }
                else
                {
                    string[] costumes = new string[]
                    {"sprites\\boy000.png",
                "sprites\\boy001.png",
                "sprites\\boy002.png",
                "sprites\\boy003.png",
                "sprites\\boy004.png",
                "sprites\\boy005.png",
                "sprites\\boy006.png",
                "sprites\\boy007.png",
                "sprites\\boy008.png",
                "sprites\\boy009.png",
                "sprites\\boy010.png",
                "sprites\\boy011.png"};
                    player.Costumes.RemoveAt(0);
                    player.AddCostumes(costumes);
                    player.NextCostume();
                }
                
                player.Knowledge = int.Parse(s[2]);
                player.Charisma = int.Parse(s[3]);
                player.Fitness = int.Parse(s[4]);
                player.Stress = int.Parse(s[5]);
                player.Energy = int.Parse(s[6]);
                player.Money = int.Parse(s[7]);
                player.Place = s[8];
                day = int.Parse(s[9]);
                timer = s[10];
                TimerUpdate(0);

                if (player.Place == "home")
                {
                    player.GotoXY(100, 100);
                    SendKeys.Send("W");
                }                    
                else if (player.Place == "university")
                {
                    player.GotoXY(300, 100);
                    SendKeys.Send("W");
                }                    
                else if (player.Place == "lounge")
                {
                    player.GotoXY(500, 100);
                    SendKeys.Send("W");
                }                    
                else if (player.Place == "shop")
                {
                    player.GotoXY(100, 300);
                    SendKeys.Send("S");
                }                    
                else if (player.Place == "company")
                {
                    player.GotoXY(300, 300);
                    SendKeys.Send("S");
                }                    
                else if (player.Place == "shop")
                {
                    player.GotoXY(100, 300);
                    SendKeys.Send("S");
                }
                else if (player.Place == "gym")
                {
                    player.GotoXY(500, 300);
                    SendKeys.Send("S");
                }
            }
        }//metoda za učitavanje prije spremljene igre
        private void ClosingScene()
        {
            setBackgroundPicture("backgrounds\\uni_sim2.jpg");
            allSprites.Clear();
            ISPIS = "";
        } // metoda koja se pokreće kad igra završi

        



















    }









    /* ------------ GAME CODE END ------------ */


}
