using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace WebAppNTCEcommerce2._0.Models
{
    public static class Imagens
    {
        const int BOXHEIGHT = 300;
        const int BOXWIDTH = 300;

        public static string ImagemCorrigida(byte[] imagemAtual)
        {
            Bitmap original, resizedImage;
            int newWidth = 0;
            int newHeight = 0;
            string fileName = string.Empty;
            try
            {
                
                string base64String = Convert.ToBase64String(imagemAtual);
                string imgProduct = "data:image/png;base64," + base64String;

                fileName = Guid.NewGuid().ToString().Substring(0, 8);

                using (FileStream fs = new FileStream("~/imagens/" + fileName, FileMode.Open))
                {
                    original = new Bitmap(fs);
                }

                int rectHeight = BOXHEIGHT;
                int rectWidth = BOXWIDTH;

                if (original.Height == original.Width)
                {
                    resizedImage = new Bitmap(original, rectHeight, rectHeight);
                }
                else
                {
                    float aspect = original.Width / (float)original.Height;

                    newWidth = (int)(rectWidth * aspect);
                    newHeight = (int)(newWidth / aspect);

                    if (newWidth > rectWidth || newHeight > rectHeight)
                    {
                        if (newWidth > newHeight)
                        {
                            newWidth = rectWidth;
                            newHeight = (int)(newWidth / aspect);
                        }
                        else
                        {
                            newHeight = rectHeight;
                            newWidth = (int)(newHeight * aspect);
                        }
                    }
                }
                resizedImage = new Bitmap(original, newWidth, newHeight);
                resizedImage.Save("~/imagens/" + fileName);
                return fileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}