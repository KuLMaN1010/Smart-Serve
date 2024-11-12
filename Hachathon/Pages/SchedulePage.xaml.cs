using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace Hachathon.Pages
{
    public partial class SchedulePage : ContentPage
    {
        public SchedulePage()
        {
            InitializeComponent();
        }

        // Predefined meal plan for each day of the week
        private readonly Dictionary<string, List<(string Time, string Meal)>> _predefinedMealPlan = new Dictionary<string, List<(string, string)>>
        {
            { "Sunday", new List<(string, string)>
                {
                    ("8am", "Smoothie with spinach"),
                    ("11am", "Quinoa salad with black beans"),
                    ("4pm", "Fruit bowl with nuts"),
                    ("7pm", "Grilled salmon with vegetables")
                }
            },
            { "Monday", new List<(string, string)>
                {
                    ("8am", "Avocado toast with eggs"),
                    ("11am", "Chicken Caesar salad"),
                    ("4pm", "Greek yogurt with berries"),
                    ("7pm", "Stir-fried tofu with rice")
                }
            },
            { "Tuesday", new List<(string, string)>
                {
                    ("8am", "Oatmeal with bananas and honey"),
                    ("11am", "Turkey wrap with veggies"),
                    ("4pm", "Smoothie bowl with chia seeds"),
                    ("7pm", "Pasta with marinara sauce")
                }
            },
            { "Wednesday", new List<(string, string)>
                {
                    ("8am", "Pancakes with maple syrup"),
                    ("11am", "Lentil soup with bread"),
                    ("4pm", "Apple slices with peanut butter"),
                    ("7pm", "Roasted chicken with mashed potatoes")
                }
            },
            { "Thursday", new List<(string, string)>
                {
                    ("8am", "Granola with almond milk"),
                    ("11am", "Veggie burger with sweet potato fries"),
                    ("4pm", "Trail mix"),
                    ("7pm", "Beef stew with carrots and potatoes")
                }
            },
            { "Friday", new List<(string, string)>
                {
                    ("8am", "Scrambled eggs with toast"),
                    ("11am", "Sushi rolls with veggies"),
                    ("4pm", "Protein bar"),
                    ("7pm", "Pizza with a side salad")
                }
            },
            { "Saturday", new List<(string, string)>
                {
                    ("8am", "Bagel with cream cheese"),
                    ("11am", "Tomato soup with grilled cheese"),
                    ("4pm", "Banana and almonds"),
                    ("7pm", "Spaghetti Bolognese")
                }
            }
        };

        // Method to populate the meal plan when the button is clicked
        private async void OnGenerateScheduleClicked(object sender, EventArgs e)
        {
            // Show a loading message
            await DisplayAlert("Generating", "Please wait while your meal schedule is being generated...", "OK");

            // Wait for 3 seconds to simulate AI processing
            await Task.Delay(3000);

            // Populate the meal plan after delay
            PopulatePredefinedMealPlan();
        }

        private void PopulatePredefinedMealPlan()
        {
            ScheduleGrid.Children.Clear();

            // Add Day Labels in Row 0
            string[] days = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            for (int i = 0; i < days.Length; i++)
            {
                var dayLabel = new Label { Text = days[i], HorizontalOptions = LayoutOptions.Center };
                ScheduleGrid.Children.Add(dayLabel);
                Grid.SetRow(dayLabel, 0);
                Grid.SetColumn(dayLabel, i);
            }

            // Loop through each day and add meals to the correct time slots
            foreach (var dayPlan in _predefinedMealPlan)
            {
                string day = dayPlan.Key;
                var meals = dayPlan.Value;

                int column = GetColumnForDay(day); // Get the column for the specific day

                foreach (var mealEntry in meals)
                {
                    string time = mealEntry.Time;
                    string mealDescription = mealEntry.Meal;

                    int row = GetRowForTime(time); // Get the row for the specific time

                    if (row >= 0 && column >= 0) // Ensure valid row and column
                    {
                        Label mealLabel = new Label
                        {
                            Text = $"{time} - {mealDescription}",
                            FontSize = 12,
                            BackgroundColor = Colors.LightYellow,
                            TextColor = Colors.Black,
                            HorizontalOptions = LayoutOptions.Fill,
                            VerticalOptions = LayoutOptions.Fill,
                            Padding = new Thickness(5)
                        };

                        // Add the meal label to the grid at the calculated row and column
                        ScheduleGrid.Children.Add(mealLabel);
                        Grid.SetRow(mealLabel, row);
                        Grid.SetColumn(mealLabel, column);
                    }
                }
            }
        }

        // Helper function to get the column index for the given day
        private int GetColumnForDay(string day)
        {
            return day switch
            {
                "Sunday" => 0,
                "Monday" => 1,
                "Tuesday" => 2,
                "Wednesday" => 3,
                "Thursday" => 4,
                "Friday" => 5,
                "Saturday" => 6,
                _ => -1 // Invalid day, ignore
            };
        }

        // Helper function to get the row index for the given time
        private int GetRowForTime(string time)
        {
            return time switch
            {
                "7am" => 1,
                "8am" => 2,
                "9am" => 3,
                "10am" => 4,
                "11am" => 5,
                "12pm" => 6,
                "1pm" => 7,
                "2pm" => 8,
                "3pm" => 9,
                "4pm" => 10,
                "5pm" => 11,
                "6pm" => 12,
                "7pm" => 13,
                "8pm" => 14,
                "9pm" => 15,
                "10pm" => 16,
                _ => -1 // Invalid time, ignore
            };
        }
    }
}
