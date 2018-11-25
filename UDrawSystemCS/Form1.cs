using System;
using System.Drawing;
using System.Windows.Forms;

namespace UDrawSystemCS
{
    public enum EDrawMode : int
    {
        Draw1 = 0,
        Draw2,
        Draw3,
        Button1,
        Button2,
        Button3,
        List1,
        List2,
        List3,
        SB1,
        SB2,
        SB3
    }

    public partial class Form1 : Form
    {
        //
        // Properties
        //
        #region プロパティ

        public EDrawMode drawMode;

        #endregion プロパティ

        public Form1()
        {
            InitializeComponent();

            Initialize();
        }

        // TreeViewの項目が選択された
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Console.WriteLine(e.Node.Text);

            switch (e.Node.Tag)
            {
                case "101":
                    drawMode = EDrawMode.Draw1;
                    break;
                case "102":
                    drawMode = EDrawMode.Draw2;
                    break;
                case "103":
                    drawMode = EDrawMode.Draw3;
                    break;
                case "201":
                    drawMode = EDrawMode.Button1;
                    break;
                case "202":
                    drawMode = EDrawMode.Button2;
                    break;
                case "203":
                    drawMode = EDrawMode.Button3;
                    break;
                case "301":
                    drawMode = EDrawMode.List1;
                    break;
                case "302":
                    drawMode = EDrawMode.List2;
                    break;
                case "303":
                    drawMode = EDrawMode.List3;
                    break;
                case "401":
                    drawMode = EDrawMode.SB1;
                    break;
                case "402":
                    drawMode = EDrawMode.SB2;
                    break;
                case "403":
                    drawMode = EDrawMode.SB3;
                    break;
                default:
                    return;
            }
            panel2.Invalidate();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            switch (drawMode)
            {
                case EDrawMode.Draw1:
                    DrawDraw1(g);
                    break;
                case EDrawMode.Draw2:
                    DrawDraw2(g);
                    break;
                case EDrawMode.Draw3:
                    DrawDraw3(g);
                    break;
                case EDrawMode.Button1:
                    break;
                case EDrawMode.Button2:
                    break;
                case EDrawMode.Button3:
                    break;
                case EDrawMode.List1:
                    break;
                case EDrawMode.List2:
                    break;
                case EDrawMode.List3:
                    break;
                case EDrawMode.SB1:
                    break;
                case EDrawMode.SB2:
                    break;
                case EDrawMode.SB3:
                    break;
            }
        }

        #region メソッド
        private void Initialize()
        {
            drawMode = EDrawMode.Draw1;
        }
        #endregion メソッド

        #region DrawModeメソッド

        private void DrawDraw1(Graphics g)
        {
            g.FillRectangle(Brushes.Red, 100, 100, 100, 100);
        }
        private void DrawDraw2(Graphics g)
        {
            g.FillRectangle(Brushes.Green, 100, 100, 100, 100);
        }
        private void DrawDraw3(Graphics g)
        {
            g.FillRectangle(Brushes.Blue, 100, 100, 100, 100);
        }

        #endregion DrawModeメソッド
    }
}
