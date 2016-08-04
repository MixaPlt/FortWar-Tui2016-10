using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FortWar
{
    class EditMap
    {
        //Для удобства
        private int fieldHeight = Properties.Settings.Default.gameHeight;
        private int fieldWidth = Properties.Settings.Default.gameWidth;
        //Ссылочки на онно и сетку
        Window MainWindow;
        Canvas MainCanvas;
        //Само поле
        //0 - Пустя клеточка, 1 - горы, 2 - река, 3 - клетка первого, 4 - клеткаа второго, 5 - горы первого, 6 - горы второго, 7 - замок первого, 8 - замок второго, 9 - крепость первого, 10 - крепость второго
        private int[, ] field = new int[55, 55];
        //Высота и ширина поля
        private int loadedFieldHeight = 0, loadedFieldWidth = 0;
        //Абсолютная высота и ширина шестиугольничков imageWidth здесь для удобства (т.к. всегда равен imageHeight * 1.1547 = (2 / sqrt(3))
        private double imageWidth, imageHeight;
        public void Build(Canvas mainCanvas, Window mainWindow)
        {
            //Чистим-чистим хорошо,  чтобы было чисто
            mainCanvas.Children.Clear();
            //Тут всё ясно
            MainWindow = mainWindow;
            MainCanvas = mainCanvas;
            //Если поле существует, добавляем его, обнуляем field
            //Проверка файла (наличие и подходящие размеры)
            bool isLoad = false;
            //Считываемый текст
            String text = "";
            try
            {
                text = System.IO.File.ReadAllText("SecondModeCustomField.map");
                if (text.Length >= 6)
                {
                    loadedFieldHeight = ((int)text[0] - 48) * 10 + (int)text[1];
                    loadedFieldHeight = ((int)text[2] - 48) * 10 + (int)text[3];
                    //Проверка *правильности* файла
                    if (loadedFieldHeight > 0 && loadedFieldHeight <= 50 && loadedFieldWidth > 0 && loadedFieldWidth <= 50 && text.Length == loadedFieldHeight * loadedFieldWidth + 4)
                        isLoad = true;
                }
            }
            catch (System.IO.FileNotFoundException)
            { isLoad = false; }
            //Обнуление массива поля
            for (int i = 0; i < Properties.Settings.Default.gameHeight; i++)
                for (int j = 0; j < Properties.Settings.Default.gameWidth; j++)
                    field[i, j] = 0;
            //Повторная проверка всех элементов файла и запись сохранения в массив field
            if(isLoad)
            {
                bool isZeroingRepeat = false;
                for(int i = 0; i < loadedFieldHeight; i++)
                    for(int j = 0; j < loadedFieldWidth; j++)
                    {
                        field[i, j] = (int)text[loadedFieldWidth * i + j + 4];
                        if (field[i, j] < 0 || field[i, j] > 2)
                            isZeroingRepeat = true;
                    }
                if(isZeroingRepeat)
                {
                    for (int i = 0; i < fieldHeight; i++)
                        for (int j = 0; j < fieldWidth; j++)
                            field[i, j] = 0;
                }
            }
            //Поле загружено и полностью готово к использованию
            //Вычисление размеров шестиугольничков
            imageWidth = MainWindow.Width * 4 / (3 * fieldWidth + 1);
            imageHeight = (MainWindow.Height - 26) / (fieldHeight + 0.5);
            if (imageWidth > imageHeight * 1.1547)
                imageWidth = imageHeight * 1.1547;
            else
                imageHeight = imageWidth / 1.1547;
            //Все соурсы в конце
        }

    }
}
