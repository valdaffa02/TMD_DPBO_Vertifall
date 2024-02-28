using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace Vertifall.Model
{
    public class Platform : GameObject
    {
        private string status;
        private int attachedScore;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public int AttachedScore
        {
            get { return attachedScore; }
            set { attachedScore = value; }
        }

        public Platform(string name, int height, int width, int leftBound, int topBound, Brush brush) : base(name, height, width, leftBound, topBound, brush)
        {
            attachedScore = 100 - (width / 6);
            status = "active";
        }

        public Platform(string name, int height, int width, int leftBound, int topBound, string uri) : base(name, height, width, leftBound, topBound, uri)
        {
            attachedScore = 100 - (width / 6);
            status = "active";

        }

        public override void Update()
        {
            // Move the platform
            Canvas.SetTop(this.objRectangle, Canvas.GetTop(this.objRectangle) - 4);

            // Update hitbox position
            this.hitBox = new Rect(Canvas.GetLeft(this.objRectangle), Canvas.GetTop(this.objRectangle), this.objRectangle.Width, this.objRectangle.Height);

            this.textureHandling();
        }

        private void textureHandling()
        {
            if (this.status == "active")
            {
                switch (this.objRectangle.Width / 60)
                {
                    case 1:
                        this.texture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/platform_singular.png"));
                        break;
                    case 2:
                        this.texture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/platform_2.png"));
                        break;
                    case 3:
                        this.texture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/platform_3.png"));
                        break;
                    case 4:
                        this.texture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/platform_4.png"));
                        break;
                    case 5:
                        this.texture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/platform_5.png"));
                        break;
                    case 6:
                        this.texture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/platform_6.png"));
                        break;
                    case 7:
                        this.texture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/platform_7.png"));
                        break;
                    case 8:
                        this.texture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/platform_8.png"));
                        break;
                    case 9:
                        this.texture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/platform_9.png"));
                        break;
                }
            }
            else if (this.status == "claimed")
            {
                switch (this.objRectangle.Width / 60)
                {
                    case 1:
                        this.texture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/platform_singular.png"));
                        break;
                    case 2:
                        this.texture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/platform_2_empty.png"));
                        break;
                    case 3:
                        this.texture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/platform_3_empty.png"));
                        break;
                    case 4:
                        this.texture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/platform_4_empty.png"));
                        break;
                    case 5:
                        this.texture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/platform_5_empty.png"));
                        break;
                    case 6:
                        this.texture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/platform_6_empty.png"));
                        break;
                    case 7:
                        this.texture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/platform_7_empty.png"));
                        break;
                    case 8:
                        this.texture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/platform_8_empty.png"));
                        break;
                    case 9:
                        this.texture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/platform_9_empty.png"));
                        break;
                }
            }
        }

        // Add additional properties and methods specific to the Platform class
    }
}
