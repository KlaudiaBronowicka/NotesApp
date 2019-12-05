using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NotesApp.Models;
using NotesApp.ViewModels;
using System.Collections.Generic;
using NotesApp.Services;

namespace NotesApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel _viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            BindingContext = viewModel;

        }

        public ItemDetailPage()
        {
            InitializeComponent();
            
            _viewModel = new ItemDetailViewModel();
            BindingContext = _viewModel;
        }
         
        public void Cancel_Clicked(object sender, EventArgs eventArgs)
        {
            Navigation.PopModalAsync();
        }

        public void Save_Clicked(object sender, EventArgs eventArgs)
        {
            if (_viewModel.IsNewNote)
            {
                MessagingCenter.Send(this, "SaveNote", _viewModel.Note);
            }
            else
            {
                MessagingCenter.Send(this, "UpdateNote", _viewModel.Note);
            }

            Navigation.PopModalAsync();
        }
    }
}