using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ExpandableLayout
{
	public class ExpandableLayout : RoundedFrameUWP.RoundedFrame
	{
		private bool IsOpened { get; set; }
		private Grid GridMain { get; set; }
		private Grid RowOne { get; set; }
		private Grid RowTwo { get; set; }

		#region BindableProperties
		public static readonly BindableProperty TitleProperty = BindableProperty.Create(
			propertyName: "Title",
			returnType: typeof(String),
			declaringType: typeof(ExpandableLayout),
			defaultValue: String.Empty);


		public String Title
		{
			get => (String)GetValue(TitleProperty);
			set => SetValue(TitleProperty, value);
		}

		public static readonly BindableProperty TitleColorProperty = BindableProperty.Create(
			propertyName: "TitleColor",
			returnType: typeof(Color),
			declaringType: typeof(ExpandableLayout),
			defaultValue: Color.Black);


		public Color TitleColor
		{
			get => (Color)GetValue(TitleColorProperty);
			set => SetValue(TitleColorProperty, value);
		}

		public static readonly BindableProperty ViewProperty = BindableProperty.Create(
			propertyName: "View",
			returnType: typeof(ContentView),
			declaringType: typeof(ExpandableLayout),
			defaultValue: null);

		public ContentView View
		{
			get => (ContentView)GetValue(ViewProperty);
			set => SetValue(ViewProperty, value);
		}

		public static readonly BindableProperty ImageProperty = BindableProperty.Create(
			propertyName: "Image",
			returnType: typeof(String),
			declaringType: typeof(ExpandableLayout),
			defaultValue: string.Empty);


		public String Image
		{
			get => (String)GetValue(ImageProperty);
			set => SetValue(ImageProperty, value);
		}

		public static readonly BindableProperty PaddinRowOneProperty = BindableProperty.Create(
			propertyName: "PaddinRowOne",
			returnType: typeof(Thickness),
			declaringType: typeof(ExpandableLayout),
			defaultValue: new Thickness(0));


		public Thickness PaddinRowOne
		{
			get => (Thickness)GetValue(PaddinRowOneProperty);
			set => SetValue(PaddinRowOneProperty, value);
		}
#endregion

		public ExpandableLayout()
		{
			this.Padding = 0;
			GridMain = new Grid();
			IsOpened = false;
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			CreateRowDefinitions();
			CreateOneRow();
			CreateTwoRow();
			this.Content = GridMain;
		}

		private void CreateOneRow()
		{
			RowOne = new Grid();
			RowOne.Padding = PaddinRowOne;
			//Label
			var label = new Label
			{
				Text = Title,
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.Center,
				TextColor = TitleColor

			};
			RowOne.Children.Add(label);
			//Image
			var image = new Image
			{
				Source = Image,
				HorizontalOptions = LayoutOptions.End
			};
			RowOne.Children.Add(image);

			//Add gesture
			var tapGesture = new TapGestureRecognizer
			{
				Command = new Command(UpdateUi)
			};
			RowOne.GestureRecognizers.Add(tapGesture);

			//Add to parent
			GridMain.Children.Add(RowOne, 0, 0);
		}

		private void CreateTwoRow()
		{
			RowTwo = new Grid();
			RowTwo.Children.Add(this.View);

			//Gone
			RowTwo.IsVisible = false;

			//Add to parent
			GridMain.Children.Add(RowTwo, 0, 1);
		}



		private void UpdateUi()
		{
			RowTwo.IsVisible = !IsOpened;
			IsOpened = !IsOpened;
		}

		private void CreateRowDefinitions()
		{
			var rowDefinitions = new RowDefinitionCollection();
			var oneRow = new RowDefinition
			{
				Height = GridLength.Auto
			};
			var twoRow = new RowDefinition { Height = GridLength.Auto };
			rowDefinitions.Add(oneRow);
			rowDefinitions.Add(twoRow);
			GridMain.RowDefinitions = rowDefinitions;
		}
	}
}
