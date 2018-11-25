namespace UDrawSystemCS
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Test1");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Test2");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Test3");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Draw", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Test1");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Test2");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Test3");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Button", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Test1");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Test2");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Test3");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("List", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Test1");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Test2");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Test3");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("ScrollBar", new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14,
            treeNode15});
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 567);
            this.panel1.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(200, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 567);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "DrawTest1";
            treeNode1.Tag = "101";
            treeNode1.Text = "Test1";
            treeNode2.Name = "DrawTest2";
            treeNode2.Tag = "102";
            treeNode2.Text = "Test2";
            treeNode3.Name = "DrawTest3";
            treeNode3.Tag = "103";
            treeNode3.Text = "Test3";
            treeNode4.Name = "DrawNode";
            treeNode4.Text = "Draw";
            treeNode5.Name = "ButtonTest1";
            treeNode5.Tag = "201";
            treeNode5.Text = "Test1";
            treeNode6.Name = "ButtonTest2";
            treeNode6.Tag = "202";
            treeNode6.Text = "Test2";
            treeNode7.Name = "ButtonTest3";
            treeNode7.Tag = "203";
            treeNode7.Text = "Test3";
            treeNode8.Name = "ButtonNode";
            treeNode8.Text = "Button";
            treeNode9.Name = "ListTest1";
            treeNode9.Tag = "301";
            treeNode9.Text = "Test1";
            treeNode10.Name = "ListTest2";
            treeNode10.Tag = "302";
            treeNode10.Text = "Test2";
            treeNode11.Name = "ListTest3";
            treeNode11.Tag = "303";
            treeNode11.Text = "Test3";
            treeNode12.Name = "ListNode";
            treeNode12.Text = "List";
            treeNode13.Name = "SBTest1";
            treeNode13.Tag = "401";
            treeNode13.Text = "Test1";
            treeNode14.Name = "SBTest2";
            treeNode14.Tag = "402";
            treeNode14.Text = "Test2";
            treeNode15.Name = "SBTest3";
            treeNode15.Tag = "403";
            treeNode15.Text = "Test3";
            treeNode16.Name = "ScrollBarNode";
            treeNode16.Text = "ScrollBar";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode8,
            treeNode12,
            treeNode16});
            this.treeView1.Size = new System.Drawing.Size(200, 567);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(203, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(610, 567);
            this.panel2.TabIndex = 2;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 567);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel2;
    }
}

