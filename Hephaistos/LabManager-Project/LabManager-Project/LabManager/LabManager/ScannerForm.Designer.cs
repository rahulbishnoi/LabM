namespace LabManager
{
    partial class ScannerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
     /*   protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        */
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_Receive = new System.Windows.Forms.TextBox();
            this.button_Command1 = new System.Windows.Forms.Button();
            this.label_IP = new System.Windows.Forms.Label();
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.textBox_Message = new System.Windows.Forms.TextBox();
            this.label_Port = new System.Windows.Forms.Label();
            this.textBox_Port = new System.Windows.Forms.TextBox();
            this.button_Command2 = new System.Windows.Forms.Button();
            this.textBox_Message2 = new System.Windows.Forms.TextBox();
            this.button_Stop = new System.Windows.Forms.Button();
            this.checkBox_Online = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBox_Receive
            // 
            this.textBox_Receive.Location = new System.Drawing.Point(12, 235);
            this.textBox_Receive.Multiline = true;
            this.textBox_Receive.Name = "textBox_Receive";
            this.textBox_Receive.Size = new System.Drawing.Size(423, 154);
            this.textBox_Receive.TabIndex = 14;
            // 
            // button_Command1
            // 
            this.button_Command1.Location = new System.Drawing.Point(54, 12);
            this.button_Command1.Name = "button_Command1";
            this.button_Command1.Size = new System.Drawing.Size(144, 22);
            this.button_Command1.TabIndex = 13;
            this.button_Command1.Text = "Command1";
            this.button_Command1.UseVisualStyleBackColor = true;
            this.button_Command1.Click += new System.EventHandler(this.button_Command1_Click);
            // 
            // label_IP
            // 
            this.label_IP.AutoSize = true;
            this.label_IP.Location = new System.Drawing.Point(12, 47);
            this.label_IP.Name = "label_IP";
            this.label_IP.Size = new System.Drawing.Size(20, 13);
            this.label_IP.TabIndex = 12;
            this.label_IP.Text = "IP:";
            // 
            // textBox_IP
            // 
            this.textBox_IP.Location = new System.Drawing.Point(38, 43);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(131, 20);
            this.textBox_IP.TabIndex = 11;
            // 
            // textBox_Message
            // 
            this.textBox_Message.Location = new System.Drawing.Point(12, 75);
            this.textBox_Message.Multiline = true;
            this.textBox_Message.Name = "textBox_Message";
            this.textBox_Message.Size = new System.Drawing.Size(205, 154);
            this.textBox_Message.TabIndex = 10;
            // 
            // label_Port
            // 
            this.label_Port.AutoSize = true;
            this.label_Port.Location = new System.Drawing.Point(169, 46);
            this.label_Port.Name = "label_Port";
            this.label_Port.Size = new System.Drawing.Size(29, 13);
            this.label_Port.TabIndex = 16;
            this.label_Port.Text = "Port:";
            // 
            // textBox_Port
            // 
            this.textBox_Port.Location = new System.Drawing.Point(204, 43);
            this.textBox_Port.Name = "textBox_Port";
            this.textBox_Port.Size = new System.Drawing.Size(100, 20);
            this.textBox_Port.TabIndex = 15;
            // 
            // button_Command2
            // 
            this.button_Command2.Location = new System.Drawing.Point(259, 12);
            this.button_Command2.Name = "button_Command2";
            this.button_Command2.Size = new System.Drawing.Size(144, 22);
            this.button_Command2.TabIndex = 17;
            this.button_Command2.Text = "Command2";
            this.button_Command2.UseVisualStyleBackColor = true;
            this.button_Command2.Click += new System.EventHandler(this.button_Command2_Click);
            // 
            // textBox_Message2
            // 
            this.textBox_Message2.Location = new System.Drawing.Point(223, 75);
            this.textBox_Message2.Multiline = true;
            this.textBox_Message2.Name = "textBox_Message2";
            this.textBox_Message2.Size = new System.Drawing.Size(212, 154);
            this.textBox_Message2.TabIndex = 18;
            // 
            // button_Stop
            // 
            this.button_Stop.Location = new System.Drawing.Point(288, 398);
            this.button_Stop.Name = "button_Stop";
            this.button_Stop.Size = new System.Drawing.Size(144, 22);
            this.button_Stop.TabIndex = 19;
            this.button_Stop.Text = "Stop Listen";
            this.button_Stop.UseVisualStyleBackColor = true;
            this.button_Stop.Click += new System.EventHandler(this.button_Stop_Click);
            // 
            // checkBox_Online
            // 
            this.checkBox_Online.AutoSize = true;
            this.checkBox_Online.Location = new System.Drawing.Point(368, 43);
            this.checkBox_Online.Name = "checkBox_Online";
            this.checkBox_Online.Size = new System.Drawing.Size(54, 17);
            this.checkBox_Online.TabIndex = 20;
            this.checkBox_Online.Text = "online";
            this.checkBox_Online.UseVisualStyleBackColor = true;
            // 
            // ScannerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 432);
            this.Controls.Add(this.checkBox_Online);
            this.Controls.Add(this.button_Stop);
            this.Controls.Add(this.textBox_Message2);
            this.Controls.Add(this.button_Command2);
            this.Controls.Add(this.label_Port);
            this.Controls.Add(this.textBox_Port);
            this.Controls.Add(this.textBox_Receive);
            this.Controls.Add(this.button_Command1);
            this.Controls.Add(this.label_IP);
            this.Controls.Add(this.textBox_IP);
            this.Controls.Add(this.textBox_Message);
            this.Name = "ScannerForm";
            this.Text = "ScannerForm";
            this.Load += new System.EventHandler(this.ScannerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Receive;
        private System.Windows.Forms.Button button_Command1;
        private System.Windows.Forms.Label label_IP;
        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.TextBox textBox_Message;
        private System.Windows.Forms.Label label_Port;
        private System.Windows.Forms.TextBox textBox_Port;
        private System.Windows.Forms.Button button_Command2;
        private System.Windows.Forms.TextBox textBox_Message2;
        private System.Windows.Forms.Button button_Stop;
        private System.Windows.Forms.CheckBox checkBox_Online;
    }
}