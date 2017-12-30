using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Chess.Forms;
using Chess.Gravity;
using Chess.SpaceObjects;
using SharpAvi;
using SharpAvi.Codecs;
using System.Threading;

namespace Chess.CustomControls
{
    public class Display : WinFormsGraphicsDevice.GraphicsDeviceControl
    {
        int backgroundIndex = 0;
        Boolean showNames = true;
        Boolean showScale = true;
        Boolean showAsDots = false;
        Boolean showTrailsAll = true;
        Boolean showVectorsAll = false;
        Texture2D textureBackground;
        Texture2D textureMercury;
        Texture2D textureSun;
        Texture2D textureVenus;
        Texture2D textureEarth;
        Texture2D textureMoon;
        Texture2D textureMars;
        Texture2D textureJupiter;
        Texture2D textureSaturn;
        Texture2D textureNeptune;
        Texture2D textureUranus;
        Texture2D texturePluto;
        Texture2D texturePlanet;
        Texture2D textureRedBall;
        Texture2D textureMetalBall;
        Texture2D textureGoldenBall;
        Texture2D textureArrow;
        Texture2D textureVector;
        Texture2D textureTrace;
        Texture2D textureSmallDot;
        Texture2D textureLargeDot;
        Texture2D textureAsteroid;
        SpriteFont fontNormal;
        SpriteFont fontSmall;
        SpriteFont fontLindsey;
        Texture2D textureLine;     //base for the line texture
        Rectangle rectVector;
        bool simulationRunning = false;
        bool simulationStepping = false;
        bool recordingVideo = false;
        DateTime simulationTime;
        long simulationTimeLarge;
        bool reverse = false;
        BlendState blendState = BlendState.AlphaBlend;
        ScreenRecorder screenRecorder;
        private string videoCaptureCompression;
        int videoCaptureFPS = 60;
        SmallDot smallDot = new SmallDot();
        Random rnd = new Random();

        ContentManager contentManager;
        long timeUnitsPerStep = 1;

        int scrollWheelValue, lastScrollWheelValue;

        // Input state.
        KeyboardState currentKeyboardState;
        GamePadState currentGamePadState;

        KeyboardState lastKeyboardState;
        GamePadState lastGamePadState;
        System.Drawing.Point lastMousePosition;
        FormMain parentForm;

        SpriteBatch spriteBatch;
        GravitySystem gravitySystem;
        PresetObjects presetObjects;

        internal GravitySystem GravitySystem
        {
            get
            {
                return gravitySystem;
            }

            set
            {
                gravitySystem = value;
            }
        }

        public bool ShowScale
        {
            get
            {
                return showScale;
            }

            set
            {
                showScale = value;
            }
        }

        public long TimeUnitsPerStep
        {
            get
            {
                return timeUnitsPerStep;
            }

            set
            {
                timeUnitsPerStep = value;
            }
        }

        public bool SimulationRunning
        {
            get
            {
                return simulationRunning;
            }

            set
            {
                simulationRunning = value;
            }
        }

        public bool SimulationStepping
        {
            get
            {
                return simulationStepping;
            }

            set
            {
                simulationStepping = value;
            }
        }

        public bool ShowAsDots
        {
            get
            {
                return showAsDots;
            }

            set
            {
               showAsDots = value;
            }
        }

        public DateTime SimulationTime
        {
            get
            {
                return SimulationTime1;
            }

            set
            {
                SimulationTime1 = value;
            }
        }

        public long SimulationTimeLarge
        {
            get
            {
                return simulationTimeLarge;
            }

            set
            {
                simulationTimeLarge = value;
            }
        }

        public int BackgroundIndex
        {
            get
            {
                return backgroundIndex;
            }

            set
            {
                backgroundIndex = value;
            }
        }

        public bool ShowNames
        {
            get
            {
                return showNames;
            }

            set
            {
                showNames = value;
            }
        }

        public bool Reverse
        {
            get
            {
                return reverse;
            }

            set
            {
                reverse = value;
            }
        }

        public BlendState BlendState
        {
            get
            {
                return blendState;
            }

            set
            {
                blendState = value;
            }
        }

        public bool ShowTrailsAll
        {
            get
            {
                return showTrailsAll;
            }

            set
            {
                showTrailsAll = value;
            }
        }

        public bool ShowVectorsAll
        {
            get
            {
                return showVectorsAll;
            }

            set
            {
                showVectorsAll = value;
            }
        }

        public bool RecordingVideo
        {
            get
            {
                return recordingVideo;
            }

            set
            {
                recordingVideo = value;
            }
        }

        public int VideoCaptureFPS
        {
            get
            {
                return videoCaptureFPS;
            }

            set
            {
                videoCaptureFPS = value;
            }
        }

        public string VideoCaptureCompression
        {
            get
            {
                return videoCaptureCompression;
            }

            set
            {
                videoCaptureCompression = value;
            }
        }

        public DateTime SimulationTime1 { get => simulationTime; set => simulationTime = value; }
        public Texture2D TextureBackground { get => textureBackground; set => textureBackground = value; }
        public Texture2D TextureMercury { get => textureMercury; set => textureMercury = value; }
        public Texture2D TextureSun { get => textureSun; set => textureSun = value; }
        public Texture2D TextureVenus { get => textureVenus; set => textureVenus = value; }
        public Texture2D TextureEarth { get => textureEarth; set => textureEarth = value; }
        public Texture2D TextureMoon { get => textureMoon; set => textureMoon = value; }
        public Texture2D TextureMars { get => textureMars; set => textureMars = value; }
        public Texture2D TextureJupiter { get => textureJupiter; set => textureJupiter = value; }
        public Texture2D TextureSaturn { get => textureSaturn; set => textureSaturn = value; }
        public Texture2D TextureNeptune { get => textureNeptune; set => textureNeptune = value; }
        public Texture2D TextureUranus { get => textureUranus; set => textureUranus = value; }
        public Texture2D TexturePluto { get => texturePluto; set => texturePluto = value; }
        public Texture2D TexturePlanet { get => texturePlanet; set => texturePlanet = value; }
        public Texture2D TextureRedBall { get => textureRedBall; set => textureRedBall = value; }
        public Texture2D TextureMetalBall { get => textureMetalBall; set => textureMetalBall = value; }
        public Texture2D TextureGoldenBall { get => textureGoldenBall; set => textureGoldenBall = value; }
        public Texture2D TextureArrow { get => textureArrow; set => textureArrow = value; }
        public Texture2D TextureVector { get => textureVector; set => textureVector = value; }
        public Texture2D TextureTrace { get => textureTrace; set => textureTrace = value; }
        public Texture2D TextureSmallDot { get => textureSmallDot; set => textureSmallDot = value; }
        public Texture2D TextureLargeDot { get => textureLargeDot; set => textureLargeDot = value; }
        public Texture2D TextureAsteroid { get => textureAsteroid; set => textureAsteroid = value; }
        internal PresetObjects PresetObjects { get => presetObjects; set => presetObjects = value; }
        internal SmallDot SmallDot { get => smallDot; set => smallDot = value; }
        public FormMain ParentForm { get => parentForm; set => parentForm = value; }

        public void initSmallDot(int dotSize, Color myColor)
        {
            myColor.A = (byte)SmallDot.Alpha;
            if (dotSize < 3 || SmallDot.Type == 0)
            {
                TextureSmallDot = new Texture2D(GraphicsDevice, dotSize, dotSize);
                if(SmallDot.RandomColor)
                {
                    int color = rnd.Next(0, 6);
                    myColor.R = (color < 2 || color == 4) ? (byte)0 : (byte)255;
                    myColor.G = (color > 0 && color < 4) ? (byte)0 : (byte)255;
                    myColor.B = (color > 2) ? (byte)0 : (byte)255;
                }
                Color[] az = Enumerable.Range(0, dotSize * dotSize).Select(i => myColor).ToArray();
                TextureSmallDot.SetData(az);
            }
            else if(SmallDot.Type == 1)
            {
                TextureSmallDot = contentManager.Load<Texture2D>("smalldot"+ dotSize);
                TextureSmallDot = createTextureFromResource(TextureSmallDot, myColor);
            }
            else
            {
                TextureSmallDot = contentManager.Load<Texture2D>("cross" + dotSize);
                TextureSmallDot = createTextureFromResource(TextureSmallDot, myColor);
            }
            TextureSmallDot.Name = "<Custom Shape>";
        }

        private void setColor(Texture2D texture, Color myColor)
        {
            int width = texture.Width;
            int height = texture.Height;
            Color[] data = new Color[width * height];
            texture.GetData<Color>(data, 0, data.Length);
            for (int k=0; k<width*height; k++)
            {
                if (data[k].R > 10 || data[k].G > 10 || data[k].B > 10)     // pixel is part of the shape
                {
                    data[k].R = myColor.R;
                    data[k].G = myColor.G;
                    data[k].B = myColor.B;
                }
                data[k].A = myColor.A;
            }
            texture.SetData(data);
        }

        private Texture2D createTextureFromResource(Texture2D texture, Color myColor)
        {
            int width = texture.Width;
            int height = texture.Height;
            Color[] data = new Color[width * height];
            texture.GetData<Color>(data, 0, data.Length);
            Color[] newData = new Color[width * height];
            int color = rnd.Next(0, 6);
            for (int k = 0; k < width * height; k++)
            {
                if (data[k].R > 10 || data[k].G > 10 || data[k].B > 10)     // pixel is part of the shape
                {
                    if (SmallDot.RandomColor)
                    {
                        newData[k] = new Color();
                        newData[k].A = (byte)SmallDot.Alpha;
                        newData[k].R = (color < 2 || color == 4) ? (byte)0 : (byte)data[k].R;
                        newData[k].G = (color > 0 && color < 4) ? (byte)0 : (byte)data[k].G;
                        newData[k].B = (color > 2) ? (byte)0 : (byte)data[k].B;
                    }
                    else
                    {
                        newData[k] = myColor;
                    }
                }
            }
            Texture2D newTexture = new Texture2D(GraphicsDevice, width, height);
            newTexture.SetData(newData);

            return newTexture;
        }

        public void updateSmallDots(int startPosition=0)
        {
            for (int k=startPosition; k < gravitySystem.GravityObjects.Count; k++)
            {
                GravityObject o = gravitySystem.GravityObjects[k];
                if (o.Texture.Name.Equals("<Custom Shape>"))
                {
                    int dotSize = SmallDot.PixelSize;
                    if (SmallDot.RandomSize)
                    {
                        dotSize = rnd.Next(3, 11);
                    }
                    initSmallDot(dotSize, o.Color);

                    o.Texture = TextureSmallDot;
                    o.ScaledTextureHeight = TextureSmallDot.Height;
                    o.ScaledTextureWidth = TextureSmallDot.Width;
                }
            }
        }

        private void MouseWheelHandler(object sender, MouseEventArgs e)
        {
            scrollWheelValue += e.Delta;
        }

        public void setReverse(bool reverse_)
        {
            if (reverse != reverse_)
            {
                foreach (GravityObject o in GravitySystem.GravityObjects)
                {
                    o.XSpeed = -o.XSpeed;
                    o.YSpeed = -o.YSpeed;
                }
            }
            reverse = reverse_;
        }

        // Returns a unique (numbered) name of a given text
        public string FindFreeName(string name)
        {
            string result = name;
            int count = 1;
            while (gravitySystem.GravityObjects.Exists(item => item.Name == result))
            {
                count++;
                result = name + count.ToString();
            }

            return result;
        }

        protected override void Initialize()
        {
            gravitySystem = new GravitySystem(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            PresetObjects = new PresetObjects(this);

            SimulationTime1 = DateTime.Now;
            simulationTimeLarge = SimulationTime1.Year;

            ParentForm = (this.Parent as FormMain);
//                        contentManager = new ContentManager(Services);
//                        contentManager.RootDirectory = "Content";
            contentManager = new ResourceContentManager(Services, Resources.ResourceManager);
            // LET OP! Content dir wordt niet meer gebruikt. Als je resources wilt toevoegen moet je dit in de resourcemanager doen.
            // Je moet daar dan de .XNB files toevoegen
            // Deze kun je genereren door de uitgecommentarieerde contentmanager terug te zetten, of de XNAContentCompiler te gebruiken.

            // Load the background content. 
            TextureMercury = contentManager.Load<Texture2D>("mercury");
            TextureMercury.Name = "Mercury";
            TextureSun = contentManager.Load<Texture2D>("sun");
            TextureSun.Name = "Sun";
            TextureVenus = contentManager.Load<Texture2D>("venus");
            TextureVenus.Name = "Venus";
            TextureEarth = contentManager.Load<Texture2D>("earth");
            TextureEarth.Name = "Earth";
            TextureMoon = contentManager.Load<Texture2D>("moon");
            TextureMoon.Name = "Moon";
            TextureMars = contentManager.Load<Texture2D>("mars");
            TextureMars.Name = "Mars";
            TextureJupiter = contentManager.Load<Texture2D>("jupiter");
            TextureJupiter.Name = "Jupiter";
            TextureSaturn = contentManager.Load<Texture2D>("saturn");
            TextureSaturn.Name = "Saturn";
            TextureNeptune = contentManager.Load<Texture2D>("neptune");
            TextureNeptune.Name = "Neptune";
            TextureUranus = contentManager.Load<Texture2D>("uranus");
            TextureUranus.Name = "Uranus";
            TexturePluto = contentManager.Load<Texture2D>("pluto");
            TexturePluto.Name = "Pluto";
            TextureRedBall = contentManager.Load<Texture2D>("ball");
            TextureRedBall.Name = "Red Ball";
            TextureMetalBall = contentManager.Load<Texture2D>("metalball");
            TextureMetalBall.Name = "Metal Ball";
            TextureGoldenBall = contentManager.Load<Texture2D>("goldenball");
            TextureGoldenBall.Name = "Golden Ball";
            TexturePlanet = contentManager.Load<Texture2D>("planet");
            TexturePlanet.Name = "Planet";
            TextureArrow = contentManager.Load<Texture2D>("arrow");
            TextureArrow.Name = "Arrow";
            TextureVector = contentManager.Load<Texture2D>("vector");
            TextureVector.Name = "Vector";
            TextureLargeDot = contentManager.Load<Texture2D>("largedot");
            TextureLargeDot.Name = "Point";
            TextureAsteroid = contentManager.Load<Texture2D>("asteroid");
            TextureAsteroid.Name = "Asteroid";
            rectVector = new Rectangle(0, 0, TextureVector.Width, TextureVector.Height);
            fontLindsey = contentManager.Load<SpriteFont>("font_lindsey");
            fontNormal = contentManager.Load<SpriteFont>("font_segoeuimono");
            fontSmall = contentManager.Load<SpriteFont>("font_small");
            TextureSmallDot = new Texture2D(GraphicsDevice, SmallDot.PixelSize, SmallDot.PixelSize);

            TextureTrace = new Texture2D(GraphicsDevice, 2, 2);
            Color[] az = Enumerable.Range(0, 4).Select(i => new Color(255, 255, 255, 100)).ToArray();
            TextureTrace.SetData(az);

            // create 1x1 texture for line drawing
            textureLine = new Texture2D(GraphicsDevice, 1, 1);
            textureLine.SetData<Color>(
                new Color[] { Color.White });// fill the texture with white


            this.MouseWheel += new MouseEventHandler(MouseWheelHandler);
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public void setBackground(int backgroundIndex_)
        {
            backgroundIndex = backgroundIndex_;
            switch (backgroundIndex)
            {
                case 1:
                    TextureBackground = contentManager.Load<Texture2D>("space");
                    break;
                case 2:
                    TextureBackground = contentManager.Load<Texture2D>("space2");
                    break;
                case 3:
                    TextureBackground = contentManager.Load<Texture2D>("space3");
                    break;
                case 4:
                    TextureBackground = contentManager.Load<Texture2D>("space4");
                    break;
                case 5:
                    TextureBackground = contentManager.Load<Texture2D>("space5");
                    break;
            }
        }

        public void SetAllVectors(bool isTrue)
        {
            showVectorsAll = isTrue;
            foreach (GravityObject o in gravitySystem.GravityObjects)
            {
                o.Vector = isTrue;
            }
        }

        public void SetAllTraces(bool isTrue)
        {
            showTrailsAll = isTrue;
            foreach (GravityObject o in gravitySystem.GravityObjects)
            {
                o.Trace = isTrue;
                if (isTrue==false)
                {
                    // remove all traces
                    o.GetTraces().Clear();
                }
            }
        }

        public Texture2D getTextureByName(string text)
        {
            Texture2D texture = null;

            switch (text)
            {
                case "Planet":
                    texture = TexturePlanet;
                    break;
                case "Earth":
                    texture = TextureEarth;
                    break;
                case "Sun":
                    texture = TextureSun;
                    break;
                case "Moon":
                    texture = TextureMoon;
                    break;
                case "Mercury":
                    texture = TextureMercury;
                    break;
                case "Venus":
                    texture = TextureVenus;
                    break;
                case "Mars":
                    texture = TextureMars;
                    break;
                case "Jupiter":
                    texture = TextureJupiter;
                    break;
                case "Saturn":
                    texture = TextureSaturn;
                    break;
                case "Uranus":
                    texture = TextureUranus;
                    break;
                case "Neptune":
                    texture = TextureNeptune;
                    break;
                case "Pluto":
                    texture = TexturePluto;
                    break;
                case "Red Ball":
                    texture = TextureRedBall;
                    break;
                case "Metal Ball":
                    texture = TextureMetalBall;
                    break;
                case "Golden Ball":
                    texture = TextureGoldenBall;
                    break;
                case "Point":
                case "Large Dot":
                    texture = TextureLargeDot;
                    break;
                case "<Custom Shape>":
                    texture = TextureSmallDot;
                    break;
                case "Asteroid":
                    texture = TextureAsteroid;
                    break;
            }

            return texture;
        }

        public void AddGravityObject(int X, int Y, bool circleHost=false, bool circleHostCCW = false)
        {
            Texture2D texture = getTextureByName(ParentForm.comboBoxShape.Text);

            double xSpeed = Convert.ToDouble(ParentForm.textBoxXSpeed.Text);
            double ySpeed = Convert.ToDouble(ParentForm.textBoxYSpeed.Text);

            if ((circleHost || circleHostCCW) && gravitySystem.GravityObjects.Count>0)
            {
                GravityObject hostObject = gravitySystem.findBestHost(X, Y);
                Vector speed = gravitySystem.calcSpeedFromHost(hostObject, X, Y, Convert.ToDouble(ParentForm.textBoxMass.Text));
                xSpeed = speed.X;
                ySpeed = speed.Y;
                if(circleHostCCW)
                {
                    xSpeed = -xSpeed;
                    ySpeed = -ySpeed;
                }
            }


            gravitySystem.AddObject(ParentForm.textBoxName.Text, 
                Convert.ToDouble(ParentForm.textBoxMass.Text),
                X, Y, xSpeed, ySpeed,
                texture, ParentForm.checkBoxTrace.Checked, ParentForm.checkBoxVector.Checked);

            // perform calculation to show right vectors
            gravitySystem.CalculateStep(1/*timeUnitsPerStep*/, false);
        }
        

        void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end, int width)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);


            sb.Draw(textureLine,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    width), //width of line, change this to make thicker line
                null,
                Color.White, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);

        }

        override protected void Draw()
        {
            GraphicsDevice.Clear(Color.Black);

            try {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

                // Draw background
                Rectangle rect;
                if (BackgroundIndex>0)
                {
                    rect = new Rectangle(0, 0, Width, Height);
                    spriteBatch.Draw(TextureBackground, rect, Color.White);
                }

                // Adjust to center of object if object is centered
                GravitySystem.CenterOnObject();

                // Traces
                foreach (GravityObject o in gravitySystem.GravityObjects)
                {
                    if (o.Trace)
                    {
                        for (int i = 0; i < o.GetTraces().Count; i++)
                        {
                            Trace trace = o.GetTraces()[i];
                            if (trace.isAlive())
                            {
                                try
                                {
                                    int screen_x = gravitySystem.RealtoScreenCoordinateX(trace.X);
                                    int screen_y = gravitySystem.RealtoScreenCoordinateY(trace.Y);
                                    if (screen_x > -GraphicsDevice.Viewport.Width && screen_x < 2 * GraphicsDevice.Viewport.Width
                                        && screen_y > -GraphicsDevice.Viewport.Height && screen_y < 2 * GraphicsDevice.Viewport.Height)
                                    {
                                        spriteBatch.Draw(TextureTrace, new Rectangle(screen_x, screen_y, TextureTrace.Width, TextureTrace.Height), trace.Color);
                                    }
                                }
                                catch (OverflowException)
                                {
                                    o.GetTraces().RemoveAt(i);
                                }

                            }
                            else
                            {
                                o.GetTraces().RemoveAt(i);
                            }
                        }
                    }
                }

                foreach (GravityObject o in gravitySystem.GravityObjects)
                {
                    double screen_x = gravitySystem.RealtoScreenCoordinateX(o.X);
                    double screen_y = gravitySystem.RealtoScreenCoordinateY(o.Y);

                    // Vector
                    if (o.Vector)
                    {
                        if (screen_x > -GraphicsDevice.Viewport.Width && screen_x < 2 * GraphicsDevice.Viewport.Width
                            && screen_y > -GraphicsDevice.Viewport.Height && screen_y < 2 * GraphicsDevice.Viewport.Height)
                        {
                            spriteBatch.Draw(TextureVector, new Rectangle(Convert.ToInt32(screen_x), Convert.ToInt32(screen_y), TextureVector.Width, TextureVector.Height), null, Color.White, (float)o.AccelerationAngle, new Vector2(0, TextureVector.Height / 2), SpriteEffects.None, 0f);
//                            spriteBatch.Draw(textureVector, new Rectangle(Convert.ToInt32(screen_x), Convert.ToInt32(screen_y), textureVector.Width, textureVector.Height), null, Color.Red, (float)o.SpeedAngle, new Vector2(0, textureVector.Height / 2), SpriteEffects.None, 0f);
//                            spriteBatch.DrawString(fontSmall, o.Acceleration.ToString(), new Vector2((float)screen_x, (float)screen_y), Color.LightGray);
                        }
                    }

                    spriteBatch.End();
                    spriteBatch.Begin(SpriteSortMode.Deferred, blendState);

                    // Object
                    try {
                        if (screen_x > -GraphicsDevice.Viewport.Width && screen_x < 2 * GraphicsDevice.Viewport.Width
                            && screen_y > -GraphicsDevice.Viewport.Height && screen_y < 2 * GraphicsDevice.Viewport.Height)
                        {

                            if (!showAsDots)
                            {
                                rect = new Rectangle(Convert.ToInt32(screen_x - o.ScaledTextureWidth / 2), Convert.ToInt32(screen_y - o.ScaledTextureHeight / 2), o.ScaledTextureWidth, o.ScaledTextureHeight);
                                double percentage = 0;
                                if (SmallDot.ColorCoding > 0 && SmallDot.ColorCoding < 4 && gravitySystem.SpeedRange > 0)
                                {
                                    percentage = (o.Speed - gravitySystem.MinSpeed) / gravitySystem.SpeedRange;
                                }
                                else if (SmallDot.ColorCoding > 3 && gravitySystem.AccelerationRange > 0)
                                {
                                    percentage = (o.Acceleration - gravitySystem.MinAcceleration) / gravitySystem.AccelerationRange;
                                }
                                if (percentage > 1)  // range is not 100% accurate for speed reasons
                                {
                                    percentage = 1;
                                }
                                
                                if (SmallDot.ColorCoding==1 || SmallDot.ColorCoding == 4)        // range is not 100% accurate for speed reasons
                                {
                                    int color = Convert.ToInt32(percentage * 255);
                                    int color2 = 127 + Convert.ToInt32(percentage * 128);
                                    int color3 = 255 - Convert.ToInt32(percentage * 255);
                                    spriteBatch.Draw(o.Texture, rect, new Color(color, color2, color3, 255));
                                }
                                else if (SmallDot.ColorCoding == 2 || SmallDot.ColorCoding == 5)
                                {
                                    int color = Convert.ToInt32(percentage * 255);
                                    int color2 = 255 - Convert.ToInt32(percentage * 255);
                                    int color3 = Convert.ToInt32(percentage * 512) % 255;
                                    spriteBatch.Draw(o.Texture, rect, new Color(color, color2, color3, 255));
                                }
                                else if (SmallDot.ColorCoding == 3 || SmallDot.ColorCoding == 6)
                                {
                                    int color = 100 + Convert.ToInt32(percentage * 155);
                                    int color2 = Convert.ToInt32(percentage * 255);
                                    int color3 = 2 * Convert.ToInt32(percentage * 255);
                                    spriteBatch.Draw(o.Texture, rect, new Color(color, color2, color3, 255));
                                }
                                else
                                {
                                    spriteBatch.Draw(o.Texture, rect, Color.White);
                                }

                            }
                            else
                            {
                                rect = new Rectangle(Convert.ToInt32(screen_x - TextureLargeDot.Width / 2), Convert.ToInt32(screen_y - TextureLargeDot.Height / 2), TextureLargeDot.Width, TextureLargeDot.Height);
                                spriteBatch.Draw(TextureLargeDot, rect, Color.White);
                            }
                        }
                    } catch (OverflowException)
                    {
                        gravitySystem.GravityObjects.Remove(o);
                    }

                    spriteBatch.End();
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

                    // Object name
                    if (showNames)
                    {
                        spriteBatch.DrawString(fontSmall, o.Name, new Vector2((float)(screen_x + o.ScaledTextureWidth / 2), (float)(screen_y + o.ScaledTextureHeight / 2)), Color.LightGray);
                    }

                }

                // Scale display
                if (showScale)
                {
                    // at scale 1, one pixel = 1000 km
                    // we want maximum 1000 pixels and minimum 100 pixels
                    Double beamLength = (1000.0 / gravitySystem.Scale);
                    long factor = 1;
                    while (beamLength < 100)
                    {
                        factor *= 10;
                        beamLength = (1000.0 * factor / (gravitySystem.Scale));
                    }

                    DrawLine(spriteBatch, new Vector2(900, GraphicsDevice.Viewport.Height - 80), new Vector2(900 + 900 * factor / gravitySystem.Scale, GraphicsDevice.Viewport.Height - 80), 3);
                    DrawLine(spriteBatch, new Vector2(900, GraphicsDevice.Viewport.Height - 80), new Vector2(900, GraphicsDevice.Viewport.Height - 90), 2);
                    DrawLine(spriteBatch, new Vector2(898 + 900 * factor / gravitySystem.Scale, GraphicsDevice.Viewport.Height - 80), new Vector2(898 + 900 * factor / gravitySystem.Scale, GraphicsDevice.Viewport.Height - 90), 2);
                    spriteBatch.DrawString(fontLindsey, "0", new Vector2(900, GraphicsDevice.Viewport.Height - 80), Color.White);
                    string maxScaleText = factor.ToString() + " mln. km";
                    if (factor >= 1000)
                    {
                        maxScaleText = (factor / 1000).ToString() + " bln. km";
                    }
                    if (factor >= 1000000)
                    {
                        maxScaleText = (factor / 1000000).ToString() + " tln. km";
                    }
                    if (factor >= 1000000000)
                    {
                        maxScaleText = (factor / 1000000000).ToString() + " qdn. km";
                    }
                    spriteBatch.DrawString(fontLindsey, maxScaleText, new Vector2(GraphicsDevice.Viewport.Width - 1040 + 900 * factor / gravitySystem.Scale, GraphicsDevice.Viewport.Height - 80), Color.White);
                }

                // Simulation time
                if (timeUnitsPerStep <= 31558150)
                {
                    spriteBatch.DrawString(fontNormal, SimulationTime1.ToString("MM/dd/yyyy HH:mm"), new Vector2(20, GraphicsDevice.Viewport.Height - 140), Color.White);
                }
                else
                {
                    spriteBatch.DrawString(fontNormal, "Year: " + string.Format("{0:n0}",simulationTimeLarge), new Vector2(20, GraphicsDevice.Viewport.Height - 140), Color.White);
                }

                // Calculations per step
                if (GravitySystem.CalculationsPerStepSetting == -2)
                {
                    if (gravitySystem.IsCalculating)
                    {
                        string text = "calculating frame: " + gravitySystem.FrameNumberCalc + "/" + gravitySystem.NumPrecalculatedFrames();
                        if (gravitySystem.CalculationsPerStepPrecalculated > 1)
                        {
                            text += " (" + gravitySystem.CalculationsPerStepPrecalculated + " steps/frame)";
                        }
                        spriteBatch.DrawString(fontSmall, text, new Vector2(20, GraphicsDevice.Viewport.Height - 200), Color.LightGray);
                    }
                }
                else
                {
                    spriteBatch.DrawString(fontSmall, "calculations/step: " + gravitySystem.CalculationsPerStepActual.ToString(), new Vector2(20, GraphicsDevice.Viewport.Height - 200), Color.LightGray);
                }
                if (gravitySystem.CalculationsPerStepSetting == -2)
                {
                    spriteBatch.DrawString(fontSmall, "playing frame: " + gravitySystem.FrameNumberPlay, new Vector2(20, GraphicsDevice.Viewport.Height - 170), Color.LightGray);
                }

                if(gravitySystem.Message.Time >0)
                {
                    gravitySystem.Message.Time--;
                    spriteBatch.DrawString(fontSmall, gravitySystem.Message.Text, new Vector2(20, GraphicsDevice.Viewport.Height - 230), Color.LightSalmon);
                }


                /*                  
                                if (!gravitySystem.IsCalculating && gravitySystem.GravityObjects.Count>0)
                                {
                                    gravitySystem.InitCalculation();
                                    gravitySystem.QuadTree.DetermineBoundingBox(gravitySystem.GravityObjects);
                                    gravitySystem.QuadTree.Create(gravitySystem.Calculation, gravitySystem.GravityObjects, gravitySystem.FrameNumberPlay);

                                    if (gravitySystem.ObjectIndex != -1 && parentForm.gradientPanelObjectProperties.Visible)
                                    {
                                        gravitySystem.QuadTree.DrawUsedInternalNodes(spriteBatch, textureTrace, gravitySystem.CurrentObject(), gravitySystem.Scale, gravitySystem.OffsetX, gravitySystem.OffsetY, fontSmall);
                                    }
                                    else
                                    {
                                        gravitySystem.QuadTree.DrawQuadrants(spriteBatch, textureTrace, gravitySystem.Scale, gravitySystem.OffsetX, gravitySystem.OffsetY, fontSmall);
                                    }
                                }
                */

                // Arrow 
                if (gravitySystem.ObjectIndex != -1 && ParentForm.gradientPanelObjectProperties.Visible)
                {
                    try
                    {
                        int real_x = gravitySystem.RealtoScreenCoordinateX(gravitySystem.CurrentObject().X);
                        int real_y = gravitySystem.RealtoScreenCoordinateY(gravitySystem.CurrentObject().Y);
                        if (real_x > -GraphicsDevice.Viewport.Width && real_x < 2 * GraphicsDevice.Viewport.Width
                            && real_y > -GraphicsDevice.Viewport.Height && real_y < 2 * GraphicsDevice.Viewport.Height)
                        {
                            rect = new Rectangle(real_x - gravitySystem.CurrentObject().ScaledTextureWidth / 2 - 64, real_y - 14, TextureArrow.Width, TextureArrow.Height);
                            spriteBatch.Draw(TextureArrow, rect, Color.White);
                        }
                    }
                    catch (OverflowException)
                    {
                    }
                }

                spriteBatch.End();

            } catch (System.NullReferenceException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (InvalidOperationException e)
            {
                if (e.Message.Equals("Begin cannot be called again until End had been succesfully called."))
                {
                    spriteBatch.End();
                }
            }
        }

        public void Rewind()
        {
            if (timeUnitsPerStep < 31558151)
            {
                SimulationTime1 = SimulationTime1.AddSeconds(-timeUnitsPerStep * GravitySystem.FrameNumberPlay);
            }
            else
            {
                simulationTimeLarge -= (timeUnitsPerStep * GravitySystem.FrameNumberPlay / 31558150);
            }
            GravitySystem.FrameNumberPlay = 0;
            foreach (GravityObject o in gravitySystem.GravityObjects) 
            {
                // remove all traces
                o.GetTraces().Clear();
            }
        }

        public void UpdateFrame()
        {
            if (simulationRunning || SimulationStepping)
            {
                SimulationStepping = false;
                if (gravitySystem.CalculationsPerStepSetting != -2)
                {
                    gravitySystem.CalculateStep(timeUnitsPerStep);
                } else
                {
                    if (gravitySystem.PlayOneFrame() || (gravitySystem.CalculationsPerStepPrecalculated==1 && gravitySystem.FrameNumberCalc<=gravitySystem.FrameNumberPlay))
                    {
                        // back to beginning of replay
                        Rewind();
                    }
                }

                if (timeUnitsPerStep<31558151) {
                    try
                    {
                        if (reverse)
                        {
                            SimulationTime1 = SimulationTime1.AddSeconds(-timeUnitsPerStep);
                        }
                        else
                        {
                            SimulationTime1 = SimulationTime1.AddSeconds(timeUnitsPerStep);
                        }
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        SimulationTime1 = DateTime.Now;
                    }
                }
                else
                {
                    if (reverse)
                    {
                        simulationTimeLarge -= (timeUnitsPerStep / 31558150);
                    }
                    else
                    {
                        simulationTimeLarge += (timeUnitsPerStep / 31558150);
                    }
                }

            }
            if (recordingVideo)
            {
                screenRecorder.RecordOneFrame();
                Thread.Sleep(25);        // Wait a bit to make sure video has been streamed
            }

            HandleInput();
            //Invalidate();
        }

        public void UpdateScreen()
        {
            Invalidate();
        }
        
        /// <summary>
        /// Handles input for quitting the game and cycling
        /// through the different particle effects.
        /// </summary>
        void HandleInput()
        {
            lastKeyboardState = currentKeyboardState;
            lastGamePadState = currentGamePadState;
            lastMousePosition = MousePosition;
            lastScrollWheelValue = scrollWheelValue;

            currentKeyboardState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
        }

        public void StartRecording(string fileName)
        {
            var usedCodec = KnownFourCCs.Codecs.Uncompressed;
            foreach (var codec in Mpeg4VideoEncoderVcm.GetAvailableCodecs())
            {
                if(codec.Name.Equals(ParentForm.DisplayXNA.VideoCaptureCompression))
                {
                    usedCodec = codec.Codec;
                }
            }

            screenRecorder = new ScreenRecorder(ParentForm, fileName, usedCodec, VideoCaptureFPS);
            recordingVideo = true;
        }

        public void StopRecording()
        {
            recordingVideo = false;
            screenRecorder.Dispose();
        }
    }
}
