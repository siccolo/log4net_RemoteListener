using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace log4netTester
{
    public partial class Form1 : Form
    {
        private readonly ILogger _Logger;
        public Form1(ILogger logger)
        {
            _Logger = logger ?? throw new System.ArgumentNullException("logger is missing");

            InitializeComponent();
        }

        private void buttonGenerateError_Click(object sender, EventArgs e)
        {
            _Logger.LogError("error #1");
        }

        private void buttonGenerateInfo_Click(object sender, EventArgs e)
        {
            _Logger.LogInfo("some info #1");
        }
    }
}
