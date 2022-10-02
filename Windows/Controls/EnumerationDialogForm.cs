using Shirehorse.Core.Extensions;
using System.Data;

namespace Shirehorse.Core
{
    public class EnumerationDialogForm<ChoiceEnum> : EnumerationDialogForm where ChoiceEnum : Enum
    {
        public EnumerationDialogForm() : base()
        {
            BuildButtons();
            Show();
        }

        public new ChoiceEnum DialogResult { get; private set; }
        public bool CloseOnSelection { get; set; } = true;

        private static Button ButtonTemplate(ChoiceEnum choice) => new()
        {
            Width = 100,
            Height = 40,
            Text = choice.ToString().CamelCaseAddSpaces(),
            Tag = choice
        };

        private int ButtonSpacing { get; } = 6;

        private void BuildButtons()
        {
            var choices = Enum
                .GetValues(typeof(ChoiceEnum))
                .Cast<ChoiceEnum>();

            int xPos = ButtonSpacing / 2;

            foreach (var choice in choices)
            {
                var button = ButtonTemplate(choice);
                Controls.Add(button);
                button.Location = new Point(xPos, label_message.Height + ButtonSpacing + 4);
                button.Click += Button_Click;

                xPos += ButtonSpacing + button.Width;
            }
        }

        public event EventHandler<ChoiceEnum> ChoiceSelected;
        private void Button_Click(object? sender, EventArgs e)
        {
            if (sender is Button button)
            {
                DialogResult = (ChoiceEnum)button.Tag;
                ChoiceSelected?.Invoke(this, DialogResult);
            }

            if (CloseOnSelection)
            {
                Close();
            }
        }


    }

}
