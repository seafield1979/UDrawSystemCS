using System;
using System.Drawing;
using System.Windows.Forms;
using UDrawSystemCS.UView;
using UDrawSystemCS.UUtil;
using UDrawSystemCS.UDraw;

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

        // マウスのイベントを変換するクラス
        private ViewTouch vt;

        private UDrawManager drawManager;

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
                    {
                        drawMode = EDrawMode.Draw1;
                        drawManager.initDrawList();
                        UDrawable obj = new UDrawableRect(100,  "rect1", 100, 100, 100, 100);
                        obj.Color = Color.Yellow;
                        drawManager.addDrawable(obj);

                        UDrawable obj3 = new UDrawableRect(1, "rect2", 200, 200, 100, 100);
                        obj3.Color = Color.Gray;
                        drawManager.addDrawable(obj3);

                        UDrawable obj2 = new UDrawableRect(1, "rect3", 150, 150, 100, 100);
                        obj2.Color = Color.Orange;
                        drawManager.addDrawable(obj2);

                    }
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
                    //DrawDraw1(g);
                    if (drawManager.draw(g))
                    {
                        panel2.Invalidate();
                    }
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
            // treeのノードを開く
            treeView1.ExpandAll();

            drawMode = EDrawMode.Draw1;

            ULog.init();

            vt = new ViewTouch();

            drawManager = new UDrawManager();
            drawManager.init();
            
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

        #region Mouseイベント
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine( "x:" + e.Location.X + " y:" + e.Location.Y);

            vt.args = e;
            vt.MEvent = MouseEvent.Down;
            if (drawManager.touchEvent(vt))
            {
                panel2.Invalidate();
            }
        }
        
        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            vt.args = e;
            vt.MEvent = MouseEvent.Move;
            
            if (drawManager.touchEvent(vt))
            {
                panel2.Invalidate();
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            vt.args = e;
            vt.MEvent = MouseEvent.Up;


            if (drawManager.touchEvent(vt))
            {
                panel2.Invalidate();
            }
        }

        private void panel2_MouseLeave(object sender, EventArgs e)
        {
            vt.MEvent = MouseEvent.Cancel;

            if (drawManager.touchEvent(vt))
            {
                panel2.Invalidate();
            }
        }

        #endregion Mouseイベント

    }
}
