using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace Meow {
    class BrickForm : Form {
        protected override CreateParams CreateParams {
            get {
                var Params = base.CreateParams;
                Params.ExStyle |= 0x00000080;
                return Params;
            }
        }
    }

    class Program {
        static Random rand = new Random();

        public static void Main(){
            while(true){
                Task.Run(()=>{
                    CreateBrick();
                });

                Thread.Sleep(2000);
            }
        }

        public static void CreateBrick(){
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            var form = new BrickForm();
            form.FormBorderStyle = FormBorderStyle.None;
            form.TransparencyKey = Color.DeepPink;
            form.BackColor = Color.DeepPink;
            form.ShowInTaskbar = false;

            var lbl = new Label();
            var img = Image.FromFile("brick.png");
            lbl.Image = img;
            lbl.Size = new Size(img.Width, img.Height);

            form.Location = new Point(rand.Next(0, screenWidth - img.Width), rand.Next(0, screenHeight - img.Height));
            form.ClientSize = lbl.Size;

            form.Closing += (o, e)=>{
                e.Cancel = true;
            };

            Task.Run(()=>{
                int x = rand.Next(0, screenWidth - img.Width), y = rand.Next(0, screenHeight - img.Height);
                bool xSpeed = false, ySpeed = false;

                while(true){
                    if(xSpeed?(x > 0):((x + img.Width) < screenWidth)){
                        x += 10 * (xSpeed?-1:1);
                    }else{
                        xSpeed = !xSpeed;
                    }

                    if(ySpeed?(y > 0):((y + img.Height) < screenHeight)){
                        y += 10 * (ySpeed?-1:1);
                    }else{
                        ySpeed = !ySpeed;
                    }

                    form.Location = new Point(x, y);
                    Application.DoEvents();
                    Thread.Sleep(20);
                }
            });

            form.Controls.Add(lbl);
            form.ShowDialog();
        }
    }
}