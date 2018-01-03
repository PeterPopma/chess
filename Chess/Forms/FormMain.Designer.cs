using Chess.CustomControls;

namespace Chess.Forms
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonReset = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelMessage = new System.Windows.Forms.Label();
            this.displayMonogame = new CustomControls.Display();
            this.buttonUndo = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(1042, 15);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(144, 38);
            this.buttonReset.TabIndex = 1;
            this.buttonReset.Text = "Reset game";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.labelMessage);
            this.panel1.Location = new System.Drawing.Point(1042, 77);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(324, 94);
            this.panel1.TabIndex = 2;
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMessage.Location = new System.Drawing.Point(22, 17);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(167, 51);
            this.labelMessage.TabIndex = 0;
            this.labelMessage.Text = "XXXXX";
            // 
            // displayMonogame
            // 
            this.displayMonogame.Location = new System.Drawing.Point(12, 12);
            this.displayMonogame.Name = "displayMonogame";
            this.displayMonogame.ParentForm = null;
            this.displayMonogame.Size = new System.Drawing.Size(1014, 1016);
            this.displayMonogame.TabIndex = 0;
            this.displayMonogame.Text = "displayMonogame";
            this.displayMonogame.MouseClick += new System.Windows.Forms.MouseEventHandler(this.displayMonogame_MouseClick);
            // 
            // buttonUndo
            // 
            this.buttonUndo.Location = new System.Drawing.Point(1222, 15);
            this.buttonUndo.Name = "buttonUndo";
            this.buttonUndo.Size = new System.Drawing.Size(144, 38);
            this.buttonUndo.TabIndex = 3;
            this.buttonUndo.Text = "Undo last move";
            this.buttonUndo.UseVisualStyleBackColor = true;
            this.buttonUndo.Click += new System.EventHandler(this.buttonUndo_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.buttonUndo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.displayMonogame);
            this.Name = "FormMain";
            this.Text = "Chess v1.0 (c) P.Popma 2018";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControls.Display displayMonogame;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Button buttonUndo;

        public Display DisplayMonogame { get => displayMonogame; set => displayMonogame = value; }
    }
}