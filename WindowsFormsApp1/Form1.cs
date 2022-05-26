using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;
using Form1;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : MaterialForm
    {
        string processForBoost = "A";

        ServiceBooster sb = new ServiceBooster();
        ProcessBooster pbs = new ProcessBooster();
        Cleaner cl = new Cleaner();
        PCInfo pci = new PCInfo();  

        public Form1()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }
      
        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {

                    pbs.FoundProcessForBoost();
                    pbs.SetPriorityHigh();
               
            }
            if (checkBox2.Checked)
            {
                cl.CleanTemp();
            }
            if (checkBox3.Checked)
            {
                
                string[] ServiceForBoost = new string[2];
                ServiceForBoost[0] = "Факс";
                ServiceForBoost[1] = "Dmwappushservice";
                sb.ServiceClose(ServiceForBoost);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {

            if (checkBox1.Checked)
            {
 
                pbs.SetPriorityDefault();
            }
            
            if (checkBox3.Checked)
            {
                string[] ServiceForBoost = new string[2];
                ServiceForBoost[0] = "Факс";
                ServiceForBoost[1] = "Dmwappushservice";
                sb.ServiceStart(ServiceForBoost);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pci.GetPCInfo();

            materialLabel9.Text = pci._CPU;
            materialLabel10.Text = Convert.ToString(pci._Cores);
            materialLabel11.Text = Convert.ToString(pci._LogicalCores);
            materialLabel12.Text = pci._GPU[0];
            materialLabel13.Text = Convert.ToString(pci._GPUMemory[0]);
            materialLabel14.Text = Convert.ToString(pci._Memory);
            if (pci._GPU.Length==2)
            {
                materialLabel6.Visible = true;
                materialLabel7.Visible = true;
                materialLabel15.Visible = true;
                materialLabel16.Visible = true;
                materialLabel15.Text = pci._GPU[1];
                materialLabel16.Text = Convert.ToString(pci._GPUMemory[1]);
            }
        }
    }
  

    

}
