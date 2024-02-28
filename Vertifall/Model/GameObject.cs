using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace Vertifall.Model
{
    public abstract class GameObject
    {
        protected Rectangle objRectangle;
        protected Rect hitBox;
        protected ImageBrush texture;

        public GameObject() { }

        public GameObject(string name, int height, int width, int leftBound, int topBound, Brush brush)
        {
            objRectangle = new Rectangle();
            objRectangle.Name = name;
            objRectangle.Width = width;
            objRectangle.Height = height;
            objRectangle.Fill = brush;
            Canvas.SetTop(objRectangle, topBound);
            Canvas.SetLeft(objRectangle, leftBound);

            hitBox = new Rect();
            hitBox.X = leftBound;
            hitBox.Y = topBound;
            hitBox.Width = width;
            hitBox.Height = height;
        }

        public GameObject(string name, int height, int width, int leftBound, int topBound, string uri)
        {
            texture = new ImageBrush();
            texture.ImageSource = new BitmapImage(new Uri(uri));

            objRectangle = new Rectangle();
            objRectangle.Name = name;
            objRectangle.Width = width;
            objRectangle.Height = height;
            objRectangle.Fill = texture;
            Canvas.SetTop(objRectangle, topBound);
            Canvas.SetLeft(objRectangle, leftBound);


            hitBox = new Rect();
            hitBox.X = leftBound;
            hitBox.Y = topBound;
            hitBox.Width = width;
            hitBox.Height = height;


        }

        public abstract void Update();

        // Add additional properties and methods as needed

        // Example properties:
        public Rect HitBox
        {
            get { return hitBox; }
            set { hitBox = value; }
        }

        public Rectangle ObjRectangle
        {
            get { return objRectangle; }
            set { objRectangle = value; }
        }

        public ImageBrush Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        // Example methods:
        public void Draw(Canvas canvas)
        {
            // Add the object to the canvas
            canvas.Children.Add(objRectangle);
        }

        // Add other necessary methods and logic for the GameObject class
    }
}
