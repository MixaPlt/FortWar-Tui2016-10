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
        //0 - Пустя клеточка, 1 - горы, 2 - река, 3 - клетка первого, 4 - клеткаа второго, 5 - горы первого, 6 - горы второго, 7 - крепость первого, 8 - крепость второго, 9 - замок первого, 10 - замок второго
        private int[, ] field = new int[55, 55];
        //Массив картиночек
        private Image[,] imageField = new Image[51, 51];
        //Соурсы к картинкам поля. Номера аналогичные. Присваиваются в методе MakeSources
        private BitmapImage[] Sources = new BitmapImage[11];
        //Высота и ширина поля
        private int loadedFieldHeight = 0, loadedFieldWidth = 0;
        //Абсолютная высота и ширина шестиугольничков imageWidth здесь для удобства (т.к. всегда равен imageHeight * 1.1547 = (2 / sqrt(3))
        private double imageWidth, imageHeight;
        public void Build(Canvas mainCanvas, Window mainWindow)
        {
            //Инициализация соурсов
            InitSources();
            //Чистим-чистим хорошо,  чтобы было чисто
            mainCanvas.Children.Clear();
            //Тут всё ясно
            MainWindow = mainWindow;
            MainCanvas = mainCanvas;
            //Инициализация ивентов
            MainCanvas.MouseUp += MainCanvasClick;
            MainWindow.KeyUp += AnyKeyUp;
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
                    loadedFieldHeight = ((int)text[0] - 48) * 10 + (int)text[1] - 48;
                    loadedFieldWidth = ((int)text[2] - 48) * 10 + (int)text[3] - 48;
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
                        field[i, j] = (int)text[loadedFieldWidth * i + j + 4] - 48;
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
            imageWidth = (MainWindow.Width) * 4 / (3 * fieldWidth + 1);
            imageHeight = (MainWindow.Height - 50) / (fieldHeight + 0.5);
            if (imageWidth > imageHeight * 1.1547)
                imageWidth = imageHeight * 1.1547;
            else
                imageHeight = imageWidth / 1.1547;
            //Кнопки сохранения и выхода
            Thickness margin = new Thickness() { Left = 0, Top = MainWindow.Height - 62};
            Button exitButton = new Button() { Margin = margin, Height = 24, Width = MainWindow.Width / 2, Content = "Отмена" };
            exitButton.Click += Exit;
            MainCanvas.Children.Add(exitButton);
            margin.Left = MainWindow.Width / 2;
            Button saveButton = new Button() { Margin = margin, Height = 24, Width = MainWindow.Width / 2, Content = "Сохранить" };
            saveButton.Click += Save;
            MainCanvas.Children.Add(saveButton);
            //Заполение поля картиночек
            {
                //Умножаем отступы на 0.97 для избежания белых полос между картинками
                Thickness imageMargin = new Thickness { Top = 0, Left = 0 };
                for (int i = 0; i < fieldHeight; i++)
                {
                    for (int j = 0; j < fieldWidth; j++)
                    {
                        imageField[i, j] = new Image() { Margin = imageMargin, Source = Sources[field[i, j]], Height = imageHeight, Width = imageWidth};
                        MainCanvas.Children.Add(imageField[i, j]);
                        imageMargin.Left += (3 * imageWidth / 4) * 0.97;
                        if (j % 2 == 0)
                            imageMargin.Top += (imageHeight / 2) * 0.97;
                        else
                            imageMargin.Top -= (imageHeight / 2) * 0.97;
                    }
                    imageMargin.Top = imageHeight * (i + 1) * 0.97;
                    imageMargin.Left = 0;
                }
            }     
        }
        //Метод, присваивающий соурсы. Описан в начале
        private void InitSources()
        {
            Sources[0] = new BitmapImage();
            Sources[0].BeginInit();
            Sources[0].UriSource = new Uri("Geks0.png", UriKind.Relative);
            Sources[0].EndInit();

            Sources[1] = new BitmapImage();
            Sources[1].BeginInit();
            Sources[1].UriSource = new Uri("Geks7.png", UriKind.Relative);
            Sources[1].EndInit();
            
            Sources[2] = new BitmapImage();
            Sources[2].BeginInit();
            Sources[2].UriSource = new Uri("Geks10.png", UriKind.Relative);
            Sources[2].EndInit();
            
            Sources[3] = new BitmapImage();
            Sources[3].BeginInit();
            Sources[3].UriSource = new Uri("Geks5.png", UriKind.Relative);
            Sources[3].EndInit();
            
            Sources[4] = new BitmapImage();
            Sources[4].BeginInit();
            Sources[4].UriSource = new Uri("Geks6.png", UriKind.Relative);
            Sources[4].EndInit();
            
            Sources[5] = new BitmapImage();
            Sources[5].BeginInit();
            Sources[5].UriSource = new Uri("Geks8.png", UriKind.Relative);
            Sources[5].EndInit();
            
            Sources[6] = new BitmapImage();
            Sources[6].BeginInit();
            Sources[6].UriSource = new Uri("Geks9.png", UriKind.Relative);
            Sources[6].EndInit();
            
            Sources[7] = new BitmapImage();
            Sources[7].BeginInit();
            Sources[7].UriSource = new Uri("Geks3.png", UriKind.Relative);
            Sources[7].EndInit();
            
            Sources[8] = new BitmapImage();
            Sources[8].BeginInit();
            Sources[8].UriSource = new Uri("Geks4.png", UriKind.Relative);
            Sources[8].EndInit();
            
            Sources[9] = new BitmapImage();
            Sources[9].BeginInit();
            Sources[9].UriSource = new Uri("Geks1.png", UriKind.Relative);
            Sources[9].EndInit();
            
            Sources[10] = new BitmapImage();
            Sources[10].BeginInit();
            Sources[10].UriSource = new Uri("Geks2.png", UriKind.Relative);
            Sources[10].EndInit();
        }
        //Метод события клика по полю
        private void MainCanvasClick(object sender, MouseEventArgs e)
        {
            for(int i = 0; i < fieldHeight; i++)  
                for(int j = 0; j < fieldWidth; j++)
                    if(imageField[i, j].IsMouseOver)
                    {
                        field[i, j] = (field[i, j] + 1) % 3;
                        imageField[i, j].Source = Sources[field[i, j]];
                        return;
                    }
        }
        //Метод события нажатия кнопки
        private void AnyKeyUp(object sender, KeyEventArgs e)
        {

        }
        //Выход
        private void Exit (object sender, RoutedEventArgs e)
        {
            MainCanvas.MouseUp -= MainCanvasClick;
            MainWindow.KeyUp -= AnyKeyUp;
            GlobalSettings globalSettings = new GlobalSettings();
            globalSettings.Build(MainCanvas, MainWindow);
        }
        //Сохранение
        private void Save(object sender, RoutedEventArgs e)
        {
            String text = "";
            if (fieldHeight < 10)
                text += "0" + fieldHeight.ToString();
            else
                text += fieldHeight.ToString();
            if (fieldWidth < 10)
                text += "0" + fieldWidth.ToString();
            else
                text += fieldWidth.ToString();
            for (int i = 0; i < fieldHeight; i++)
            {
                for (int j = 0; j < fieldWidth; j++)
                {
                    text += field[i, j].ToString();
                }
            }
            System.IO.File.WriteAllText("SecondModeCustomField.map", text);
            Exit(null, null);
        }
    }
}
