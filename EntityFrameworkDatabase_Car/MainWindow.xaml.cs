using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EntityFrameworkDatabase_Car
{
    public partial class MainWindow : Window
    {
        private readonly CarDbContext carContext = new CarDbContext();

        public ObservableCollection<Car> Cars { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Cars = new ObservableCollection<Car>(carContext.Cars.ToList());
            DataContext = this;
        }
          

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool isAnyFieldFilled = !string.IsNullOrEmpty(TbMark.Text) || !string.IsNullOrEmpty(TbModel.Text) ||
                                    !string.IsNullOrEmpty(TbYear.Text) || !string.IsNullOrEmpty(TbStNumber.Text);

            BtnDelete.IsEnabled = CarsListBox.SelectedItem != null;
            BtnUpdate.IsEnabled = isAnyFieldFilled && CarsListBox.SelectedItem != null;
        }

        private void Cars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CarsListBox.SelectedItem is Car selectedCar)
            {
                BtnDelete.IsEnabled = true;
                TbMark.Text = selectedCar.Mark;
                TbModel.Text = selectedCar.Model;
                TbYear.Text = selectedCar.Year.ToString();
                TbStNumber.Text = selectedCar.StNumber.ToString();
            }
        }


        private void TbClear()
        {
            TbMark.Clear();
            TbModel.Clear();
            TbYear.Clear();
            TbStNumber.Clear();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TbMark.Text) || string.IsNullOrEmpty(TbModel.Text)) return;

            Car newCar = new Car
            {
                Model = TbModel.Text,
                Mark = TbMark.Text,
                Year = Convert.ToInt32(TbYear.Text),
                StNumber = Convert.ToInt32(TbStNumber.Text)
            };

            carContext.Cars.Add(newCar);
            carContext.SaveChanges();

            Cars.Clear();
            carContext.Cars.ToList().ForEach(c => Cars.Add(c));

            TbClear();
        }


        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (CarsListBox.SelectedItem is not Car selectedCar) return;

            Car car = carContext.Cars.FirstOrDefault(c => c.Id == selectedCar.Id);
            if (car != null)
            {
                car.Mark = TbMark.Text;
                car.Model = TbModel.Text;
                car.Year = Convert.ToInt32(TbYear.Text);
                car.StNumber = Convert.ToInt32(TbStNumber.Text);

                carContext.Cars.Update(car);
                carContext.SaveChanges();

                Cars.Clear();
                carContext.Cars.ToList().ForEach(c => Cars.Add(c));

                TbClear();
            }
        }


        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (CarsListBox.SelectedItem is not Car selectedCar) return;

            Car car = carContext.Cars.FirstOrDefault(c => c.Id == selectedCar.Id);
            if (car != null)
            {
                carContext.Cars.Remove(car);
                carContext.SaveChanges();

                Cars.Clear();
                carContext.Cars.ToList().ForEach(c => Cars.Add(c));

                TbClear();
                CarsListBox.SelectedItem = null;
            }
        }
    }
}

