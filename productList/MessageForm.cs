using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace productList
{
    public partial class MessageForm : Form
    {
        private Bitmap _barcode;
        private string _code;
        public MessageForm()
        {
            InitializeComponent();
        }

        public Bitmap barcode
        {
            get { return _barcode; }
            set { _barcode = value; }
        }
        public string Code
        {
            get { return _code; } 
            set { _code = value; } 
        }

        private void trueBtn_Click(object sender, EventArgs e)
        {
            CreateBarCode(barPic.Image, _code);
            Hide();
            ProductList productList = new ProductList();
            productList.FormClosed += (s, args) => Close();
            productList.Show();
        }

        private void CreateBarCode(Image img, string code)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = code + ".pdf";
            saveFileDialog.Filter = "PDF файлы (*.pdf)|*.pdf";

            string filePath;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = saveFileDialog.FileName;
            }
            else
            {
                return;
            }

            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += (s, e) =>
            {
                RectangleF imageRect = new RectangleF(100, 100, 200, 150);
                e.Graphics.DrawImage(img, imageRect);
            };

            printDocument.PrinterSettings.PrintToFile = true;
            printDocument.PrinterSettings.PrintFileName = filePath;
            printDocument.Print();
        }

        private void falseBtn_Click(object sender, EventArgs e)
        {
            Hide();
            ProductList productList = new ProductList();
            productList.FormClosed += (s, args) => Close();
            productList.Show();
        }

        private void MessageForm_Load(object sender, EventArgs e)
        {
            barPic.SizeMode = PictureBoxSizeMode.Zoom;
            barPic.Image = barcode;
        }
    }
}
