﻿using Microsoft.Toolkit.Mvvm.Input;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TestingPlanner.Data;
using TestingPlanner.Domain.Models;
using TestingPlanner.Views;

namespace TestingPlanner.Viewmodels
{
    public class ViewmodelRequestForm : ViewModelBase
    {
        // Jobrequest data container
        // Only one getter/setter needs to be made for all changes in GUI
        private JR _jr;
        private EUT _eut;

        // EUT's
        // Does not necessarily need to be linked to JR? We can retrieve the JR ID and add it in DAO
        public ObservableCollection<EUT> EUTs { get; set; }
        private EUT _selectedEUT;

        // Combobox contents
        public ObservableCollection<string> JobNatures { get; set; }
        public ObservableCollection<string> Divisions { get; set; }

        // Command declaration
        // RelayCommand<Class> takes object of type class as input
        public RelayCommand<Window> addJobRequestCommand { get; set; }
        public RelayCommand<Window> cancelCommand { get; set; }
        // ICommand does not take pinput
        public ICommand addEUTCommand { get; set; }
        public ICommand removeEUTCommand { get; set; }
        public ICommand refreshJRCommand { get; set; }
        public ICommand addMockEUTCommand { get; set; }

        // Constructor for new JR
        public ViewmodelRequestForm()
        {
            init();

            // JR = new JR
            refreshJR();

            // addJRCommand calls function to insert new JR
            addJobRequestCommand = new RelayCommand<Window>(InsertJr);
        }

        // Constructor for existing JR
        public ViewmodelRequestForm(int idRequest)
        {
            init();

            // Look for JR with correct ID
            this._jr = _dao.GetJRWithId(idRequest);

            // addJRCommand calls function to save existing JR
            addJobRequestCommand = new RelayCommand<Window>(UpdateJr);
        }

        // Code reused in both constructors
        private void init()
        {
            // Collection initialization
            EUTs = new ObservableCollection<EUT>();
            JobNatures = new ObservableCollection<string>();
            Divisions = new ObservableCollection<string>();

            // Command initialization
            cancelCommand = new RelayCommand<Window>(ChangeWindows);
            refreshJRCommand = new DelegateCommand(refreshJR);
            addEUTCommand = new DelegateCommand(addEUT);
            removeEUTCommand = new DelegateCommand(removeSelectedEUT);
            addMockEUTCommand = new DelegateCommand(addMockEUT);
        }

        // Getters/Setters
        public JR JR
        {
            get { return _jr; }
            set
            {
                _jr = value;
                OnpropertyChanged();
            }
        }
        public EUT eut
        {
            get { return _eut; }
            set
            {
                _eut = value;
                OnpropertyChanged();
            }
        }
        public EUT SelectedEUT
        {
            get { return _selectedEUT; }
            set
            {
                _selectedEUT = value;
                OnpropertyChanged();
            }
        }

        // Function used in code behind
        // Loads jobNatures, divisions in cbb
        public void Load()
        {
            var jobNatures = _dao.GetAllJobNatures();
            var divisions = _dao.GetAllDivisions();
            JobNatures.Clear();
            Divisions.Clear();

            foreach (var jobNature in jobNatures)
            {
                JobNatures.Add(jobNature.Nature);
            }

            foreach (var division in divisions)
            {
                Divisions.Add(division.Afkorting);
            }
        }

        // Command functions
        // Adds and stores a job request and switches windows
        public void InsertJr(Window window)
        {
            
            var jr =_dao.AddJobRequest(JR); // SaveChanges included in function

            foreach (var thisEUT in EUTs)
            {
                _dao.AddEutToRqRequest(jr, thisEUT);
            }
            // Here we call the SaveChanges method, so that we can link several EUTs to one JR
            _dao.SaveChanges();
            ChangeWindows(window);          
        }

        // Updates existing job request and switches windows
        public void UpdateJr(Window window)
        {
            string error = _dao.UpdateJobRequest(JR); // SaveChanges included in function

            if (error == null)
            {
                ChangeWindows(window);
            }
            else
            {
                MessageBox.Show(error);
            }    
        }

        // Adds and stores job request in DB via _dao instance
        private void ChangeWindows(Window window)
        {
            MainWindow overviewWindow = new MainWindow();
            overviewWindow.Show();
            window.Close();
        }

        // This function adds an new EUT instance into the GUI RequestForm
        // EUT in Database
        public void addEUT()
        {
            EUTs.Add(new EUT());
        }

        // Clear all data in JR
        private void refreshJR()
        {
            this.JR = _dao.GetNewJR();
            EUTs.Clear();
        }

        // deletes selected EUT via _selectedEut variable
        public void removeSelectedEUT()
        {
            EUTs.Remove(SelectedEUT); 
        }

        // Temporary function to demo loading EUT datatemplate
        public void addMockEUT()
        {
            EUTs.Add(new EUT
            {
                PartNr = "TEST",
                AvailabilityDate = new DateTime(2021, 03, 12),
                NetWeight = "1.8",
                GrossWeight = "2.3",
                EMC = true,
                REL = true
            });
        }
    }
}
