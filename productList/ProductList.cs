using iTextSharp.text.pdf;
using iTextSharp.text;
using productList.model;
using productList.Properties;
using productList.provider;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace productList
{
    public partial class ProductList : Form
    {
        private ProductProvider productProvider;
        private List<Product> productList;
        private List<string> typeList;
        private const int itemsPerPage = 5;
        private int currentPage = 1;
        private int offset;
        private int limit;
        private int totalPages;

        public ProductList()
        {
            InitializeComponent();
            productProvider = new ProductProvider();
            productList = new List<Product>();
            searchTxt.Text = "Введите для поиска";
            searchTxt.ForeColor = Color.Gray;
            productList = productProvider.GetAllProducts();
            typeList = productProvider.GetAllTypes();
            string[] typeArray = new string[typeList.Count];
            typeList.CopyTo(typeArray, 0);

            sortBox.DrawMode = DrawMode.OwnerDrawFixed;
            sortBox.ItemHeight = 29;
            filterBox.DrawMode = DrawMode.OwnerDrawFixed;
            filterBox.ItemHeight = 29;
            filterBox.Items.Add("Все типы");
            filterBox.Items.AddRange(typeArray);

            searchTxt.Multiline = false;
            searchTxt.AutoSize = false;
            searchTxt.Height = 35;

            DisplayPage(currentPage, null, null, null);
        }

        private void DisplayPage(int page, string text, string sorter, string filter)
        {
            flowLayoutPanelProducts.Controls.Clear();
            flowLayoutPanelPagination.Controls.Clear();

            offset = (page - 1) * itemsPerPage;
            limit = itemsPerPage;


            var filteredProducts = productList;

            if (!string.IsNullOrEmpty(text) && text != "Введите для поиска")
            {
                filteredProducts = filteredProducts
                    .Where(p => p.Title.Contains(text) ||
                                p.Description.Contains(text))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(sorter) && sorter != "Сортировка")
            {
                switch (sorter)
                {
                    case "По наименованию (по возрастанию)":
                        filteredProducts = filteredProducts.OrderBy(p => p.Title).ToList();
                        break;
                    case "По наименованию (по убыванию)":
                        filteredProducts = filteredProducts.OrderByDescending(p => p.Title).ToList();
                        break;
                    case "По номеру производственного цеха (по возрастанию)":
                        filteredProducts = filteredProducts.OrderBy(p => p.ProductShopNumber).ToList();
                        break;
                    case "По номеру производственного цеха (по убыванию)":
                        filteredProducts = filteredProducts.OrderByDescending(p => p.ProductShopNumber).ToList();
                        break;
                    case "По стоимости (по возрастанию)":
                        filteredProducts = filteredProducts.OrderBy(p => p.Cost).ToList();
                        break;
                    case "По стоимости (по убыванию)":
                        filteredProducts = filteredProducts.OrderByDescending(p => p.Cost).ToList();
                        break;
                    default:
                        break;
                }
            }

            if (!string.IsNullOrEmpty(filter) && filter != "Фильтрация" && filter != "Все типы")
            {
                filteredProducts = filteredProducts
                    .Where(p => p.Type.Equals(filter))
                    .ToList();
            }

            int resultCount = filteredProducts.Count;   
            filteredProducts = filteredProducts.Skip(offset).Take(limit).ToList();

            for (int i = 0; i < filteredProducts.Count; i++)
            {
                DisplayProduct(filteredProducts[i]);
            }

            DisplayPagination(resultCount);
        }

        public System.Drawing.Image byteArrayToImage(byte[] bytesArr)
        {
            try
            {
                using (MemoryStream memstr = new MemoryStream(bytesArr))
                {
                    System.Drawing.Image img = System.Drawing.Image.FromStream(memstr);
                    return img;
                } 
            }
            catch
            {
                return Resources.pic;
            }
        }

        private void DisplayProduct(Product product)
        {
            Panel productPanel = new Panel
            {
                Height = 120,
                Width = flowLayoutPanelProducts.Width - 30,
                BorderStyle = BorderStyle.FixedSingle,
            };

            PictureBox pictureBox = new PictureBox
            {
                Image = byteArrayToImage(product.Image),
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 120,
                Height = 120,
            };
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            Label attributesLabel = new Label
            {
                Text = $"Артикул: \n{product.Article}",
                Location = new Point(130, 40),
                Width = 150,
                Height = 50,
                Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 10)
            };

            attributesLabel.Click += (sender, e) =>
            {
                string fullText = ((Label)sender).Text;
                string articleNum = fullText.Split(' ')[1].Trim();
                DrawBarcode(articleNum);
            };

            Label nameLabel = new Label
            {
                Text = product.Title,
                Location = new Point(300, 20),
                Width = 200,
                Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold)
            };

            Label descriptionLabel =     new Label
            {
                Text = product.Description,
                Location = new Point(300, 45),
                Width = 200,
                Height = 100,
            };

            Label costLabel = new Label
            {
                Text = $"Стоимость: \n {product.Cost:C}",
                Location = new Point(flowLayoutPanelProducts.Width - 150, 20),
                Width = 100,
                Height = 50,
                Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold)
            };

            productPanel.Controls.Add(pictureBox);
            productPanel.Controls.Add(attributesLabel);
            productPanel.Controls.Add(nameLabel);
            productPanel.Controls.Add(descriptionLabel);
            productPanel.Controls.Add(costLabel);

            flowLayoutPanelProducts.Controls.Add(productPanel);
        }

        private void DisplayPagination(int count)
        {
            totalPages = (int)Math.Ceiling((double)count / itemsPerPage);

            flowLayoutPanelPagination.RightToLeft = RightToLeft.Yes;

            AddPageButton("<", currentPage + 1);

            for (int i = totalPages; i != 0; i--)
            {
                Label pageButton = new Label
                {
                    Text = i.ToString(),
                    Width = 20,
                    Height = 20,
                    Margin = new Padding(5),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Cursor = Cursors.Hand
                };

                if (i == currentPage)
                {
                    pageButton.Font = new System.Drawing.Font(pageButton.Font, FontStyle.Bold | FontStyle.Underline);
                }

                pageButton.Click += (sender, e) =>
                {
                    currentPage = int.Parse(((Label)sender).Text);
                    DisplayPage(currentPage, searchTxt.Text, sortBox.Text, filterBox.Text);
                };

                flowLayoutPanelPagination.Controls.Add(pageButton);
            }

            AddPageButton(">", currentPage - 1);
        }

        private void AddPageButton(string buttonText, int targetPage)
        {
            Label pageButton = new Label
            {
                Text = buttonText,
                Width = 15,
                Height = 15,
                Margin = new Padding(5),
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand
            };

            pageButton.Click += (sender, e) =>
            {
                if (targetPage > 0 && targetPage <= totalPages)
                {
                    currentPage = targetPage;
                    DisplayPage(currentPage, searchTxt.Text, sortBox.Text, filterBox.Text);
                }
            };

            flowLayoutPanelPagination.Controls.Add(pageButton);
        }

        private void searchTxt_Enter(object sender, EventArgs e)
        {
            if (searchTxt.Text.Equals("Введите для поиска"))
            {
                searchTxt.Clear();
                searchTxt.ForeColor = Color.Black;
            }
        }

        private void searchTxt_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchTxt.Text))
            {
                searchTxt.Text = "Введите для поиска";
                searchTxt.ForeColor = Color.Gray;
            }
        }

        private void searchTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            currentPage = 1;
            DisplayPage(currentPage, searchTxt.Text, sortBox.Text, filterBox.Text);
        }

        private void sortBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            using (SolidBrush brush = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(sortBox.Items[e.Index].ToString(), e.Font, brush, e.Bounds);
            }
            e.DrawFocusRectangle();
        }

        private void filterBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            using (SolidBrush brush = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(filterBox.Items[e.Index].ToString(), e.Font, brush, e.Bounds);
            }
            e.DrawFocusRectangle();
        }

        private void sortBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPage = 1;
            DisplayPage(currentPage, searchTxt.Text, sortBox.Text, filterBox.Text);
        }

        private void filterBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            currentPage = 1;
            DisplayPage(currentPage, searchTxt.Text, sortBox.Text, filterBox.Text);
        }

        private void DrawBarcode(string code, int resolution = 20) // resolution - пикселей на миллиметр
        {
            int numberCount = 13; // количество цифр
            float height = 25.93f * resolution; // высота штрих кода
            float lineHeight = 22.85f * resolution; // высота штриха
            float leftOffset = 3.63f * resolution; // свободная зона слева
            float rightOffset = 2.31f * resolution; // свободная зона справа
                                                    //штрихи, которые образуют правый и левый ограничивающие знаки,
                                                    //а также центральный ограничивающий знак должны быть удлинены вниз на 1,65мм
            float longLineHeight = lineHeight + 1.65f * resolution;
            float fontHeight = 2.75f * resolution; // высота цифр
            float lineToFontOffset = 0.165f * resolution; // минимальный размер от верхнего края цифр до нижнего края штрихов
            float lineWidthDelta = 0.15f * resolution; // ширина 0.15*{цифра}
            float lineWidthFull = 1.35f * resolution; // ширина белой полоски при 0 или 0.15*9
            float lineOffset = 0.2f * resolution; // между штрихами должно быть расстояние в 0.2мм

            float width = leftOffset + rightOffset + 6 * (lineWidthDelta + lineOffset) + numberCount * (lineWidthFull + lineOffset); // ширина штрих-кода

            Bitmap bitmap = new Bitmap((int)width, (int)height); // создание картинки нужных размеров
            Graphics g = Graphics.FromImage(bitmap); // создание графики

            System.Drawing.Font font = new System.Drawing.Font("Arial", fontHeight, FontStyle.Regular, GraphicsUnit.Pixel); // создание шрифта

            StringFormat fontFormat = new StringFormat(); // Центрирование текста
            fontFormat.Alignment = StringAlignment.Center;
            fontFormat.LineAlignment = StringAlignment.Center;

            float x = leftOffset; // позиция рисования по x
            for (int i = 0; i < numberCount; i++)
            {
                int number = Convert.ToInt32(code[i].ToString()); // число из кода
                if (number != 0)
                {
                    g.FillRectangle(Brushes.Black, x, 0, number * lineWidthDelta, lineHeight); // рисуем штрих
                }
                RectangleF fontRect = new RectangleF(x, lineHeight + lineToFontOffset, lineWidthFull, fontHeight); // рамки для буквы
                g.DrawString(code[i].ToString(), font, Brushes.Black, fontRect, fontFormat); // рисуем букву
                x += lineWidthFull + lineOffset; // смещаем позицию рисования по x


                if (i == 0 || i == numberCount / 2 ||  i == numberCount - 1) // если это начало, середина или конец кода рисуем разделители
                { 
                    for (int j = 0; j < 2; j++) // рисуем 2 линии разделителя
                    {
                        g.FillRectangle(Brushes.Black, x, 0, lineWidthDelta, longLineHeight); // рисуем длинный штрих
                        x += lineWidthDelta + lineOffset; // смещаем позицию рисования по x
                    }
                }
            }
            Hide();
            MessageForm messageForm = new MessageForm();
            messageForm.barcode = bitmap;
            messageForm.Code = code;
            messageForm.FormClosed += (s, args) => Close();
            messageForm.ShowDialog();   
        }
    }
}
