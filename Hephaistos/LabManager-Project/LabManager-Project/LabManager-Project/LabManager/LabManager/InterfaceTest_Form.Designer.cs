namespace LabManager
{
    partial class InterfaceTest_Form
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
            this.statusStrip_TCPIP = new System.Windows.Forms.StatusStrip();
            this.textBox_Message = new System.Windows.Forms.TextBox();
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.textBox_Port = new System.Windows.Forms.TextBox();
            this.label_IP = new System.Windows.Forms.Label();
            this.label_Port = new System.Windows.Forms.Label();
            this.button_Test = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // statusStrip_TCPIP
            // 
            this.statusStrip_TCPIP.Location = new System.Drawing.Point(0, 262);
            this.statusStrip_TCPIP.Name = "statusStrip_TCPIP";
            this.statusStrip_TCPIP.Size = new System.Drawing.Size(409, 22);
            this.statusStrip_TCPIP.TabIndex = 0;
            this.statusStrip_TCPIP.Text = "statusStrip1";
            // 
            // textBox_Message
            // 
            this.textBox_Message.Location = new System.Drawing.Point(12, 97);
            this.textBox_Message.Multiline = true;
            this.textBox_Message.Name = "textBox_Message";
            this.textBox_Message.Size = new System.Drawing.Size(386, 154);
            this.textBox_Message.TabIndex = 1;
            // 
            // textBox_IP
            // 
            this.textBox_IP.Location = new System.Drawing.Point(56, 59);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(131, 20);
            this.textBox_IP.TabIndex = 2;
            // 
            // textBox_Port
            // 
            this.textBox_Port.Location = new System.Drawing.Point(284, 59);
            this.textBox_Port.Name = "textBox_Port";
            this.textBox_Port.Size = new System.Drawing.Size(100, 20);
            this.textBox_Port.TabIndex = 3;
            // 
            // label_IP
            // 
            this.label_IP.AutoSize = true;
            this.label_IP.Location = new System.Drawing.Point(30, 62);
            this.label_IP.Name = "label_IP";
            this.label_IP.Size = new System.Drawing.Size(20, 13);
            this.label_IP.TabIndex = 4;
            this.label_IP.Text = "IP:";
            // 
            // label_Port
            // 
            this.label_Port.AutoSize = true;
            this.label_Port.Location = new System.Drawing.Point(240, 63);
            this.label_Port.Name = "label_Port";
            this.label_Port.Size = new System.Drawing.Size(29, 13);
            this.label_Port.TabIndex = 5;
            this.label_Port.Text = "Port:";
            // 
            // button_Test
            // 
            this.button_Test.Location = new System.Drawing.Point(33, 21);
            this.button_Test.Name = "button_Test";
            this.button_Test.Size = new System.Drawing.Size(351, 22);
            this.button_Test.TabIndex = 6;
            this.button_Test.Text = "Test";
            this.button_Test.UseVisualStyleBackColor = true;
            this.button_Test.Click += new System.EventHandler(this.button_Test_Click);
            // 
            // InterfaceTest_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 284);
            this.Controls.Add(this.button_Test);
            this.Controls.Add(this.label_Port);
            this.Controls.Add(this.label_IP);
            this.Controls.Add(this.textBox_Port);
            this.Controls.Add(this.textBox_IP);
            this.Controls.Add(this.textBox_Message);
            this.Controls.Add(this.statusStrip_TCPIP);
            this.Name = "InterfaceTest_Form";
            this.Text = "InterfaceConfig_Form";
            this.Load += new System.EventHandler(this.InterfaceConfig_Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip_TCPIP;
        private System.Windows.Forms.TextBox textBox_Message;
        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.TextBox textBox_Port;
        private System.Windows.Forms.Label label_IP;
        private System.Windows.Forms.Label label_Port;
        private System.Windows.Forms.Button button_Test;
    }
}