namespace Homeworkapp;

public class AddHomework : ContentPage
{

    string homeName;
    bool nameAdded = false;
    string Selectedsubject;
    bool subjectSet = false;


    Entry name;
    Picker subjecktPicekr;



    public AddHomework()
    {

        VerticalStackLayout vs = new VerticalStackLayout() { Margin = 30 };
        //back and add
        {
            Button button = new Button { Text = "Back", FontSize = 25, HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill, BackgroundColor = Colors.LightGray, TextColor = Colors.Black };
            button.Clicked += async (sender, args) =>
            {
                await Navigation.PushModalAsync(new MainMenue());
            };

            Button add = new Button
            {
                Text = "Add",
                FontSize = 25,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                BackgroundColor = Colors.LightGray,
                TextColor = Colors.Black
            };

            add.Clicked += async (sender, args) =>
            {
                if (nameAdded && subjectSet && homeName != "")
                {
                    Networking networking = new Networking();
                    await networking.AddHomework(homeName, Selectedsubject);
                    name.Text = "";
                    subjecktPicekr.SelectedItem = null;
                    subjectSet = false;
                    nameAdded = false;
                }
            };
            Label non = new Label() { Text = "" };

            Grid grid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition{Width = new GridLength(30, GridUnitType.Star) },
                    new ColumnDefinition{Width = new GridLength(10, GridUnitType.Star) },
                    new ColumnDefinition{Width = new GridLength(60, GridUnitType.Star) }
                },
                RowDefinitions =
                {
                    new RowDefinition{Height=new GridLength(50, GridUnitType.Star) },
                    new RowDefinition{Height=new GridLength(50, GridUnitType.Star) }
                }
            };
            Frame frame = new Frame
            {
                BorderColor = Colors.Black,
                Padding = new Thickness(5),
                BackgroundColor = Colors.LightGray,
                Content = button
            };
            Frame frame2 = new Frame
            {
                BorderColor = Colors.Black,
                Padding = new Thickness(5),
                BackgroundColor = Colors.LightGray,

                Content = add
            };


            grid.Add(frame, 0, 0);
            grid.Add(frame2, 2, 0);
            grid.Add(non, 0, 1);
            vs.Add(grid);

        }


        name = new Entry { Placeholder = "Name (max.20)", HorizontalOptions = LayoutOptions.Center, FontSize = 25, WidthRequest = 300, HeightRequest = 50, MaxLength = 20, TextColor = Colors.Black };
        Frame nameF = new Frame
        {
            BorderColor = Colors.LightGray,
            Padding = new Thickness(5),
            Content = name,
            Margin = 3
        };

        List<string> subjects = new List<string>() { "Mathe", "Englisch", "Chemie", "Physik", "Deutsch" };

        subjecktPicekr = new Picker
        {
            Title = "Select subject",
            ItemsSource = subjects,
            FontSize = 25,
            WidthRequest = 300,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.Black
        };
        subjecktPicekr.SelectedIndexChanged += OnPickerSelectedIndexChanged;
        Frame subjecktPicekrF = new Frame
        {
            BorderColor = Colors.LightGray,
            Padding = new Thickness(5),
            Content = subjecktPicekr,
            Margin = 3
        };

        name.TextChanged += nametChange;

        vs.Add(nameF);
        vs.Add(subjecktPicekrF);

        Content = vs;
    }

    void nametChange(object sender, EventArgs e)
    {
        homeName = ((Entry)sender).Text;
        if (!string.IsNullOrEmpty(homeName))
        {
            nameAdded = true;
        }

    }
    void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        if (selectedIndex != -1)
        {
            Selectedsubject = (string)picker.ItemsSource[selectedIndex];
            subjectSet = true;
        }
    }
}