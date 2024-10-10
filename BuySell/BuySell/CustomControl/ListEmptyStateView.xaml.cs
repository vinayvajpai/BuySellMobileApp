using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace BuySell.CustomControl
{	
	public partial class ListEmptyStateView : ContentView
	{
		ListView _list;

		public static readonly BindableProperty IsLoadingProperty =
			BindableProperty.Create(nameof(IsLoading), typeof(bool), typeof(ListEmptyStateView), false, propertyChanged: HandleIsLoadingChanged);

		public bool IsLoading
		{
			get => (bool)GetValue(IsLoadingProperty);
			set => SetValue(IsLoadingProperty, value);
		}

		public Xamarin.Forms.View EmptyState
		{
			get => this.emptyState.Content;
			set => this.emptyState.Content = value;
		}

		public ListView ListView
		{
			get => _list;
			set
			{
				if (_list != null)
				{
					_list.PropertyChanging -= HandleListViewPropertyChanging;
					_list.PropertyChanged -= HandleListViewPropertyChanged;
					RemoveItemsSourceSubscription(_list);
				}
				_list = value;

				if (_list != null)
				{
					_list.PropertyChanging += HandleListViewPropertyChanging;
					_list.PropertyChanged += HandleListViewPropertyChanged;
					AddItemsSourceSubscription(_list);
				}

				OnListChanged();
			}
		}

		public ListEmptyStateView()
		{
			InitializeComponent();
		}

		private void RemoveItemsSourceSubscription(ListView list)
		{
			if (list?.ItemsSource is INotifyCollectionChanged)
			{
				try
				{
					var collection = list.ItemsSource as INotifyCollectionChanged;
					collection.CollectionChanged -= HandleItemsSourceCollectionChanged;
				}
				// incase the collection wasn't previouslly handled
				catch { }
			}
		}

		void AddItemsSourceSubscription(ListView list)
		{
			if (list?.ItemsSource is INotifyCollectionChanged)
			{
				try
				{
					var collection = list.ItemsSource as INotifyCollectionChanged;
					collection.CollectionChanged += HandleItemsSourceCollectionChanged;
				}
				// incase the collection wasn't previouslly handled
				catch { }
			}
		}

		void HandleListViewPropertyChanging(object sender, Xamarin.Forms.PropertyChangingEventArgs e)
		{
			if (e.PropertyName == ListView.ItemsSourceProperty.PropertyName)
			{
				RemoveItemsSourceSubscription(_list);
			}
		}

		void HandleListViewPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == ListView.ItemsSourceProperty.PropertyName)
			{
				AddItemsSourceSubscription(_list);
				ReloadViewStates();
			}
		}

		void HandleItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			ReloadViewStates();
		}

		void OnListChanged()
		{
			ReloadViewStates();
		}

		void ReloadViewStates()
		{
			var data = _list.ItemsSource;
			if (data == null || !data.Cast<object>().Any())
			{
				_list.IsVisible = false;
				IsVisible = true;

				if (IsLoading)
				{
					spinner.IsRunning = true;
					spinner.IsVisible = true;
					emptyState.IsVisible = false;
				}
				else
				{
					spinner.IsRunning = false;
					spinner.IsVisible = false;
					emptyState.IsVisible = true;
				}
			}
			else
			{
				if (IsLoading)
				{
					_list.IsVisible = false;
					IsVisible = true;
					spinner.IsRunning = true;
					spinner.IsVisible = true;
					emptyState.IsVisible = false;
				}
				else
				{
					IsVisible = false;
					_list.IsVisible = true;
					emptyState.IsVisible = false;
				}
			}
		}

		static void HandleIsLoadingChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var isLoading = (bool)newValue;
			var view = bindable as ListEmptyStateView;

			view.ReloadViewStates();
		}
	}
}

